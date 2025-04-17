using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Authorization_Manager.Components.Pages
{
    // Model to hold the sign-in data
    public class SignInModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";

        public bool RememberMe { get; set; } = false;

    }

    public partial class SignIn : ComponentBase
    {
        // SignInModel instance to hold the form data
        public SignInModel signInInstance = new SignInModel();

        // Redirecting to Register page
        [Parameter]
        public EventCallback<string> redirect { get; set; }

        public async Task redirectfunction()
        {
            await redirect.InvokeAsync("Register");
        }

        // Method to handle form submission
    }
}