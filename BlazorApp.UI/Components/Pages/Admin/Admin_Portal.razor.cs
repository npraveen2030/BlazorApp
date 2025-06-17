
using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Service;

namespace BlazorApp.UI.Components.Pages.Admin
{
    public partial class Admin_Portal : ComponentBase
    {
        [Inject] public IAdminPortalService AdminPortalService { get; set; } = null!;
        [Inject] public IUserAssociatedProjectsService UserAssociatedProjectsService { get; set; } = null!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        private int ActiveTab { get;set;}
        public UserSession SessionDetails { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                SessionDetails.UserId = Convert.ToInt32(user.Identity.Name);
                SessionDetails.UserName = await UserAssociatedProjectsService.GetUserNameFromIdAsync(SessionDetails.UserId);
                SessionDetails.UserRoles = user.Claims
                                            .Where(c => c.Type == ClaimTypes.Role)
                                            .Select(c => c.Value)
                                            .ToList();

            }

            var TabId = await AdminPortalService.GetUserPreferenceTabById(SessionDetails.UserId);
            if (TabId != 0)
            {
                ActiveTab = TabId;
            }
        }

        private async Task SavePreferenceHandler()
        {
            var success = await AdminPortalService.SaveUserPreferences(ActiveTab, SessionDetails.UserId);
        }

    }
}
