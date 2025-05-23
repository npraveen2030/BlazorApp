using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Components.Pages.Authorization
{
    public partial class ResetPassword:ComponentBase
    {
        [Parameter] public string? Token { get; set; }
        [Inject] private NavigationManager NavManager { get; set; } = null!;
        [Inject] private AuthDbContext Context { get; set; } = null!;
        private ResetModel resetModel = new();
        private string? token;

        protected override void OnInitialized()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var t))
            {
                token = t;
            }
        }

        private async Task HandleResetPassword()
        {
            var user = await Context.UserDetails.FirstOrDefaultAsync(u => u.ResetToken == token &&
                                                                      u.ResetTokenExpiration > DateTime.UtcNow);
            if (user != null)
            {
                user.Password = PasswordHelper.HashPassword(resetModel.NewPassword);
                user.ResetToken = null;
                user.ResetTokenExpiration = null;
                await Context.SaveChangesAsync();

                NavManager.NavigateTo("/",forceLoad:true);
            }
            else
            {
                Console.WriteLine("Reset Failed");
            }
        }

        public class ResetModel
        {
            [Required]
            public string NewPassword { get; set; } = null!;
        }
    }
}
