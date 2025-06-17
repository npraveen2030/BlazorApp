using Bunit;
using Bunit.TestDoubles;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using BlazorApp.UI.Components.Pages.Admin;
using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Personalization;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Common;
using FluentAssertions;

namespace BlazorApp.Test.UI
{
    [TestFixture]
    public class UserManagerTests : Bunit.TestContext
    {
        private readonly Mock<IUserManagerService> _userManagerMock;
        private readonly Mock<IProjectLeadPortalService> _portalServiceMock;
        private readonly Mock<IUserAssociatedProjectsService> _userAssociatedProjectsMock;
        private readonly Mock<IJSRuntime> _jsMock;
        private readonly TestAuthorizationContext _authContext;

        public UserManagerTests()
        {
            _userManagerMock = new Mock<IUserManagerService>();
            _portalServiceMock = new Mock<IProjectLeadPortalService>();
            _userAssociatedProjectsMock = new Mock<IUserAssociatedProjectsService>();
            _jsMock = new Mock<IJSRuntime>();

            Services.AddSingleton(_userManagerMock.Object);
            Services.AddSingleton(_portalServiceMock.Object);
            Services.AddSingleton(_userAssociatedProjectsMock.Object);
            Services.AddSingleton(_jsMock.Object);

            _authContext = this.AddTestAuthorization();
            _authContext.SetAuthorized("123");
            _authContext.SetClaims(
                new Claim(ClaimTypes.Name, "123"),
                new Claim(ClaimTypes.Role, "Admin")
            );

            _userAssociatedProjectsMock.Setup(s => s.GetUserNameFromIdAsync(It.IsAny<int>()))
                                       .ReturnsAsync("TestAdmin");

            _portalServiceMock.Setup(p => p.GridPreferenceFetcher(It.IsAny<int>()))
                                .ReturnsAsync((true, new Dictionary<string, GridMeta>
                                 {
                { "UserId",      new GridMeta { Width="120px",   OrderIndex=0  , Visible=true   } },
                { "UserName",      new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
                { "Password",   new GridMeta { Width = "120px", OrderIndex = 2, Visible = true } },
                { "CreatedBy",     new GridMeta { Width = "120px", OrderIndex = 3, Visible = true } },
                { "CreatedDate",  new GridMeta { Width = "120px", OrderIndex = 4, Visible = true } },
                { "ModifiedBy",    new GridMeta { Width = "120px", OrderIndex = 5, Visible = true } },
                { "ModifiedDate",    new GridMeta { Width = "120px", OrderIndex = 6, Visible = true } },
                { "IsActive",      new GridMeta { Width = "120px", OrderIndex = 7, Visible = true } }
        }));

            _userManagerMock.Setup(u => u.GetAllUsers())
                            .ReturnsAsync(new List<UserDetailDto> {
                                new UserDetailDto { UserId = 1, UserName = "test", Password = "pwd", IsActive = true }
                            });
        }

        private UserSession CreateTestSession() => new()
        {
            UserId = 123,
            UserName = "TestAdmin"
        };

        [Test]
        public void OnInitializedAsync_LoadsSessionDataAndUsers()
        {
            var cut = RenderComponent<User_Manager>(parameters =>
                parameters.Add(p => p.SessionDetails, CreateTestSession()));
            var instance = cut.Instance;

            Assert.That(instance.SessionDetails.UserId, Is.EqualTo(123));
            Assert.That(instance.SessionDetails.UserName, Is.EqualTo("TestAdmin"));
            Assert.That(instance.UserDetails, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task AddUserSubmitHandler_AddsUserAndRefreshesList()
        {
            var cut = RenderComponent<User_Manager>(parameters =>
                parameters.Add(p => p.SessionDetails, CreateTestSession()));
            var instance = cut.Instance;

            instance.AdminAddUserForm.UserName = "newuser";
            instance.AdminAddUserForm.Password = "newpwd";

            UserDetail? capturedUser = null;

            _userManagerMock
                .Setup(m => m.AddUserToDb(It.IsAny<UserDetail>()))
                .Callback<UserDetail>(u => capturedUser = u)
                .ReturnsAsync(true);

            await cut.InvokeAsync(() => instance.AddUserSubmitHandler());

            var hashedPassword = PasswordHelper.HashPassword("newpwd");
            // Use FluentAssertions to verify capturedUser
            capturedUser.Should().NotBeNull();
            capturedUser!.UserName.Should().Be("newuser");
            capturedUser.Password.Should().Be(hashedPassword);
            capturedUser.CreatedBy.Should().Be(123);

            _userManagerMock.Invocations.Count(i => i.Method.Name == "GetAllUsers")
                .Should().Be(2, "because user list should refresh after addition");

            var modalCall = _jsMock.Invocations.FirstOrDefault(i =>
                i.Method.Name == "InvokeAsync"
                && i.Arguments[0] as string == "bootstrapInterop.hideModal"
                && i.Arguments[1] is object[] args
                && args.Length == 1
                && args[0]!.ToString() == "#AddUserModal");

            modalCall.Should().NotBeNull("because the modal should be closed after adding user");
        }


        [Test]
        public async Task HandleEdit_EnablesEditMode()
        {
            var cut = RenderComponent<User_Manager>(parameters =>
                parameters.Add(p => p.SessionDetails, CreateTestSession()));
            var instance = cut.Instance;
            var user = instance.UserDetails.First();

            await cut.InvokeAsync(() => instance.HandleEdit(user));

            Assert.That(user.IsEdit, Is.True);
            Assert.That(instance.Editing, Is.True);
        }

        [Test]
        public async Task HandleSubmit_SavesEditAndReloads()
        {
            var cut = RenderComponent<User_Manager>(parameters =>
                parameters.Add(p => p.SessionDetails, CreateTestSession()));
            var instance = cut.Instance;
            var user = instance.UserDetails.First();
            user.Password = "edited";

            _userManagerMock.Setup(m => m.EditUserInDb(user.UserId, 123, user.Password, user.IsActive))
                            .ReturnsAsync(true);

            await cut.InvokeAsync(() => instance.HandleSubmit(user));

            _userManagerMock.Verify(m => m.EditUserInDb(user.UserId, 123, user.Password, user.IsActive), Times.Once);
            _userManagerMock.Verify(m => m.GetAllUsers(), Times.Exactly(2));

            Assert.That(user.IsEdit, Is.False);
            Assert.That(instance.Editing, Is.False);
        }

        [Test]
        public async Task HandleInActive_DisablesUser()
        {
            var cut = RenderComponent<User_Manager>(parameters =>
                parameters.Add(p => p.SessionDetails, CreateTestSession()));
            var instance = cut.Instance;
            var user = instance.UserDetails.First();

            _userManagerMock.Setup(m => m.SetUserStatus(user.UserId, false)).ReturnsAsync(true);

            await cut.InvokeAsync(() => instance.HandleInActive(user));

            _userManagerMock.Verify(m => m.SetUserStatus(user.UserId, false), Times.Once);
            _userManagerMock.Verify(m => m.GetAllUsers(), Times.Exactly(2));
        }

        [Test]
        public async Task HandleActive_EnablesUser()
        {
            var cut = RenderComponent<User_Manager>(parameters =>
                parameters.Add(p => p.SessionDetails, CreateTestSession()));
            var instance = cut.Instance;
            var user = instance.UserDetails.First();

            _userManagerMock.Setup(m => m.SetUserStatus(user.UserId, true)).ReturnsAsync(true);

            await cut.InvokeAsync(() => instance.HandleActive(user));

            _userManagerMock.Verify(m => m.SetUserStatus(user.UserId, true), Times.Once);
            _userManagerMock.Verify(m => m.GetAllUsers(), Times.Exactly(2));
        }
    }
}
