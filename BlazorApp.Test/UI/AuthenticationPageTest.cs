
using BlazorApp.BLL.Interface;
using BlazorApp.UI.Components.Pages.Authentication;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using static BlazorApp.Test.UI.SignInTests;

namespace BlazorApp.Test.UI;
public class AuthenticationPageTest : Bunit.TestContext
{
    private Mock<ISigninService> _signinServiceMock;
    private Mock<IJSRuntime> _jsRuntimeMock;
    private MockNavigationManager _navManager;

    public AuthenticationPageTest()
    {
        _signinServiceMock = new Mock<ISigninService>();
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _navManager = new MockNavigationManager();

        Services.AddSingleton(_signinServiceMock.Object);
        Services.AddSingleton(_jsRuntimeMock.Object);
        Services.AddSingleton<NavigationManager>(_navManager);
    }

    [Test]
    public async Task SetActiveTab_onchange_ToRegister()
    {
        var signinServiceMock = new Mock<ISigninService>();
        Services.AddSingleton(signinServiceMock.Object);

        var cut = RenderComponent<AuthenticationPage>();
        var instance = cut.Instance;

        await cut.InvokeAsync(() => instance.SetActiveTab("Register"));

        Assert.That(instance.activeTab, Is.EqualTo("Register"));
    }
}
