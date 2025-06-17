using Bunit;
using Bunit.TestDoubles;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.UI.Components.Pages.Associations;
using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.BLL.Utilities;
using Microsoft.JSInterop;
using BlazorApp.Model.CustomModels.Personalization;

namespace BlazorApp.Test.UI
{
    [TestFixture]
    public class ProjectLeadPortalTests : Bunit.TestContext
    {
        private readonly Mock<IUserAssociatedProjectsService> _userServiceMock;
        private readonly Mock<IProjectLeadPortalService> _portalServiceMock;
        private readonly Mock<IJSRuntime> _jsMock;
        private readonly TestAuthorizationContext _authContext;

        public ProjectLeadPortalTests()
        {
            _userServiceMock = new Mock<IUserAssociatedProjectsService>();
            _portalServiceMock = new Mock<IProjectLeadPortalService>();
            _jsMock = new Mock<IJSRuntime>();

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

            _portalServiceMock.Setup(p => p.GridPreferenceFetcher(It.IsAny<int>()))
                              .ReturnsAsync((true, new Dictionary<string, GridMeta>()));

            _portalServiceMock.Setup(p => p.FetchUserDetailsFromIdAndRole(It.IsAny<int>(), 12))
                              .ReturnsAsync((true, new UserProjectRoleAssociationDto { ProjectId = 1 }));

            _portalServiceMock.Setup(p => p.FetchAssociation(It.IsAny<int>()))
                              .ReturnsAsync(new List<UserProjectRoleAssociationDto>
                              {
                                  new UserProjectRoleAssociationDto { ProjectId = 1, UserId = 5 }
                              });

            _portalServiceMock.Setup(p => p.GetActiveUsersFromProjectId(It.IsAny<int>()))
                              .ReturnsAsync(new List<UserProjectRoleAssociationDto>());

            _portalServiceMock.Setup(p => p.GetAvailableRoles())
                              .ReturnsAsync(new List<UserProjectRoleAssociationDto>());

            _portalServiceMock.Setup(p => p.AddAssociation(It.IsAny<UserProjectRoleAssociationDto>()))
                              .ReturnsAsync(true);

            _portalServiceMock.Setup(p => p.DeleteAssociation(It.IsAny<int>(), It.IsAny<int>()))
                              .ReturnsAsync(true);

            _portalServiceMock.Setup(p => p.GridPreferenceSaver(It.IsAny<Dictionary<string, GridMeta>>(), It.IsAny<int>(), 2, 2))
                              .ReturnsAsync(true);
        }

        [Test]
        public void OnInitializedAsync_LoadsSessionData()
        {
            var cut = RenderComponent<Project_Lead_Portal>();
            var instance = cut.Instance;

            Assert.That(instance.SessionDetails.UserId, Is.EqualTo(123));
            Assert.That(instance.SessionDetails.UserName, Is.EqualTo("TestUser"));
            Assert.That(instance.PLDetails.ProjectId, Is.EqualTo(1));
            Assert.That(instance.Associations.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task HandleAddAssociation_LoadsActiveUsersAndShowsModal()
        {
            var cut = RenderComponent<Project_Lead_Portal>();
            var instance = cut.Instance;

            await cut.InvokeAsync(() => instance.HandleAddAssociation());

            _portalServiceMock.Verify(p => p.GetActiveUsersFromProjectId(1), Times.Once);

            _jsMock.Verify(js => js.InvokeAsync<object>(
                "bootstrapInterop.showModal",
                It.Is<object[]>(args => args[0]!.ToString() == "#AddAssociationModal")
            ), Times.Once);
        }

        [Test]
        public async Task AddAssociationSubmitHandler_AddsAndRefreshesAssociations()
        {
            var cut = RenderComponent<Project_Lead_Portal>();
            var instance = cut.Instance;

            instance.AddAssociationForm.UserId = 5;
            instance.AddAssociationForm.ProjectId = 1;

            await cut.InvokeAsync(() => instance.AddAssociationSubmitHandler());

            _portalServiceMock.Verify(p => p.AddAssociation(It.IsAny<UserProjectRoleAssociationDto>()), Times.Once);
            _portalServiceMock.Verify(p => p.FetchAssociation(1), Times.Exactly(2)); // once in init, once in submit

            _jsMock.Verify(js => js.InvokeAsync<object>(
                "bootstrapInterop.hideModal",
                It.Is<object[]>(args => args[0]!.ToString() == "#AddAssociationModal")
            ), Times.Once);
        }

        [Test]
        public async Task DeleteAssociation_RemovesAndRefreshes()
        {
            var cut = RenderComponent<Project_Lead_Portal>();
            var instance = cut.Instance;

            var dto = new UserProjectRoleAssociationDto { UpraId = 123 };

            await cut.InvokeAsync(() => instance.DeleteAssociation(dto));

            _portalServiceMock.Verify(p => p.DeleteAssociation(123, 123), Times.Once);
            _portalServiceMock.Verify(p => p.FetchAssociation(1), Times.AtLeast(1));
        }

        [Test]
        public async Task SavePreferencesHandler_SavesAndShowsToast()
        {
            var cut = RenderComponent<Project_Lead_Portal>();
            var instance = cut.Instance;

            await cut.InvokeAsync(() => instance.SavePreferencesHandler());

            _portalServiceMock.Verify(p => p.GridPreferenceSaver(It.IsAny<Dictionary<string, GridMeta>>(), 123, 2, 2), Times.Once);

            _jsMock.Verify(js => js.InvokeAsync<object>(
                "bootstrapInterop.showToast",
                It.Is<object[]>(args => args[0]!.ToString() == "savepreferenestoast")
            ), Times.Once);
        }
    }
}