using BlazorApp.Models.Dtos;

namespace BlazorApp.Components.Pages.Associations
{
    public partial class UserAssociatedProjects : ComponentBase
    {
        [Inject] public AuthDbContext Context { get; set; } = null!;
        [Inject] public UserSession SessionDetails { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;

        public List<UserAssociatedProjectsDto> AssociatedProjects { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            AssociatedProjects = await Context.UserProjectRoleAssociations
                                              .Where(a => a.IsActive && a.UserId == SessionDetails.UserId)
                                              .Include(a => a.Project)
                                              .Select(a => new UserAssociatedProjectsDto
                                              {
                                                  UserId = SessionDetails.UserId,
                                                  RoleId = a.RoleId,
                                                  ProjectId = a.ProjectId,
                                                  ProjectName = a.Project.ProjectName ?? ""
                                              })
                                              .ToListAsync();
        }

        public void HandleProjectSelect(int id)
        {
            NavManager.NavigateTo($"/project/{id}");
        }
    }
}
