using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Models.Dtos;
using BlazorApp.Components.Pages.Features;

namespace BlazorApp.Components.Pages.Authentication
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

        [Inject] private UserSession SessionDetails { get; set; } = null!;

        internal async Task HandleSignin()
        {
            var user = await Context.UserDetails
                        .FirstOrDefaultAsync(u => u.UserName == SigninFormDetails.UserName);

            if (user != null)
            {
                if (!PasswordHelper.VerifyPassword(SigninFormDetails.Password, user.Password))
                {
                    Console.WriteLine("Invalid User or Password");
                    return;
                }
                else
                {
                    SessionDetails.UserId = user.UserId;
                    SessionDetails.RoleId = await Context.UserProjectRoleAssociations
                                                           .Where(assoc => assoc.UserId == user.UserId && assoc.IsActive)
                                                           .Include(assoc => assoc.Role)
                                                           .OrderByDescending(assoc => assoc.Role.RolePriority) 
                                                           .Select(assoc => (int?)assoc.RoleId)
                                                           .FirstOrDefaultAsync() ?? 0;
                    SessionDetails.IsAuthenticated = true;
                    Navigation.NavigateTo("/associatedprojects");
                }
            }
            else
            {
                Console.WriteLine("Invalid User or Password");
                return;
            }

        }
    }
}