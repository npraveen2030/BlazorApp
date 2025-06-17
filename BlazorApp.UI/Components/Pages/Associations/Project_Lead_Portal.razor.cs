using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Entities.AuthDB.Personalization;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace BlazorApp.UI.Components.Pages.Associations
{
    public partial class Project_Lead_Portal : ComponentBase
    {
        [Inject] public IUserAssociatedProjectsService UserAssociatedProjectsService { get; set; } = null!;
        [Inject] public IProjectLeadPortalService ProjectLeadPortalService { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        public List<UserProjectRoleAssociationDto> Associations { get; set; } = null!;
        public List<UserProjectRoleAssociationDto> ActiveUsers { get; set; } = null!;
        public List<int> ProjectAssociatedAuthorities { get; set; } = null!;
        public List<UserProjectRoleAssociationDto> AvailableRoles { get; set; } = null!;
        public UserProjectRoleAssociationDto PLDetails { get;set; } = new();
        public UserProjectRoleAssociationDto AddAssociationForm { get; set; } = new();

        public List<int> pageSizeOptions = new() { 5, 8, 10 };

        public Dictionary<string,GridMeta> GridState { get; set; } = new()
            {
                { "UserName",      new GridMeta { Width="120px",   OrderIndex=0  , Visible=true   } },
                { "RoleName",      new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
                { "CreatedDate",   new GridMeta { Width = "120px", OrderIndex = 2, Visible = true } },
                { "CreatedBy",     new GridMeta { Width = "120px", OrderIndex = 3, Visible = true } },
                { "ModifiedDate",  new GridMeta { Width = "120px", OrderIndex = 4, Visible = true } },
                { "ModifiedBy",    new GridMeta { Width = "120px", OrderIndex = 5, Visible = true } },
                { "IsActive",      new GridMeta { Width = "120px", OrderIndex = 6, Visible = true } }
            };

        public GridPreferencesSaver<UserProjectRoleAssociationDto> gridPreferencesSaver { get; set; } = null!;
        public UserSession SessionDetails { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            try
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

                var success = await ProjectLeadPortalService.FetchUserDetailsFromIdAndRole(SessionDetails.UserId,12);
                if (success.Item1)
                    PLDetails = success.Item2;

                AddAssociationForm.ProjectId = PLDetails.ProjectId;

                Associations = await ProjectLeadPortalService.FetchAssociation(PLDetails.ProjectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during OnInitializedAsync: " + ex.Message);
            }
        }

        public async Task HandleAddAssociation()
        {
            ActiveUsers = await ProjectLeadPortalService.GetActiveUsersFromProjectId(PLDetails.ProjectId);

            await JS.InvokeVoidAsync("bootstrapInterop.showModal", "#AddAssociationModal");
        }

        public async Task HideModal()
        {
            // Resetting Form
            AddAssociationForm = new UserProjectRoleAssociationDto() { ProjectId = PLDetails.ProjectId };

            await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddAssociationModal");
        }

        public async Task SelectedUserHandler(int userid)
        {
            AddAssociationForm.UserId = userid;
            AvailableRoles = await ProjectLeadPortalService.GetAvailableRoles();
            StateHasChanged();
        }

        public async Task AddAssociationSubmitHandler()
        {
            try
            {
                AddAssociationForm.CreatedBy = Convert.ToString(SessionDetails.UserId);

                var success = await ProjectLeadPortalService.AddAssociation(AddAssociationForm);
                if (success)
                {
                    AddAssociationForm = new UserProjectRoleAssociationDto() { ProjectId = PLDetails.ProjectId };
                    await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddAssociationModal");
                    Associations = await ProjectLeadPortalService.FetchAssociation(PLDetails.ProjectId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task DeleteAssociation(UserProjectRoleAssociationDto association)
        {
            var success = await ProjectLeadPortalService.DeleteAssociation(association.UpraId, SessionDetails.UserId);
            Associations = await ProjectLeadPortalService.FetchAssociation(PLDetails.ProjectId);
        }

        public async Task SavePreferencesHandler()
        {
            try
            {
                var success = await ProjectLeadPortalService.GridPreferenceSaver(GridState, SessionDetails.UserId,2,2);
                if (success)
                    await JS.InvokeVoidAsync("bootstrapInterop.showToast", "savepreferenestoast");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
