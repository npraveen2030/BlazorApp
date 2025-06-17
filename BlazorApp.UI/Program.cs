using BlazorApp.UI.Components;
using BlazorApp.UI.MinimalApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using BlazorApp.Model.Common;
using BlazorApp.BLL.Utilities;


namespace BlazorApp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddServerSideBlazor()
                .AddCircuitOptions(options =>
                {
                    options.DetailedErrors = true;
                });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(options =>
                            {
                                options.LoginPath = "/";
                                options.AccessDeniedPath = "/forbidden";
                                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                                options.SlidingExpiration = true;
                                options.Cookie.Name = "AuthCookie";
                            });

            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            builder.Services.AddSingleton<EmailService>();

            builder.Services.AddHttpClient("Default", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7139"); 
            });

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));

            builder.Services.RegisterServices();
            builder.Services.AddScoped<Login>();

            builder.Services.AddRadzenComponents();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAntiforgery();

            app.MapPost("/logout", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/");
            });

            app.MapPost("/login", async (HttpContext context, [FromBody] Model.Common.LoginRequest request,
                Login login) =>
            {
                var userId = request.UserId;
                var roles = request.RoleIds;

                await login.SendCookies(userId, roles);
                return Results.Ok();
            });

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
