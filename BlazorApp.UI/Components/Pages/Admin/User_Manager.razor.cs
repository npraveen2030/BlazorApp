using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Service;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.UI.Components.Pages.Admin
{
    public partial class User_Manager : ComponentBase
    {
        [Inject] public IUserManagerService UserManagerService { get; set; } = null!;
        [Inject] public IProjectLeadPortalService ProjectLeadPortalService { get; set; } = null!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        [Inject] public IUserAssociatedProjectsService UserAssociatedProjectsService { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;
        [Parameter] public UserSession SessionDetails { get;set;} = null!;

        public UserDetailDto AdminAddUserForm { get; set; } = new();
        public List<UserDetailDto> UserDetails { get; set; } = null!;
        public GridPreferencesSaver<UserDetailDto> gridPreferencesSaver { get; set; } = null!;

        public List<int> pageSizeOptions = new() { 5, 8, 10 };

        public Dictionary<string, GridMeta> GridState { get; set; } = new()
            {
                { "UserId",      new GridMeta { Width="120px",   OrderIndex=0  , Visible=true   } },
                { "UserName",      new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
                { "Password",   new GridMeta { Width = "120px", OrderIndex = 2, Visible = true } },
                { "CreatedBy",     new GridMeta { Width = "120px", OrderIndex = 3, Visible = true } },
                { "CreatedDate",  new GridMeta { Width = "120px", OrderIndex = 4, Visible = true } },
                { "ModifiedBy",    new GridMeta { Width = "120px", OrderIndex = 5, Visible = true } },
                { "ModifiedDate",    new GridMeta { Width = "120px", OrderIndex = 6, Visible = true } },
                { "IsActive",      new GridMeta { Width = "120px", OrderIndex = 7, Visible = true } }
        };

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

            var FetchedGridState = await ProjectLeadPortalService.GridPreferenceFetcher(SessionDetails.UserId);

            if (FetchedGridState.Item1)
                GridState = FetchedGridState.Item2;

            gridPreferencesSaver = new GridPreferencesSaver<UserDetailDto>(GridState);

            UserDetails = await UserManagerService.GetAllUsers();
        }

        // Adding a User
        public async Task AddUserSubmitHandler()
        {
            try
            {
                var newUser = new UserDetail
                {
                    UserName = AdminAddUserForm.UserName,
                    Password = PasswordHelper.HashPassword(AdminAddUserForm.Password),
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = 1,
                    IsActive = true
                };

                var success = await UserManagerService.AddUserToDb(newUser);
                if (success)
                {
                    AdminAddUserForm = new();
                    await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddUserModal");
                    UserDetails = await UserManagerService.GetAllUsers();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Editing a user
        public bool Editing { get; set; } = false;

        public async Task HandleEdit(UserDetailDto user)
        {
            if (!Editing)
            {
                user.IsEdit = true;
                Editing = true;
            }
            else
            {
                await JS.InvokeVoidAsync("alert", "You can edit only one User at a time.");
            }
        }

        public async Task HandleSubmit(UserDetailDto user)
        {
            user.IsEdit = false;
            Editing = false;

            var success = await UserManagerService.EditUserInDb(user.UserId, SessionDetails.UserId, user.Password, user.IsActive);

            UserDetails = await UserManagerService.GetAllUsers();
        }

        public void HandleCancel(UserDetailDto user)
        {
            user.IsEdit = false;
            Editing = false;
        }

        // Toggling a user

        public UserDetailDto DeleteConfirmationuser { get; set; } = new();

        public async Task HandleInActive(UserDetailDto user)
        {
            var success = await UserManagerService.SetUserStatus(user.UserId,false);
            if (success)
            {
                await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#DeleteConfirmationModal");
                UserDetails = await UserManagerService.GetAllUsers();
            }  
        }

        public async Task HandleActive(UserDetailDto user)
        {

            var success = await UserManagerService.SetUserStatus(user.UserId, true);
            if (success)
            {
                UserDetails = await UserManagerService.GetAllUsers();
            }

            UserDetails = await UserManagerService.GetAllUsers();
        }
    }
}
