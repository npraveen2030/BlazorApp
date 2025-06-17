using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Service;
using BlazorApp.BLL.Utilities;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Entities.AuthDB.Personalization;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace BlazorApp.UI.Components.Pages.Admin
{
    public partial class Project_Manager : ComponentBase
    {
        [Inject] public IProjectManagerService ProjectManagerService { get; set; } = null!;
        [Inject] public IUserAssociatedProjectsService UserAssociatedProjectsService { get; set; } = null!;
        [Inject] public IProjectLeadPortalService ProjectLeadPortalService { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        [Parameter] public UserSession SessionDetails { get; set; } = null!;

        public ProjectDto AddProjectForm { get; set; } = new();
        public List<ProjectDto> Projects { get; set; } = null!;
        public GridPreferencesSaver<ProjectDto> gridPreferencesSaver { get; set; } = null!;

        public List<int> pageSizeOptions = new() { 5, 8, 10 };

        public Dictionary<string, GridMeta> GridState { get; set; } = new()
            {
                { "ProjectId",      new GridMeta { Width="120px",   OrderIndex=0  , Visible=true   } },
                { "ProjectName",      new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
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

            gridPreferencesSaver = new GridPreferencesSaver<ProjectDto>(GridState);

            Projects = await ProjectManagerService.GetAllProjects();
        }

        // Adding a Project
        public async Task AddProjectSubmitHandler()
        {
            try
            {
                var newProject = new Project
                {
                    ProjectName = AddProjectForm.ProjectName,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = 1,
                    isActive = true
                };

                var success = await ProjectManagerService.AddProjectToDb(newProject);
                if (success)
                {
                    AddProjectForm = new();
                    await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddProjectModal");
                    Projects = await ProjectManagerService.GetAllProjects();
                }    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Editing a Project
        public bool Editing { get; set; } = false;

        public async Task HandleEdit(ProjectDto project)
        {
            if (!Editing)
            {
                project.IsEdit = true;
                Editing = true;
            }
            else
            {
                await JS.InvokeVoidAsync("alert", "You can edit only one User at a time.");
            }
        }

        public async Task HandleSubmit(ProjectDto project)
        {
            project.IsEdit = false;
            Editing = false;

            var success = await ProjectManagerService.EditUserInDb
                (project.ProjectId, SessionDetails.UserId, project.ProjectName ?? "", project.IsActive);
            if (success) {
                Projects = await ProjectManagerService.GetAllProjects();
            }
        }

        public void HandleCancel(ProjectDto project)
        {
            project.IsEdit = false;
            Editing = false;
        }

        // Toggling a user

        public ProjectDto DeleteConfirmationProject { get; set; } = new();

        public async Task HandleInActive(ProjectDto project)
        {
            var success = await ProjectManagerService.SetProjectStatus(project.ProjectId, false);
            if (success) {
                await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#DeleteProjectConfirmationModal");
                Projects = await ProjectManagerService.GetAllProjects();
            }
            
        }

        public async Task HandleActive(ProjectDto project)
        {
            var success = await ProjectManagerService.SetProjectStatus(project.ProjectId, true);
            if (success)
            {
                Projects = await ProjectManagerService.GetAllProjects();
            }
        }
    }
}
