using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorApp.Tests.Utilities
{
    public class TestAuthenticationStateProvider : AuthenticationStateProvider
    {
        private Task<AuthenticationState> _authStateTask;

        public TestAuthenticationStateProvider()
        {
            var identity = new ClaimsIdentity(); 
            _authStateTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        public void SetAuthenticationState(Task<AuthenticationState> authStateTask)
        {
            _authStateTask = authStateTask;
            NotifyAuthenticationStateChanged(_authStateTask);
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authStateTask;
    }
}
