namespace BlazorApp.Components.Pages.Associations
{
    public partial class Project_Manager_Portal : ComponentBase
    {
        [Inject] private AuthDbContext Context { get; set; } = null!;
        [Inject] private IJSRuntime JS { get; set; } = null!;
        [Inject] private NavigationManager NavManager { get;set; } = null!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        private List<UserAssociatedProjectsDto> Associations { get; set;} = null!;
        private List<UserAssociatedProjectsDto> ActiveUsers { get; set; } = null!;
        private List<UserAssociatedProjectsDto> AvailableProjects { get; set; } = null!;
        private UserAssociatedProjectsDto AddAssociationForm { get; set; } = new();
        internal UserSession SessionDetails { get; set; } = new();

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
                SessionDetails.UserRoles = user.Claims
                                                .Where(c => c.Type == ClaimTypes.Role)
                                                .Select(c => c.Value)
                                                .ToList();

            }

            await FetchAssociations();
        }

        private async Task FetchAssociations()
        {
            Associations = await Context.UserProjectRoleAssociations
                                 .Where(assoc => assoc.CreatedBy == SessionDetails.UserId
                                                              && assoc.RoleId == 12)
                                 .Include(assoc => assoc.User)
                                 .Include(assoc => assoc.Project)
                                 .Select(assoc => new UserAssociatedProjectsDto
                                 {
                                     UpraId = assoc.UpraId,
                                     UserId = assoc.UserId,
                                     ProjectId = assoc.ProjectId,
                                     RoleId = assoc.RoleId,
                                     UserName = assoc.User.UserName,
                                     ProjectName = assoc.Project.ProjectName ?? "",
                                     CreatedDate = assoc.CreatedDate,
                                     CreatedBy = Context.UserDetails
                                                        .Where(u => u.UserId == assoc.CreatedBy)
                                                        .Select(u => u.UserName)
                                                        .FirstOrDefault(),
                                     ModifiedDate = assoc.ModifiedDate,
                                     ModifiedBy = Context.UserDetails
                                                        .Where(u => u.UserId == assoc.ModifiedBy)
                                                        .Select(u => u.UserName)
                                                        .FirstOrDefault(),
                                     IsActive = assoc.IsActive,

                                 })
                                 .ToListAsync();
        }

        private List<int> PLAssociatedUsers { get;set; } = null!;

        private async Task HandleAddAssociation()
        {
            ActiveUsers = await Context.UserDetails
                                       .Where(u => u.IsActive 
                                                && u.UserId != SessionDetails.UserId)
                                       .Select(u => new UserAssociatedProjectsDto
                                       {
                                           UserId = u.UserId,
                                           UserName = u.UserName  
                                       })
                                       .ToListAsync();

            PLAssociatedUsers = await Context.UserProjectRoleAssociations
                                             .Where(assoc => assoc.RoleId == 12
                                                          && assoc.IsActive)
                                             .Select(assoc => assoc.UserId)
                                             .ToListAsync();

            ActiveUsers = ActiveUsers.Where(a => !PLAssociatedUsers.Contains(a.UserId)).ToList();

            await JS.InvokeVoidAsync("bootstrapInterop.showModal", "#AddAssociationModal");
        }

        private int UserId_BindValue
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

        private List<int> PLAssociatedProjects { get;set; } = null!;

        private async Task SelectedUserHandler(int value)
        {
            AvailableProjects = await Context.UserProjectRoleAssociations
                                             .Where(assoc => assoc.UserId == SessionDetails.UserId
                                                          && assoc.RoleId == Convert.ToInt32(SessionDetails.UserRoles[0])
                                                          && assoc.IsActive)
                                             .Include(assoc => assoc.Project)
                                             .Select(assoc => new UserAssociatedProjectsDto
                                             {
                                                 ProjectId   = assoc.ProjectId,
                                                 ProjectName = assoc.Project.ProjectName ?? ""
                                             })
                                             .ToListAsync();

            PLAssociatedProjects = await Context.UserProjectRoleAssociations
                                                .Where(assoc => assoc.RoleId == 12
                                                             && assoc.CreatedBy == SessionDetails.UserId
                                                             && assoc.IsActive)
                                                .Select(assoc => assoc.ProjectId)
                                                .ToListAsync();
            AvailableProjects = AvailableProjects.Where(a => !PLAssociatedProjects.Contains(a.ProjectId)).ToList();
            StateHasChanged();
        }

        private async Task AddAssociationSubmitHandler()
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

                Context.UserProjectRoleAssociations.Add(newUPRA);
                await Context.SaveChangesAsync();

                // Resetting Values
                UserId_BindValue = 0;
                AddAssociationForm.ProjectId = 0;

                await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddAssociationModal");
                await FetchAssociations();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DeleteAssociation(UserAssociatedProjectsDto association)
        {
            var deletedAssociation = await Context.UserProjectRoleAssociations
                                            .Where(assoc => assoc.UpraId == association.UpraId)
                                            .FirstOrDefaultAsync();

            if (deletedAssociation != null)
            {
                deletedAssociation.IsActive = false;
                await Context.SaveChangesAsync();
            }

            await FetchAssociations();
        }
    }
}
