using BlazorApp.BLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.UI.Components.Pages.Associations;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace BlazorApp.Test.UI
{
    [TestFixture]
    public class UserAssociatedProjectsTests : Bunit.TestContext
    {
        private readonly Mock<IUserAssociatedProjectsService> _mockService;
        private readonly TestAuthorizationContext _authContext;
        private readonly NavigationManager _navManager;

        public UserAssociatedProjectsTests()
        {
            // Setup mock service
            _mockService = new Mock<IUserAssociatedProjectsService>();

            _mockService.Setup(s => s.GetUserNameFromIdAsync(It.IsAny<int>()))
                        .ReturnsAsync("TestUser");

            _mockService.Setup(s => s.GetAssociatedProjects(It.IsAny<int>()))
                        .ReturnsAsync(new List<UserProjectRoleAssociationDto>
                        {
                            new UserProjectRoleAssociationDto { ProjectId = 1, ProjectName = "Project1" }
                        });

            // Register services before rendering
            Services.AddSingleton<IUserAssociatedProjectsService>(_mockService.Object);
            //Services.AddSingleton<NavigationManager, TestNavigationManager>();

            // Setup auth
            _authContext = this.AddTestAuthorization();
            _authContext.SetAuthorized("123");
            _authContext.SetClaims(
                new Claim(ClaimTypes.Name, "123"),
                new Claim(ClaimTypes.Role, "Admin")
            );

            // Get NavigationManager for assertions
            _navManager = Services.GetRequiredService<NavigationManager>();
        }

        [Test]
        public void OnInitializedAsync_LoadsProjectsAndSessionDetails()
        {
            // Render
            var cut = RenderComponent<UserAssociatedProjects>();
            var instance = cut.Instance;

            // Assertions
            Assert.That(instance.AssociatedProjects.Count, Is.EqualTo(1));
            Assert.That(instance.SessionDetails.UserId, Is.EqualTo(123));
            Assert.That(instance.SessionDetails.UserName, Is.EqualTo("TestUser"));
            Assert.That(instance.SessionDetails.UserRoles, Contains.Item("Admin"));
        }

        [Test]
        public void HandleProjectSelect_WithId21_NavigatesToPricingDetails()
        {
            var cut = RenderComponent<UserAssociatedProjects>();
            cut.Instance.HandleProjectSelect(21);

            Assert.That(_navManager.Uri, Does.Contain("/pricingdetails"));
        }

        [Test]
        public void HandleProjectSelect_WithOtherId_NavigatesToProjectPage()
        {
            var cut = RenderComponent<UserAssociatedProjects>();
            cut.Instance.HandleProjectSelect(55);

            Assert.That(_navManager.Uri, Does.Contain("/project/55"));
        }
    }
}
