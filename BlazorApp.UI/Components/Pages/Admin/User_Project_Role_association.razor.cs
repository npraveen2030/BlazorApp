using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.UI.Components.Pages.Admin
{
    public partial class User_Project_Role_Association : ComponentBase
    {
        [Inject] public IUserProjectRoleAssociationService UserProjectRoleAssociationService { get; set; } = null!;
        [Inject] public IUserAssociatedProjectsService UserAssociatedProjectsService { get; set; } = null!;
        [Inject] public IProjectLeadPortalService ProjectLeadPortalService { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        [Parameter] public UserSession SessionDetails { get; set; } = null!;

        public UserProjectRoleAssociationDto Upraform { get; set; } = new();
        public List<UserProjectRoleAssociationDto> UPRAs { get; set; } = null!;
        public GridPreferencesSaver<UserProjectRoleAssociationDto> gridPreferencesSaver { get; set; } = null!;

        public List<int> pageSizeOptions = new() { 5, 8, 10 };

        public Dictionary<string, GridMeta> GridState { get; set; } = new()
            {
                { "UserName",      new GridMeta { Width="120px",   OrderIndex=0  , Visible=true   } },
                { "RoleName",      new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
                { "ProjectName",      new GridMeta { Width = "120px", OrderIndex = 2, Visible = true } },
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

            gridPreferencesSaver = new GridPreferencesSaver<UserProjectRoleAssociationDto>(GridState);

            UPRAs = await UserProjectRoleAssociationService.GetAllUpras();
        }

        // Adding a UPRA

        public List<UserProjectRoleAssociationDto> activeUsers { get; set; } = null!;

        public async Task HandleAddUPRAClick()
        {
            activeUsers = await UserProjectRoleAssociationService.GetActiveUsers();
            await JS.InvokeVoidAsync("bootstrapInterop.showModal", "#AddUpraModal");
        }

        public int UserId_bind
        {
            get => Upraform.UserId;
            set
            {
                if (Upraform.UserId != value)
                {
                    Upraform.UserId = value;
                    if (value != 0)
                    {
                        _ = SelectedUserHandler(value);
                    }

                }
            }
        }

        public List<UserProjectRoleAssociationDto> activeProjects { get; set; } = null!;
        public List<int> UserAssociatedProjects { get; set; } = null!;
        public List<UserProjectRoleAssociationDto> inactiveProjects { get; set; } = null!;

        public async Task SelectedUserHandler(int value)
        {

            activeProjects = await UserProjectRoleAssociationService.GetActiveProjects();

            UserAssociatedProjects = await UserProjectRoleAssociationService.GetAssociatedProjects();

            inactiveProjects = activeProjects.Where(a => !UserAssociatedProjects.Contains(a.ProjectId)).ToList();
            StateHasChanged();
        }

        public async Task AddUpraSubmitHandler()
        {
            try
            {
                var newUPRA = new UserProjectRoleAssociation
                {
                    UserId = Upraform.UserId,
                    RoleId = 11,
                    ProjectId = Upraform.ProjectId,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = 1,
                    IsActive = true
                };

                var success = await UserProjectRoleAssociationService.AddUpraToDb(newUPRA);
                if (success)
                {
                    Upraform = new();
                    await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddUpraModal");
                    UPRAs = await UserProjectRoleAssociationService.GetAllUpras();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Toggling a upra

        public UserProjectRoleAssociationDto DeleteConfirmationupra { get; set; } = new();

        public async Task HandleInActive(UserProjectRoleAssociationDto upra)
        {
            var success = await UserProjectRoleAssociationService.SetUpraStatus(upra.UpraId, false);
            if (success)
            {
                await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#DeleteConfirmationModal");
                UPRAs = await UserProjectRoleAssociationService.GetAllUpras();
            }
        }
    }
}
