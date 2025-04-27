using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Models.Dtos;

namespace BlazorApp.Components.Pages
{
    public partial class SignIn : ComponentBase
    {
        // SignInModel instance to hold the form data
        public UserDetailDto SigninFormDetails = new();

        // Redirecting to Register page
        [Parameter]
        public EventCallback<string> redirect { get; set; }

        public async Task redirectfunction()
        {
            await redirect.InvokeAsync("Register");
        }

         //Method to handle form submission
        [Inject]
        private AuthDbContext Context { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        //[Inject]
        //private UserSession Session { get; set; } = null!;

        internal async Task HandleSignin()
        {
            var user = await Context.UserDetails
                        .FirstOrDefaultAsync(u => u.UserName == SigninFormDetails.UserName);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            else if  (user.Password != SigninFormDetails.Password)
            {
                Console.WriteLine("Invalid password.");
                return;
            }

            else
            {
                Navigation.NavigateTo("/manager");
            }
                
        }
    }
}