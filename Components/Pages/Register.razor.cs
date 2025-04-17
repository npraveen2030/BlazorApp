using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using Authorization_Manager.Models;
using Authorization_Manager.Data;

namespace Authorization_Manager.Components.Pages
{
    // Model to hold the registration data
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = "";

    }

   partial class Register : ComponentBase
    {
        // RegisterModel instance to hold the form data
        internal RegisterModel registerInstance = new();

        // Redirecting to SignIn page
        [Parameter]
        public EventCallback<string> Redirect { get; set; }

        internal async Task RedirectFunction()
        {
            await Redirect.InvokeAsync("SignIn");
        }

        // Method to handle form submission

        [Inject]
        private AppDbContext _context { get; set; } = null!;
        internal async Task HandleRegister()
        {
            try
            {
                var newUser = new User
                {
                    Username = registerInstance.UserName,
                    Password = registerInstance.Password,
                    LastLogin = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                await RedirectFunction();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


}