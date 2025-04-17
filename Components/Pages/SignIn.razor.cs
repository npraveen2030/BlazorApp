using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using Authorization_Manager.Models;
using Authorization_Manager.Data;
using Microsoft.EntityFrameworkCore;
using Authorization_Manager.Session;

namespace Authorization_Manager.Components.Pages
{
    // Model to hold the sign-in data
    public class SignInModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

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
        [Inject]
        private AppDbContext _context { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        [Inject]
        private UserSession Session { get; set; } = null!;

        internal async Task HandleSignin()
        {
            var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Username == signInInstance.Username);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            else if  (user.Password != signInInstance.Password)
            {
                Console.WriteLine("Invalid password.");
                return;
            }

            else
            {
                Session.UserName = user.Username;
                Session.UserRoleId = user.RoleId;
                Session.IsAuthenticated = true;

                Navigation.NavigateTo("/manager");
            }
                
        }
    }
}