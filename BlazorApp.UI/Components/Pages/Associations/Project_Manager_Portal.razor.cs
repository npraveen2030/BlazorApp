using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Entities.AuthDB.Personalization;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace BlazorApp.UI.Components.Pages.Associations
{
    public partial class Project_Manager_Portal : ComponentBase
    {
        [Inject] public IProjectManagerPortalService ProjectManagerPortalService {  get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get;set; } = null!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        public List<UserProjectRoleAssociationDto> Associations { get; set;} = null!;
        public List<UserProjectRoleAssociationDto> ActiveUsers { get; set; } = null!;
        public List<UserProjectRoleAssociationDto> AvailableProjects { get; set; } = null!;
        public UserProjectRoleAssociationDto AddAssociationForm { get; set; } = new();
        public UserSession SessionDetails { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                SessionDetails.UserId = Convert.ToInt32(user.Identity.Name);
                SessionDetails.UserName = await ProjectManagerPortalService.GetUserByIdAsync(SessionDetails.UserId);
                SessionDetails.UserRoles = user.Claims
                                                .Where(c => c.Type == ClaimTypes.Role)
                                                .Select(c => c.Value)
                                                .ToList();
            }

            Associations = await ProjectManagerPortalService.FetchAssociations(SessionDetails.UserId);
        }

        public List<int> PLAssociatedUsers { get;set; } = null!;

        public async Task HandleAddAssociation()
        {
            ActiveUsers = await ProjectManagerPortalService.GetActiveUserFromId(SessionDetails.UserId);
            PLAssociatedUsers = await ProjectManagerPortalService.GetAssociatedUsers();

            ActiveUsers = ActiveUsers.Where(a => !PLAssociatedUsers.Contains(a.UserId)).ToList();

            await JS.InvokeVoidAsync("bootstrapInterop.showModal", "#AddAssociationModal");
        }

        public int UserId_BindValue
        {
            get => AddAssociationForm.UserId;
            set
            {
                if (AddAssociationForm.UserId != value)
                {
                    AddAssociationForm.UserId = value;
                    if (value != 0)
                    {
                        _ = SelectedUserHandler(value);
                    }

                }
            }
        }
        public List<int> PLAssociatedProjects { get;set; } = null!;

        public async Task SelectedUserHandler(int value)
        {
            AvailableProjects = await ProjectManagerPortalService.GetAvailableProjects(SessionDetails.UserId, Convert.ToInt32(SessionDetails.UserRoles[0]));
            PLAssociatedProjects = await ProjectManagerPortalService.GetAssociatedProjects(SessionDetails.UserId);
            AvailableProjects = AvailableProjects.Where(a => !PLAssociatedProjects.Contains(a.ProjectId)).ToList();
            StateHasChanged();
        }

        public async Task AddAssociationSubmitHandler()
        {
            try
            {
                var newUPRA = new UserProjectRoleAssociation
                {
                    UserId = AddAssociationForm.UserId,
                    RoleId = 12,
                    ProjectId = AddAssociationForm.ProjectId,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = SessionDetails.UserId,
                    IsActive = true
                };

                var success = await ProjectManagerPortalService.AddAssociation(newUPRA);
                if (success)
                {
                    AddAssociationForm = new();
                    await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddAssociationModal");
                    Associations = await ProjectManagerPortalService.FetchAssociations(SessionDetails.UserId);
                }  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task DeleteAssociation(UserProjectRoleAssociationDto association)
        {
            var success = await ProjectManagerPortalService.DeleteAssociation(association.UpraId);
            if (success)
                Associations = await ProjectManagerPortalService.FetchAssociations(SessionDetails.UserId);
        }
    }
}
