using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace BlazorApp.Components.Pages.Associations
{
    public partial class Project_Lead_Portal : ComponentBase
    {
        [Inject] internal AuthDbContext Context { get; set; } = null!; 
        [Inject] internal IJSRuntime JS { get; set; } = null!;
        [Inject] internal NavigationManager NavManager { get; set; } = null!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        private List<UserAssociatedProjectsDto> Associations { get; set; } = null!;
        private List<UserAssociatedProjectsDto> ActiveUsers { get; set; } = null!;
        private List<UserAssociatedProjectsDto> AvailableRoles { get; set; } = null!;
        private UserAssociatedProjectsDto PLDetails { get;set; } = new();
        private UserAssociatedProjectsDto AddAssociationForm { get; set; } = new();

        private List<int> pageSizeOptions = new() { 5, 8, 10 };

        public Dictionary<string,GridMeta> GridState { get; set; } = new()
            {
                { "UserName",      new GridMeta { Width="120px",   OrderIndex=0   ,Visible=true   } },
                { "RoleName",      new GridMeta { Width = "120px", OrderIndex = 1, Visible = true } },
                { "CreatedDate",   new GridMeta { Width = "120px", OrderIndex = 2, Visible = true } },
                { "CreatedBy",     new GridMeta { Width = "120px", OrderIndex = 3, Visible = true } },
                { "ModifiedDate",  new GridMeta { Width = "120px", OrderIndex = 4, Visible = true } },
                { "ModifiedBy",    new GridMeta { Width = "120px", OrderIndex = 5, Visible = true } },
                { "IsActive",      new GridMeta { Width = "120px", OrderIndex = 6, Visible = true } }
            };

        public GridPreferencesSaver<UserAssociatedProjectsDto> gridPreferencesSaver { get; set; } = null!;
        private UserSession SessionDetails { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            try
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

                var GridStatejson = await Context.UserGridAssocs
                                         .Where(ugs => ugs.UserId == SessionDetails.UserId
                                                          && ugs.GroupId == 2
                                                          && ugs.TabId == 2)
                                         .Select(ugs => ugs.Preferences)
                                         .FirstOrDefaultAsync();
                if (GridStatejson is not null)
                {
                    GridState = JsonSerializer.Deserialize<Dictionary<string, GridMeta>>(GridStatejson) ?? GridState;
                }
                
                gridPreferencesSaver = new GridPreferencesSaver<UserAssociatedProjectsDto>(GridState);
                PLDetails = await Context.UserProjectRoleAssociations
                                           .Where(assoc => assoc.UserId == SessionDetails.UserId
                                                        && assoc.RoleId == 12)
                                           .Include(assoc => assoc.User)
                                           .Include(assoc => assoc.Role)
                                           .Include(assoc => assoc.Project)
                                           .Select(assoc => new UserAssociatedProjectsDto
                                           {
                                               UserName = assoc.User.UserName ?? "",
                                               RoleName = assoc.Role.RoleName ?? "",
                                               ProjectId = assoc.ProjectId,
                                               ProjectName = assoc.Project.ProjectName ?? ""
                                           })
                                           .FirstOrDefaultAsync() ?? new();

                AddAssociationForm.ProjectId = PLDetails.ProjectId;

                await FetchAssociations();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during OnInitializedAsync: " + ex.Message);
            }
        }

        private async Task FetchAssociations()
        {
            Associations = await Context.UserProjectRoleAssociations
                                        .Where(assoc => assoc.ProjectId == PLDetails.ProjectId
                                                     && assoc.RoleId != 11
                                                     && assoc.RoleId != 12)
                                        .Include(assoc => assoc.User)
                                        .Include(assoc => assoc.Role)
                                        .Select(assoc => new UserAssociatedProjectsDto
                                        {
                                            UpraId = assoc.UpraId,
                                            UserId = assoc.UserId,
                                            ProjectId = assoc.ProjectId,
                                            RoleId = assoc.RoleId,
                                            UserName = assoc.User.UserName,
                                            RoleName = assoc.Role.RoleName ?? "",
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

        private List<int> ProjectAssociatedAuthorities { get; set; } = null!;

        private async Task HandleAddAssociation()
        {
            ActiveUsers = await Context.UserDetails
                                       .Where(u => u.IsActive)
                                       .Select(u => new UserAssociatedProjectsDto
                                       {
                                           UserId = u.UserId,
                                           UserName = u.UserName
                                       })
                                       .ToListAsync();

            ProjectAssociatedAuthorities = await Context.UserProjectRoleAssociations
                                             .Where(assoc => assoc.ProjectId == PLDetails.ProjectId
                                                          && assoc.IsActive)
                                             .Select(assoc => assoc.UserId)
                                             .ToListAsync();

            ActiveUsers = ActiveUsers.Where(a => !ProjectAssociatedAuthorities.Contains(a.UserId)).ToList();

            await JS.InvokeVoidAsync("bootstrapInterop.showModal", "#AddAssociationModal");
        }

        private async Task HideModal()
        {
            // Resetting Form
            AddAssociationForm = new UserAssociatedProjectsDto() { ProjectId = PLDetails.ProjectId };

            await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddAssociationModal");
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
                        _ = SelectedUserHandler();
                    }

                }
            }
        }

        private async Task SelectedUserHandler()
        {
            AvailableRoles = await Context.UserRoles
                                          .Where(assoc => assoc.IsActive
                                                       && assoc.RoleId != 1
                                                       && assoc.RoleId != 11
                                                       && assoc.RoleId != 12)
                                          .Select(assoc => new UserAssociatedProjectsDto
                                          {
                                              RoleId = assoc.RoleId,
                                              RoleName = assoc.RoleName ?? ""
                                          })
                                          .ToListAsync();

            StateHasChanged();
        }

        private async Task AddAssociationSubmitHandler()
        {
            try
            {
                var newUPRA = new UserProjectRoleAssociation
                {
                    UserId = AddAssociationForm.UserId,
                    RoleId = AddAssociationForm.RoleId,
                    ProjectId = AddAssociationForm.ProjectId,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = SessionDetails.UserId,
                    IsActive = true
                };

                Context.UserProjectRoleAssociations.Add(newUPRA);
                await Context.SaveChangesAsync();

                // Resetting Form
                AddAssociationForm = new UserAssociatedProjectsDto() { ProjectId = PLDetails.ProjectId };


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
                deletedAssociation.ModifiedBy = SessionDetails.UserId;
                deletedAssociation.ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
                deletedAssociation.IsActive = false;
                await Context.SaveChangesAsync();
            }

            await FetchAssociations();
        }

        // Grid Preference Saver to DB
        private async Task SavePreferencesHandler()
        {
            try
            {
                var userpref = await Context.UserGridAssocs
                                            .Where(ugs => ugs.UserId == SessionDetails.UserId
                                                          && ugs.GroupId == 2
                                                          && ugs.TabId == 2)
                                            .FirstOrDefaultAsync();
                                            
                if (userpref == null)
                {
                    var newUGA = new UserGridAssoc
                    {
                        UserId = SessionDetails.UserId,
                        GroupId = 2,
                        TabId = 2,
                        Preferences = JsonSerializer.Serialize(GridState)
                    };

                    Context.UserGridAssocs.Add(newUGA);
                }
                else
                {
                    userpref.Preferences = JsonSerializer.Serialize(GridState); 
                }
                await Context.SaveChangesAsync();
                await JS.InvokeVoidAsync("bootstrapInterop.showToast", "savepreferenestoast");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
