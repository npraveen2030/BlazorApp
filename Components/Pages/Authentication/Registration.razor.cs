using System.ComponentModel.DataAnnotations;
using BlazorApp.Models.Dtos;
using BlazorApp.Models.Entities;
using BlazorApp.Components.Pages.Features;

namespace BlazorApp.Components.Pages.Authentication
{
    public partial class Registration : ComponentBase
    {
        [Inject] private AuthDbContext Context { get; set; } = null!;
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        public UserDetailDto RegFormDetails { get; set; } = new();
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
                Navigation.NavigateTo("/signin", forceLoad:true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


}