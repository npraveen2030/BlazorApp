using BlazorApp.BLL.Interface;
using BlazorApp.Model.Common;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorApp.UI.Components.Pages.Associations
{
    public partial class UserAssociatedProjects : ComponentBase
    {
        [Inject] public IUserAssociatedProjectsService UserAssociatedProjectsService { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;

        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        public List<UserProjectRoleAssociationDto> AssociatedProjects { get; set; } = new();
        
        public UserSession SessionDetails { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                SessionDetails.UserId = Convert.ToInt32(user.Identity.Name);
                SessionDetails.UserName = await UserAssociatedProjectsService.GetUserNameFromIdAsync(SessionDetails.UserId);
                SessionDetails.UserRoles =  user.Claims
                                                .Where(c => c.Type == ClaimTypes.Role)
                                                .Select(c => c.Value)
                                                .ToList();

            }

            AssociatedProjects = await UserAssociatedProjectsService.GetAssociatedProjects(SessionDetails.UserId);
        }

        public void HandleProjectSelect(int id)
        {
            if (id == 20)
            {
                NavManager.NavigateTo("/pricingdetails",forceLoad:true);
            }
            else
            {
                NavManager.NavigateTo($"/project/{id}");
            }
        }
    }
}
