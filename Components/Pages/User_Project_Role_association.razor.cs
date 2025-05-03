using BlazorApp.Models.Entities;
using BlazorApp.Models.Dtos;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Components.Pages
{
    public partial class User_Project_Role_Association : ComponentBase
    {
        [Inject] public AuthDbContext Context { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;
        private DotNetObjectReference<User_Project_Role_Association>? _dotNetRef;

        public UPRAForm Upraform { get; set; } = new();
        public List<UserProjectRoleAssociationDto> UPRAs { get; set; } = new();

        public Pagination pagination { get; set; } = null!;

        public string SearchText { get; set; } = null!;

        public class Pagination(AuthDbContext Context, Func<int, int, Task> LoadUPRAs, Action ChangeIsOrderCD, Action ChangeIsOrderMD)
        {
            public int CurrentPage { get; set; } = 1;
            public int PageSize { get; set; } = 10;
            public int TotalPages { get; set; }
            public bool IsPrevious { get; set; }
            public bool IsNext { get; set; }

            public void PageNavigators()
            {
                if (TotalPages == 0 || TotalPages == 1)
                {
                    IsPrevious = false;
                    IsNext = false;
                }
                else if (CurrentPage == 1)
                {
                    IsPrevious = false;
                    IsNext = true;
                }
                else if (CurrentPage == TotalPages)
                {
                    IsPrevious = true;
                    IsNext = false;
                }
                else
                {
                    IsPrevious = true;
                    IsNext = true;
                }
            }

            public async Task PageCount(string SearchText, string SelectedFilterValue)
            {
                var query = Context.UserProjectRoleAssociations
                               .Include(a => a.User)
                               .Include(a => a.Project)
                               .Include(a => a.Role)
                               .AsQueryable();

                if (SelectedFilterValue == "active")
                {
                    query = query.Where(u => u.IsActive == true);
                }

                if (SelectedFilterValue == "inactive")
                {
                    query = query.Where(u => u.IsActive == false);
                }

                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    query = query.Where(u => u.User.UserName.Contains(SearchText));
                }

                TotalPages = (int)Math.Ceiling(Convert.ToDecimal(await query.CountAsync()) / Convert.ToDecimal(PageSize));
            }

            public async Task ChangePage(int pageNumber)
            {
                try
                {
                    CurrentPage = pageNumber;
                    PageNavigators();
                    ChangeIsOrderCD();
                    ChangeIsOrderMD();
                    await LoadUPRAs(CurrentPage, PageSize);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Message : " + ex);
                }

            }

            public async Task PreviousPage()
            {
                try
                {
                    if (IsPrevious)
                    {
                        CurrentPage = CurrentPage - 1;
                        PageNavigators();
                        ChangeIsOrderCD();
                        ChangeIsOrderMD();
                        await LoadUPRAs(CurrentPage, PageSize);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Message : " + ex);
                }
            }

            public async Task NextPage()
            {
                try
                {
                    if (IsNext)
                    {
                        CurrentPage = CurrentPage + 1;
                        PageNavigators();
                        ChangeIsOrderCD();
                        ChangeIsOrderMD();
                        await LoadUPRAs(CurrentPage, PageSize);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Message : " + ex);
                }
            }
            public async Task ChangePageSize(int pageSize)
            {
                PageSize = pageSize;
                CurrentPage = 1;
                await LoadUPRAs(CurrentPage, PageSize);
            }
        }

        internal async Task LoadUPRAs(int CurrentPage, int PageSize)
        {
            await pagination.PageCount(SearchText, SelectedFilterValue);
            pagination.PageNavigators();

            var query = Context.UserProjectRoleAssociations
                               .Include(a => a.User)
                               .Include(a => a.Project)
                               .Include(a => a.Role)
                               .AsQueryable();

            // Filtering
            if (SelectedFilterValue == "active")
            {
                query = query.Where(u => u.IsActive == true);
            }

            if (SelectedFilterValue == "inactive")
            {
                query = query.Where(u => u.IsActive == false);
            }

            // Searching
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(u => u.User.UserName.Contains(SearchText));
            }

            // Resetting Order
            Ascorder = true;
            IsOrderonCreateDate = false;
            IsOrderonModifyDate = false;

            UPRAs = await query
                .Select(u => new UserProjectRoleAssociationDto
                {
                    UpraId = u.UpraId,
                    UserName = u.User.UserName,
                    RoleName = u.Role.RoleName,
                    ProjectName = u.Project.ProjectName ?? "-",
                    CreatedBy = Context.UserDetails
                                .Where(c => c.UserId == u.CreatedBy)
                                .Select(c => c.UserName)
                                .FirstOrDefault() ?? "-",
                    CreatedDate = u.CreatedDate,
                    ModifiedBy = Context.UserDetails
                                .Where(c => c.UserId == u.ModifiedBy)
                                .Select(c => c.UserName)
                                .FirstOrDefault() ?? "-",
                    ModifiedDate = u.ModifiedDate,
                    IsActive = u.IsActive
                })
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

        }

        protected override async Task OnInitializedAsync()
        {
            pagination = new Pagination(Context, LoadUPRAs, ChangeIsOrderCD, ChangeIsOrderMD);
            await LoadUPRAs(pagination.CurrentPage, pagination.PageSize);
        }

        // Searching

        internal async Task SearchUPRAs()
        {
            pagination.CurrentPage = 1;
            await LoadUPRAs(pagination.CurrentPage, pagination.PageSize);

        }

        // Ordering

        public bool Ascorder { get; set; } = true;

        public void Reorder()
        {
            if (Ascorder)
            {
                UPRAs.Reverse();
                Ascorder = false;
            }
            else
            {
                UPRAs.Reverse();
                Ascorder = true;
            }
        }

        // on created date
        public bool IsOrderonCreateDate { get; set; } = false;
        internal void OrderCreatedDate()
        {
            ChangeIsOrderMD();

            if (IsOrderonCreateDate == false)
            {
                UPRAs = UPRAs.OrderBy(c => c.CreatedDate).ToList();
                IsOrderonCreateDate = true;
            }
            else
            {
                UPRAs = UPRAs.OrderBy(c => c.UpraId).ToList();
                IsOrderonCreateDate = false;
            }

        }

        public void ChangeIsOrderCD()
        {
            IsOrderonCreateDate = false;
            Ascorder = true;
        }

        // on modified date
        public bool IsOrderonModifyDate { get; set; } = false;
        internal void OrderModifyDate()
        {
            ChangeIsOrderCD();

            if (IsOrderonModifyDate == false)
            {
                UPRAs = UPRAs.OrderBy(c => c.ModifiedDate).ToList();
                IsOrderonModifyDate = true;
            }
            else
            {
                UPRAs = UPRAs.OrderBy(c => c.UpraId).ToList();
                IsOrderonModifyDate = false;
            }

        }

        public void ChangeIsOrderMD()
        {
            IsOrderonModifyDate = false;
            Ascorder = true;
        }

        // Filtering

        public string SelectedFilterValue { get; set; } = "all";

        internal async Task OnFilterChanged(ChangeEventArgs e)
        {
            SelectedFilterValue = e.Value?.ToString() ?? "all";
            pagination.CurrentPage = 1;
            await LoadUPRAs(pagination.CurrentPage, pagination.PageSize);
        }

        // Adding a UPRA

        public List<ActiveUsers> activeUsers { get; set; } = null!;

        internal async Task HandleAddUPRAClick()
        {
            activeUsers = await Context.UserDetails
                                          .Where(u => u.IsActive)
                                          .Select(u => new ActiveUsers
                                          {
                                              UserId = u.UserId,
                                              UserName = u.UserName
                                          })
                                          .ToListAsync();
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

        public List<ActiveProjects> activeProjects { get; set; } = null!;
        public List<int> UserAssociatedProjects { get; set; } = null!;
        public List<ActiveProjects> inactiveProjects { get; set; } = null!;

        internal async Task SelectedUserHandler(int value)
        {

            activeProjects = await Context.Projects
                                          .Where(p => p.ProjectName != null)
                                          .Select(p => new ActiveProjects
                                          {
                                              ProjectId = p.ProjectId,
                                              ProjectName = p.ProjectName ?? ""
                                          })
                                          .ToListAsync();

            UserAssociatedProjects = await Context.UserProjectRoleAssociations
                                                  .Where(a => a.UserId == value && a.IsActive)
                                                  .Select(a => a.ProjectId)
                                                  .ToListAsync();

            inactiveProjects = activeProjects.Where(a => !UserAssociatedProjects.Contains(a.ProjectId)).ToList();
            StateHasChanged();
        }

        internal async Task AddUpraSubmitHandler()
        {
            try
            {
                var newUPRA = new UserProjectRoleAssociation
                {
                    UserId = Upraform.UserId,
                    RoleId = 11,
                    ProjectId = Upraform.ProjectId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1,
                    IsActive = true
                };

                Context.UserProjectRoleAssociations.Add(newUPRA);
                await Context.SaveChangesAsync();

                UserId_bind = 0;
                Upraform.ProjectId = 0;

                await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddUpraModal");
                await LoadUPRAs(pagination.CurrentPage, pagination.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Toggling a upra

        public UserProjectRoleAssociationDto? DeleteConfirmationupra { get; set; }

        internal async Task HandleInActive(UserProjectRoleAssociationDto upra)
        {
            var toggled_user = Context.UserProjectRoleAssociations.FirstOrDefault(u => u.UpraId == upra.UpraId);

            if (toggled_user != null)
            {
                toggled_user.IsActive = false;
                await Context.SaveChangesAsync();
            }

            await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#DeleteConfirmationModal");
            await LoadUPRAs(pagination.CurrentPage, pagination.PageSize);
        }

        internal async Task HandleActive(UserProjectRoleAssociationDto upra)
        {

            var toggled_user = Context.UserProjectRoleAssociations.FirstOrDefault(u => u.UpraId == upra.UpraId);

            if (toggled_user != null)
            {
                toggled_user.IsActive = true;
                await Context.SaveChangesAsync();
            }

            await LoadUPRAs(pagination.CurrentPage, pagination.PageSize);
        }

    }
}
