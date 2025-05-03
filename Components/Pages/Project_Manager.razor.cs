using BlazorApp.Models.Entities;
using BlazorApp.Models.Dtos;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Components.Pages
{
    public partial class Project_Manager : ComponentBase
    {
        [Inject] public AuthDbContext Context { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;

        public ProjectDto AddProjectForm { get; set; } = new();
        public List<ProjectDto> Projects { get; set; } = new();

        public Pagination pagination { get; set; } = null!;

        public string SearchText { get; set; } = null!;
        public class Pagination(AuthDbContext Context , Func<int, int, Task> LoadProjects, Action ChangeIsOrderCD , Action ChangeIsOrderMD)
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
                else if (CurrentPage == 1 )
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

            public async Task PageCount(string SearchText , string SelectedFilterValue)
            {
                var query = Context.Projects.AsQueryable();

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
                    query = query.Where(u => u.ProjectName.Contains(SearchText));
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
                    await LoadProjects(CurrentPage, PageSize);
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
                        await LoadProjects(CurrentPage , PageSize);
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
                        await LoadProjects(CurrentPage, PageSize);
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
                await LoadProjects(CurrentPage, PageSize);
            }
        }

        internal async Task LoadProjects(int CurrentPage ,int PageSize)
        {
            await pagination.PageCount(SearchText, SelectedFilterValue);
            pagination.PageNavigators();

            var query = Context.Projects.AsQueryable();

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
                query = query.Where(u => u.ProjectName.Contains(SearchText));
            }

            // Resetting Order
            Ascorder = true;
            IsOrderonCreateDate = false;
            IsOrderonModifyDate = false;

            Projects = await query
                .Select(u => new ProjectDto
                {
                    ProjectId = u.ProjectId,
                    ProjectName = u.ProjectName,
                    CreatedBy = Context.UserDetails
                                        .Where(c => c.UserId == u.CreatedBy)
                                        .Select(c => c.UserName)
                                        .FirstOrDefault(),
                    CreatedDate = u.CreatedDate,
                    ModifiedBy = Context.UserDetails
                                        .Where(c => c.UserId == u.ModifiedBy)
                                        .Select(c => c.UserName)
                                        .FirstOrDefault(),
                    ModifiedDate = u.ModifiedDate,
                    IsActive = u.IsActive ?? false
                })
                .Skip((CurrentPage-1)*PageSize)
                .Take(PageSize)
                .ToListAsync();

        } 

        protected override async Task OnInitializedAsync()
        {
            pagination = new Pagination(Context,LoadProjects, ChangeIsOrderCD , ChangeIsOrderMD);
            await LoadProjects(pagination.CurrentPage , pagination.PageSize);
        }

        // Searching

        internal async Task SearchProjects()
        {
            pagination.CurrentPage = 1;
            await LoadProjects(pagination.CurrentPage, pagination.PageSize);

        }

        // Ordering

        public bool Ascorder { get; set; } = true;
        
        public void Reorder()
        {
            if(Ascorder)
            {
                Projects.Reverse();
                Ascorder = false;
            }
            else
            {
                Projects.Reverse();
                Ascorder = true;
            }
        }

        // on created date
        public bool IsOrderonCreateDate {  get; set; } = false;
        internal void OrderCreatedDate()
        {
            ChangeIsOrderMD();

            if (IsOrderonCreateDate == false)
            {
                Projects = Projects.OrderBy(c => c.CreatedDate).ToList();
                IsOrderonCreateDate = true;
            }
            else
            {
                Projects = Projects.OrderBy(c => c.ProjectId).ToList();
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
                Projects = Projects.OrderBy(c => c.ModifiedDate).ToList();
                IsOrderonModifyDate = true;
            }
            else
            {
                Projects = Projects.OrderBy(c => c.ProjectId).ToList();
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
            await LoadProjects(pagination.CurrentPage, pagination.PageSize);
        }

        // Adding a Project
        internal async Task AddProjectSubmitHandler()
        {
            try
            {
                var newProject = new Project
                {
                    ProjectName = AddProjectForm.ProjectName,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = 1,
                    IsActive = true
                };

                Context.Projects.Add(newProject);
                await Context.SaveChangesAsync();

                AddProjectForm.ProjectName = "";

                await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddProjectModal");



                await LoadProjects(pagination.CurrentPage , pagination.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Editing a Project
        public bool Editing { get; set; } = false;

        internal async Task HandleEdit(ProjectDto project)
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

        internal async Task HandleSubmit(ProjectDto project)
        {
            project.IsEdit = false;
            Editing = false;

            var modified_project = Context.Projects.FirstOrDefault(u => u.ProjectId == project.ProjectId);

            if (modified_project != null)
            {
                modified_project.ProjectName = project.ProjectName;
                modified_project.ModifiedBy = 1;
                modified_project.ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
                modified_project.IsActive = project.IsActive;
                await Context.SaveChangesAsync();
            }

            await LoadProjects(pagination.CurrentPage , pagination.PageSize);
        }

        internal void HandleCancel(ProjectDto project)
        {
            project.IsEdit = false;
            Editing = false;
        }

        // Toggling a user

        public ProjectDto? DeleteConfirmationProject { get; set; } 

        internal async Task HandleInActive(ProjectDto project)
        {
            var toggled_project = Context.Projects.FirstOrDefault(u => u.ProjectId == project.ProjectId);

            if (toggled_project != null)
            {
                toggled_project.IsActive = false;
                await Context.SaveChangesAsync();
            }

            await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#DeleteProjectConfirmationModal");
            await LoadProjects(pagination.CurrentPage, pagination.PageSize);
        }

        internal async Task HandleActive(ProjectDto project)
        {

            var toggled_project = Context.Projects.FirstOrDefault(u => u.ProjectId == project.ProjectId);

            if (toggled_project != null)
            {
                toggled_project.IsActive = true;
                await Context.SaveChangesAsync();
            }

            await LoadProjects(pagination.CurrentPage, pagination.PageSize);
        }
    }
}
