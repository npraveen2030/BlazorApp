using BlazorApp.Models.Dtos;
using BlazorApp.Models.Entities;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Radzen.Blazor.Rendering;

namespace BlazorApp.Components.Pages.Associations
{
    public partial class Project_Lead_Portal : ComponentBase
    {
        [Inject] internal AuthDbContext Context { get; set; } = null!;
        [Inject] internal UserSession SessionDetails { get; set; } = null!;
        [Inject] internal IJSRuntime JS { get; set; } = null!;
        [Inject] internal NavigationManager NavManager { get; set; } = null!;

        private List<UserAssociatedProjectsDto> Associations { get; set; } = null!;
        private int PLProjectId { get;set; }
        private List<UserAssociatedProjectsDto> ActiveUsers { get; set; } = null!;
        private List<UserAssociatedProjectsDto> AvailableRoles { get; set; } = null!;
        private UserAssociatedProjectsDto AddAssociationForm { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            PLProjectId = await Context.UserProjectRoleAssociations
                                       .Where(assoc => assoc.UserId == SessionDetails.UserId)
                                       .Select(assoc => assoc.ProjectId)
                                       .FirstOrDefaultAsync();

            await FetchAssociations();
        }


        private async Task FetchAssociations()
        {
            Associations = await Context.UserProjectRoleAssociations
                                 .Where(assoc => assoc.ProjectId == PLProjectId
                                              && assoc.RoleId != 11
                                              && assoc.RoleId != 12)
                                 .Include(assoc => assoc.User)
                                 .Include(assoc => assoc.Role)
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
                                             .Where(assoc => assoc.ProjectId == PLProjectId
                                                          && assoc.IsActive)
                                             .Select(assoc => assoc.UserId)
                                             .ToListAsync();

            ActiveUsers = ActiveUsers.Where(a => !ProjectAssociatedAuthorities.Contains(a.UserId)).ToList();

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
                    ProjectId = PLProjectId,
                    CreatedDate = DateTime.Now,
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
                deletedAssociation.ModifiedBy = SessionDetails.UserId;
                deletedAssociation.ModifiedDate = DateTime.Now;
                deletedAssociation.IsActive = false;
                await Context.SaveChangesAsync();
            }

            await FetchAssociations();
        }
    }
}
