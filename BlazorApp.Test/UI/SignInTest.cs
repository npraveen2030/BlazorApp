using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.UI.Components.Pages.Authentication;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace BlazorApp.Test.UI;
public class SignInTests : Bunit.TestContext
{
    private Mock<ISigninService> _signinServiceMock;
    private Mock<IJSRuntime> _jsRuntimeMock;
    private MockNavigationManager _navManager;

    public SignInTests() 
    {
        _signinServiceMock = new Mock<ISigninService>();
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _navManager = new MockNavigationManager();

        Services.AddSingleton(_signinServiceMock.Object);
        Services.AddSingleton(_jsRuntimeMock.Object);
        Services.AddSingleton<NavigationManager>(_navManager);
    }

    [Test]
    public async Task HandleSignin_WithInvalidUser_ShowsValidationError()
    {
        _signinServiceMock.Setup(s => s.GetUserByNameAsync("invaliduser"))
            .ReturnsAsync((UserDetail?)null);

        var cut = RenderComponent<SignIn>();
        var instance = cut.Instance;

        instance.SigninForm.UserName = "invaliduser";
        instance.SigninForm.Password = "anyPassword";

        await cut.InvokeAsync(() => instance.HandleSignin());

        Assert.That(instance._editContext.GetValidationMessages(),
            Does.Contain("Invalid Username or Password"));
    }

    [Test]
    public async Task HandleSignin_WithWrongPassword_ShowsValidationError()
    {
        var user = new UserDetail
        {
            UserId = 2,
            UserName = "user2",
            Password = PasswordHelper.HashPassword("correctPassword")
        };

        _signinServiceMock.Setup(s => s.GetUserByNameAsync("user2"))
            .ReturnsAsync(user);

        var cut = RenderComponent<SignIn>();
        var instance = cut.Instance;

        instance.SigninForm.UserName = "user2";
        instance.SigninForm.Password = "wrongPassword";

        await cut.InvokeAsync(() => instance.HandleSignin());

        Assert.That(instance._editContext.GetValidationMessages(),
            Does.Contain("Invalid Username or Password"));
    }

    [Test]
    public async Task HandleSignin_WithValidCredentials_NavigatesToAssociatedProjects()
    {
        var user = new UserDetail
        {
            UserId = 5,
            UserName = "user3",
            Password = PasswordHelper.HashPassword("password")
        };

        _signinServiceMock.Setup(s => s.GetUserByNameAsync("user3"))
            .ReturnsAsync(user);

        _signinServiceMock.Setup(s => s.GetUserAssociatedRolesByIdAsync(user.UserId))
            .ReturnsAsync(new List<int> { 1 });

        var cut = RenderComponent<SignIn>();
        var instance = cut.Instance;

        instance.SigninForm.UserName = "user3";
        instance.SigninForm.Password = "password";

        await cut.InvokeAsync(() => instance.HandleSignin());

        //_jsRuntimeMock.Verify(js =>
        //                    js.InvokeAsync<object>(
        //                        "bootstrapInterop.loginaA",
        //                        It.IsAny<object[]>()
        //                    ), Times.Once);
        Assert.That(_navManager.Uri, Does.Contain("/associatedprojects"));
    }

    public class MockNavigationManager : NavigationManager
    {
        public MockNavigationManager()
        {
            Initialize("http://localhost/", "http://localhost/");
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            Uri = ToAbsoluteUri(uri).ToString();
        }
    }
}
