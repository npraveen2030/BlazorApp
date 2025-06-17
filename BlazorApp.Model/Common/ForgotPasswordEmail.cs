using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model.Common
{
    public class ForgotPasswordEmail
    {
        public string UserName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class ResetModel
    {
        [Required]
        public string NewPassword { get; set; } = null!;
    }
}
