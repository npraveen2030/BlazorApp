using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp5.Components
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public SignInModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }

    public partial class SignIn : ComponentBase
    {
        public SignInModel signInInstance = new SignInModel();
    }
}