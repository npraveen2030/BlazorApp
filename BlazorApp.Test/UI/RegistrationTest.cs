using Bunit;
using BlazorApp.UI.Components.Pages.Authentication;
using BlazorApp.BLL.Interface;
using BlazorApp.Model.Entities.AuthDB.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using BlazorApp.BLL.Utilities;

namespace BlazorApp.Test.UI
{
    public class RegistrationTests : Bunit.TestContext
    {
        private readonly Mock<IRegisterService> _registerServiceMock;

        public RegistrationTests()
        {
            _registerServiceMock = new Mock<IRegisterService>();
            Services.AddSingleton<IRegisterService>(_registerServiceMock.Object);
        }

        [Test]
        public async Task HandleRegister_ShouldCallAddUserAsyncAndNavigate()
        {
            // Arrange
            var cut = RenderComponent<Registration>();
            var instance = cut.Instance;
            var navManager = Services.GetRequiredService<NavigationManager>();

            instance.RegFormDetails.UserName = "testuser";
            instance.RegFormDetails.Password = "securePassword123";

            // Act
            await cut.InvokeAsync(() => instance.HandleRegister());

            // Assert: Service was called
            _registerServiceMock.Verify(service =>
                service.AddUserAsync(It.Is<UserDetail>(u =>
                    u.UserName == "testuser" &&
                    PasswordHelper.VerifyPassword("securePassword123", u.Password)
                )),
                Times.Once);

            // Assert: Navigation happened
            Assert.That(navManager.Uri, Does.EndWith("/"));
        }

        [Test]
        public void ChangeTabtoSignIn_ShouldTriggerEventCallback()
        {
            // Arrange
            string? tabChangedTo = null;
            var cut = RenderComponent<Registration>(parameters => parameters
                .Add(p => p.ChangeTab, EventCallback.Factory.Create<string>(this, val => tabChangedTo = val)));

            var instance = cut.Instance;

            // Act
            instance.ChangeTabtoSignIn();

            // Assert
            Assert.That(tabChangedTo, Is.EqualTo("SignIn"));
        }
    }
}
