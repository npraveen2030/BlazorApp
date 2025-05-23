using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Components.Pages.Authorization
{
    public partial class ForgotPassword:ComponentBase
    {
        [Inject] private AuthDbContext Context { get; set; } = null!;
        [Inject] private NavigationManager NavManager { get; set; } = null!;
        [Inject] private EmailService EmailService { get; set; } = null!;

        private EmailModel emailModel = new();

        private bool IsSent { get;set;} = false;

        private async Task HandleForgotPassword()
        {
            var user = await Context.UserDetails.FirstOrDefaultAsync(u => u.UserName == emailModel.UserName);
            if (user != null)
            {
                string token = Guid.NewGuid().ToString();
                user.ResetToken = token;
                user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1);
                await Context.SaveChangesAsync();

                string resetLink = NavManager.BaseUri + $"reset-password?token={token}";

                await EmailService.SendAsync(emailModel.Email, "Password Reset", $"Click to reset: {resetLink}");

                IsSent = true;
            }
        }
    }

    public class EmailModel
    {
        public string UserName { get; set; } = "";
        [Required, EmailAddress]
        public string Email { get; set; } = "";
    }
}
