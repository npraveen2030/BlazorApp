using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BlazorApp.UI.MinimalApi
{
    public class Login
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Login(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendCookies( int id , List<int> UserAssociatedRoles)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,id.ToString())
                    };

            foreach (var role in UserAssociatedRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            if (_httpContextAccessor.HttpContext != null)
            {
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties { IsPersistent = false }
                );
            }
        }
    }
}
