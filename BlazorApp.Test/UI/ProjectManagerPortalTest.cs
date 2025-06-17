using Bunit;
using Bunit.TestDoubles;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.UI.Components.Pages.Associations;
using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Common;

namespace BlazorApp.Tests.Components
{
    [TestFixture]
    public class ProjectManagerPortalTests : Bunit.TestContext
    {
        private readonly Mock<IProjectManagerPortalService> _portalServiceMock;
        private readonly Mock<IJSRuntime> _jsMock;
        private readonly TestAuthorizationContext _authContext;

        public ProjectManagerPortalTests()
        {
            _portalServiceMock = new Mock<IProjectManagerPortalService>();
            _jsMock = new Mock<IJSRuntime>();

            Services.AddSingleton(_portalServiceMock.Object);
            Services.AddSingleton(_jsMock.Object);

            _authContext = this.AddTestAuthorization();
            _authContext.SetAuthorized("123");
            _authContext.SetClaims(
                new Claim(ClaimTypes.Name, "123"),
                new Claim(ClaimTypes.Role, "12")
            );

            _portalServiceMock.Setup(s => s.GetUserByIdAsync(123)).ReturnsAsync("manager");
            _portalServiceMock.Setup(s => s.FetchAssociations(123)).ReturnsAsync(new List<UserProjectRoleAssociationDto>());
        }

        [Test]
        public void OnInitializedAsync_LoadsSessionAndAssociations()
        {
            var cut = RenderComponent<Project_Manager_Portal>();
            var instance = cut.Instance;

            Assert.That(instance.SessionDetails.UserId, Is.EqualTo(123));
            Assert.That(instance.SessionDetails.UserName, Is.EqualTo("manager"));
            Assert.That(instance.SessionDetails.UserRoles, Is.EquivalentTo(new List<string> { "12" }));
        }

        [Test]
        public async Task HandleAddAssociation_LoadsActiveUsersAndShowsModal()
        {
            var users = new List<UserProjectRoleAssociationDto>
            {
                new() { UserId = 1 },
                new() { UserId = 2 }
            };
            var associated = new List<int> { 2 };

            _portalServiceMock.Setup(s => s.GetActiveUserFromId(123)).ReturnsAsync(users);
            _portalServiceMock.Setup(s => s.GetAssociatedUsers()).ReturnsAsync(associated);

            var cut = RenderComponent<Project_Manager_Portal>();
            var instance = cut.Instance;
            instance.SessionDetails.UserId = 123;

            await cut.InvokeAsync(() => instance.HandleAddAssociation());

            Assert.That(instance.ActiveUsers.Select(u => u.UserId), Is.EquivalentTo(new[] { 1 }));
            _jsMock.Verify(js => js.InvokeAsync<object>(
                "bootstrapInterop.showModal",
                It.Is<object[]>(args => args[0]!.ToString() == "#AddAssociationModal")
            ), Times.Once);
        }

        [Test]
        public async Task UserId_BindValue_FiltersAvailableProjects()
        {
            _portalServiceMock.Setup(s => s.GetAvailableProjects(100, 12)).ReturnsAsync(new List<UserProjectRoleAssociationDto>
            {
                new() { ProjectId = 1 },
                new() { ProjectId = 2 }
            });
            _portalServiceMock.Setup(s => s.GetAssociatedProjects(100)).ReturnsAsync(new List<int> { 1 });

            var cut = RenderComponent<Project_Manager_Portal>();
            var instance = cut.Instance;
            instance.SessionDetails = new UserSession { UserId = 100, UserRoles = new List<string> { "12" } };

            instance.UserId_BindValue = 42;
            await Task.Delay(10);

            Assert.That(instance.AvailableProjects.Count, Is.EqualTo(1));
            Assert.That(instance.AvailableProjects[0].ProjectId, Is.EqualTo(2));
        }

        [Test]
        public async Task AddAssociationSubmitHandler_AddsAssociation_AndHidesModal()
        {
            var cut = RenderComponent<Project_Manager_Portal>();
            var instance = cut.Instance;
            instance.SessionDetails = new UserSession { UserId = 1 };
            instance.AddAssociationForm = new UserProjectRoleAssociationDto { UserId = 5, ProjectId = 10 };

            _portalServiceMock.Setup(s => s.AddAssociation(It.IsAny<UserProjectRoleAssociation>())).ReturnsAsync(true);
            _portalServiceMock.Setup(s => s.FetchAssociations(1)).ReturnsAsync(new List<UserProjectRoleAssociationDto>());

            await cut.InvokeAsync(() => instance.AddAssociationSubmitHandler());

            Assert.That(instance.AddAssociationForm.UserId, Is.EqualTo(0));
            Assert.That(instance.AddAssociationForm.ProjectId, Is.EqualTo(0));

            _jsMock.Verify(js => js.InvokeAsync<object>(
                "bootstrapInterop.hideModal",
                It.Is<object[]>(args => args[0]!.ToString() == "#AddAssociationModal")
            ), Times.Once);
        }

        [Test]
        public async Task DeleteAssociation_RemovesAssociation_AndRefreshesList()
        {
            var cut = RenderComponent<Project_Manager_Portal>();
            var instance = cut.Instance;
            instance.SessionDetails = new UserSession { UserId = 1 };

            var dto = new UserProjectRoleAssociationDto { UpraId = 7 };

            _portalServiceMock.Setup(s => s.DeleteAssociation(7)).ReturnsAsync(true);
            _portalServiceMock.Setup(s => s.FetchAssociations(1)).ReturnsAsync(new List<UserProjectRoleAssociationDto>());

            await cut.InvokeAsync(() => instance.DeleteAssociation(dto));

            _portalServiceMock.Verify(s => s.DeleteAssociation(7), Times.Once);
            _portalServiceMock.Verify(s => s.FetchAssociations(1), Times.Exactly(2)); // updated line
        }
    }
}
