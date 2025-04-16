using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp5.Components
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; }

        public RegisterModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }

    public partial class Register : ComponentBase
    {
        public RegisterModel registerInstance = new RegisterModel();

        [Parameter]
        public EventCallback<string> redirect { get; set; }

        public async Task redirectfunction()
        {
            await redirect.InvokeAsync("SignIn");
        }
    }


}