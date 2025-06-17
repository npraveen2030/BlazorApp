using Bunit;
using Bunit.TestDoubles;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.BLL.Interface;
using BlazorApp.UI.Components.Pages.Admin;
using BlazorApp.Model.CustomModels.Core;

namespace BlazorApp.Test.UI
{
    [TestFixture]
    public class AdminPortalTests : Bunit.TestContext
    {
        private readonly Mock<IAdminPortalService> _adminPortalServiceMock;
        private readonly Mock<IUserAssociatedProjectsService> _userProjectsServiceMock;
        private readonly TestAuthorizationContext _authContext;

        public AdminPortalTests()
        {
            _adminPortalServiceMock = new Mock<IAdminPortalService>();
            _userProjectsServiceMock = new Mock<IUserAssociatedProjectsService>();

            Services.AddSingleton(_adminPortalServiceMock.Object);
            Services.AddSingleton(_userProjectsServiceMock.Object);

            _authContext = this.AddTestAuthorization();
            _authContext.SetAuthorized("99");
            _authContext.SetClaims(
                new Claim(ClaimTypes.Name, "99"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Manager")
            );

            _userProjectsServiceMock
                .Setup(s => s.GetUserNameFromIdAsync(99))
                .ReturnsAsync("TestAdmin");

            _adminPortalServiceMock
                .Setup(p => p.GetUserPreferenceTabById(99))
                .ReturnsAsync(3);

            _adminPortalServiceMock
                .Setup(p => p.SaveUserPreferences(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);
        }

        [Test]
        public void OnInitializedAsync_WhenUserIsAuthenticated_SetsSessionAndTab()
        {
            // Act
            var cut = RenderComponent<Admin_Portal>();
            var instance = cut.Instance;

            // Assert
            Assert.That(instance.SessionDetails.UserId, Is.EqualTo(99));
            Assert.That(instance.SessionDetails.UserName, Is.EqualTo("TestAdmin"));
            Assert.That(instance.SessionDetails.UserRoles, Is.EquivalentTo(new[] { "Admin", "Manager" }));
            Assert.That(GetPrivateProperty<int>(instance, "ActiveTab"), Is.EqualTo(3));
        }

        [Test]
        public async Task SavePreferenceHandler_CallsSaveUserPreferences()
        {
            // Arrange
            var cut = RenderComponent<Admin_Portal>();
            var instance = cut.Instance;

            instance.SessionDetails.UserId = 99;
            SetPrivateProperty(instance, "ActiveTab", 5);

            // Act
            await cut.InvokeAsync(() => instance.GetType()
                .GetMethod("SavePreferenceHandler", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .Invoke(instance, null));

            // Assert
            _adminPortalServiceMock.Verify(p => p.SaveUserPreferences(5, 99), Times.Once);
        }

        // Helper for private property (not field) access:
        private static T GetPrivateProperty<T>(object obj, string propertyName)
        {
            return (T)obj.GetType().GetProperty(propertyName,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .GetValue(obj)!;
        }

        private static void SetPrivateProperty<T>(object obj, string propertyName, T value)
        {
            obj.GetType().GetProperty(propertyName,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .SetValue(obj, value);
        }
    }
}
