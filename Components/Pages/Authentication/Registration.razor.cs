using System.ComponentModel.DataAnnotations;
using BlazorApp.Models.Dtos;
using BlazorApp.Models.Entities;
using BlazorApp.Components.Pages.Features;

namespace BlazorApp.Components.Pages.Authentication
{
    public partial class Registration : ComponentBase
    {
        // RegisterModel instance to hold the form data
        public UserDetailDto RegFormDetails { get; set; } = new();

        // Redirecting to SignIn page
        [Parameter]
        public EventCallback<string> Redirect { get; set; }

        internal async Task RedirectFunction()
        {
            await Redirect.InvokeAsync("SignIn");
        }

        // Method to handle form submission

        [Inject]
        private AuthDbContext Context { get; set; } = null!;

        internal async Task HandleRegister()
        {
            try
            {
                var newUser = new UserDetail
                {
                    UserName = RegFormDetails.UserName,
                    Password = PasswordHelper.HashPassword(RegFormDetails.Password),
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    IsActive = true
                };

                Context.UserDetails.Add(newUser);
                await Context.SaveChangesAsync();
                await RedirectFunction();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


}