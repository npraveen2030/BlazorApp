using Bunit;
using Bunit.TestDoubles;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using BlazorApp.UI.Components.Pages.Admin;
using BlazorApp.BLL.Interface;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.CustomModels.Personalization;
using BlazorApp.Model.Common;

namespace BlazorApp.Test.UI
{
    [TestFixture]
    public class UserProjectRoleAssociationTests : Bunit.TestContext
    {
        private readonly Mock<IUserProjectRoleAssociationService> _upraServiceMock;
        private readonly Mock<IUserAssociatedProjectsService> _userServiceMock;
        private readonly Mock<IProjectLeadPortalService> _portalServiceMock;
        private readonly Mock<IJSRuntime> _jsMock;
        private readonly TestAuthorizationContext _authContext;

        public UserProjectRoleAssociationTests()
        {
            _upraServiceMock = new Mock<IUserProjectRoleAssociationService>();
            _userServiceMock = new Mock<IUserAssociatedProjectsService>();
            _portalServiceMock = new Mock<IProjectLeadPortalService>();
            _jsMock = new Mock<IJSRuntime>();

            Services.AddSingleton(_upraServiceMock.Object);
            Services.AddSingleton(_userServiceMock.Object);
            Services.AddSingleton(_portalServiceMock.Object);
            Services.AddSingleton(_jsMock.Object);

            _authContext = this.AddTestAuthorization();
            _authContext.SetAuthorized("123");
            _authContext.SetClaims(
                new Claim(ClaimTypes.Name, "123"),
                new Claim(ClaimTypes.Role, "Admin")
            );

            _userServiceMock.Setup(s => s.GetUserNameFromIdAsync(It.IsAny<int>())).ReturnsAsync("TestUser");
            _upraServiceMock.Setup(s => s.GetAllUpras()).ReturnsAsync(new List<UserProjectRoleAssociationDto>());
            _portalServiceMock
                            .Setup(p => p.GridPreferenceFetcher(It.IsAny<int>()))
                            .ReturnsAsync((true, new Dictionary<string, GridMeta>
                            {
                { "UserName",      new GridMeta { Width="120px",   OrderIndex=0  , Visible=true   } },
                { "RoleName",      new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
                { "ProjectName",      new GridMeta { Width = "120px", OrderIndex = 2, Visible = true } },
                { "CreatedBy",     new GridMeta { Width = "120px", OrderIndex = 3, Visible = true } },
                { "CreatedDate",  new GridMeta { Width = "120px", OrderIndex = 4, Visible = true } },
                { "ModifiedBy",    new GridMeta { Width = "120px", OrderIndex = 5, Visible = true } },
                { "ModifiedDate",    new GridMeta { Width = "120px", OrderIndex = 6, Visible = true } },
                { "IsActive",      new GridMeta { Width = "120px", OrderIndex = 7, Visible = true } }
        }));
        }

        [Test]
        public void OnInitializedAsync_LoadsSessionDetailsAndUPRAs()
        {
            var cut = RenderComponent<User_Project_Role_Association>(parameters => parameters
                .Add(p => p.SessionDetails, new UserSession()));

            var instance = cut.Instance;

            Assert.That(instance.SessionDetails.UserId, Is.EqualTo(123));
            Assert.That(instance.SessionDetails.UserName, Is.EqualTo("TestUser"));
            Assert.That(instance.UPRAs, Is.Not.Null);
            Assert.That(instance.gridPreferencesSaver, Is.Not.Null);
        }

        [Test]
        public async Task HandleAddUPRAClick_LoadsActiveUsers_AndShowsModal()
        {
            _upraServiceMock.Setup(s => s.GetActiveUsers())
                .ReturnsAsync(new List<UserProjectRoleAssociationDto>());

            var cut = RenderComponent<User_Project_Role_Association>(parameters => parameters
                .Add(p => p.SessionDetails, new UserSession()));

            var instance = cut.Instance;

            await cut.InvokeAsync(() => instance.HandleAddUPRAClick());

            _upraServiceMock.Verify(s => s.GetActiveUsers(), Times.Once);
            _jsMock.Verify(js => js.InvokeVoidAsync(
                        "bootstrapInterop.showModal",
                        It.IsAny<object[]>()
                    ), Times.Once);
        }

        [Test]
        public async Task SelectedUserHandler_LoadsInactiveProjects()
        {
            var activeProjects = new List<UserProjectRoleAssociationDto>
            {
                new UserProjectRoleAssociationDto { ProjectId = 1 },
                new UserProjectRoleAssociationDto { ProjectId = 2 }
            };

            _upraServiceMock.Setup(s => s.GetActiveProjects())
                .ReturnsAsync(activeProjects);

            _upraServiceMock.Setup(s => s.GetAssociatedProjects())
                .ReturnsAsync(new List<int> { 1 });

            var cut = RenderComponent<User_Project_Role_Association>(parameters => parameters
                .Add(p => p.SessionDetails, new UserSession()));

            var instance = cut.Instance;

            await cut.InvokeAsync(() => instance.SelectedUserHandler(5));

            Assert.That(instance.inactiveProjects.Count, Is.EqualTo(1));
            Assert.That(instance.inactiveProjects[0].ProjectId, Is.EqualTo(2));
        }

        [Test]
        public async Task AddUpraSubmitHandler_AddsAndReloadsUPRAs()
        {
            var upra = new UserProjectRoleAssociation
            {
                UserId = 5,
                ProjectId = 10
            };

            _upraServiceMock.Setup(s => s.AddUpraToDb(It.IsAny<UserProjectRoleAssociation>()))
                .ReturnsAsync(true);

            _upraServiceMock.Setup(s => s.GetAllUpras())
                .ReturnsAsync(new List<UserProjectRoleAssociationDto>());

            var cut = RenderComponent<User_Project_Role_Association>(parameters => parameters
                .Add(p => p.SessionDetails, new UserSession()));

            var instance = cut.Instance;
            instance.Upraform.UserId = upra.UserId;
            instance.Upraform.ProjectId = upra.ProjectId;

            await cut.InvokeAsync(() => instance.AddUpraSubmitHandler());

            _upraServiceMock.Verify(s => s.AddUpraToDb(It.IsAny<UserProjectRoleAssociation>()), Times.Once);
            _upraServiceMock.Verify(s => s.GetAllUpras(), Times.Exactly(2)); // once on init, once on submit

            _jsMock.Verify(js => js.InvokeVoidAsync(
                "bootstrapInterop.hideModal",
                It.Is<object[]>(args => args[0]!.ToString() == "#AddUpraModal")
            ), Times.Once);
        }

        [Test]
        public async Task HandleInActive_CallsSetStatusAndReloadsUPRAs()
        {
            _upraServiceMock.Setup(s => s.SetUpraStatus(It.IsAny<int>(), false)).ReturnsAsync(true);
            _upraServiceMock.Setup(s => s.GetAllUpras()).ReturnsAsync(new List<UserProjectRoleAssociationDto>());

            var cut = RenderComponent<User_Project_Role_Association>(parameters => parameters
                .Add(p => p.SessionDetails, new UserSession()));

            var instance = cut.Instance;

            var dto = new UserProjectRoleAssociationDto { UpraId = 100 };

            await cut.InvokeAsync(() => instance.HandleInActive(dto));

            _upraServiceMock.Verify(s => s.SetUpraStatus(100, false), Times.Once);

            _jsMock.Verify(js => js.InvokeVoidAsync(
                "bootstrapInterop.hideModal",
                It.Is<object[]>(args => args[0]!.ToString() == "#DeleteConfirmationModal")
            ), Times.Once);
        }
    }
}
