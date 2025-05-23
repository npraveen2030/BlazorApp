namespace BlazorApp.Components.Pages.Associations
{
    public partial class UserAssociatedProjects : ComponentBase
    {
        [Inject] private AuthDbContext Context { get; set; } = null!;
        [Inject] private NavigationManager NavManager { get; set; } = null!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        private List<UserAssociatedProjectsDto> AssociatedProjects { get; set; } = new();
        
        private UserSession SessionDetails { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                SessionDetails.UserId = Convert.ToInt32(user.Identity.Name);
                SessionDetails.UserName = await Context.UserDetails
                                                 .Where(u => u.UserId == SessionDetails.UserId)
                                                 .Select(u => u.UserName)
                                                 .FirstOrDefaultAsync() ?? "";
                SessionDetails.UserRoles =  user.Claims
                                                .Where(c => c.Type == ClaimTypes.Role)
                                                .Select(c => c.Value)
                                                .ToList();

            }

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
