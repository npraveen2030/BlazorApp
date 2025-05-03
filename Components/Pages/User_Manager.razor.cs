using BlazorApp.Models.Entities;
using BlazorApp.Models.Dtos;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Components.Pages
{
    public partial class User_Manager : ComponentBase
    {
        [Inject] public AuthDbContext Context { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;

        public UserDetailDto AdminAddUserForm { get; set; } = new();
        public List<UserDetailDto> UserDetails { get; set; } = new();

        public Pagination pagination { get; set; } = null!;

        public string SearchText { get; set; } = null!;
        public class Pagination(AuthDbContext Context , Func<int, int, Task> LoadUsers, Action ChangeIsOrderCD , Action ChangeIsOrderMD)
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
                var query = Context.UserDetails.AsQueryable();

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
                    query = query.Where(u => u.UserName.Contains(SearchText));
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
                    await LoadUsers(CurrentPage, PageSize);
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
                        await LoadUsers(CurrentPage , PageSize);
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
                        await LoadUsers(CurrentPage, PageSize);
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
                await LoadUsers(CurrentPage, PageSize);
            }
        }

        internal async Task LoadUsers(int CurrentPage ,int PageSize)
        {
            await pagination.PageCount(SearchText, SelectedFilterValue);
            pagination.PageNavigators();

            var query = Context.UserDetails.AsQueryable();

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
                query = query.Where(u => u.UserName.Contains(SearchText));
            }

            // Resetting Order
            Ascorder = true;
            IsOrderonCreateDate = false;
            IsOrderonModifyDate = false;

            UserDetails = await query
                .Select(u => new UserDetailDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Password = u.Password,
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
                    IsActive = u.IsActive
                })
                .Skip((CurrentPage-1)*PageSize)
                .Take(PageSize)
                .ToListAsync();

        } 

        protected override async Task OnInitializedAsync()
        {
            pagination = new Pagination(Context,LoadUsers, ChangeIsOrderCD , ChangeIsOrderMD);
            await LoadUsers(pagination.CurrentPage , pagination.PageSize);
        }

        // Searching

        internal async Task SearchUsers()
        {
            pagination.CurrentPage = 1;
            await LoadUsers(pagination.CurrentPage, pagination.PageSize);

        }

        // Ordering

        public bool Ascorder { get; set; } = true;
        
        public void Reorder()
        {
            if(Ascorder)
            {
                UserDetails.Reverse();
                Ascorder = false;
            }
            else
            {
                UserDetails.Reverse();
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
                UserDetails = UserDetails.OrderBy(c => c.CreatedDate).ToList();
                IsOrderonCreateDate = true;
            }
            else
            {
                UserDetails = UserDetails.OrderBy(c => c.UserId).ToList();
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
                UserDetails = UserDetails.OrderBy(c => c.ModifiedDate).ToList();
                IsOrderonModifyDate = true;
            }
            else
            {
                UserDetails = UserDetails.OrderBy(c => c.UserId).ToList();
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
            await LoadUsers(pagination.CurrentPage, pagination.PageSize);
        }

        // Adding a User
        internal async Task AddUserSubmitHandler()
        {
            try
            {
                var newUser = new UserDetail
                {
                    UserName = AdminAddUserForm.UserName,
                    Password = AdminAddUserForm.Password,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = 1,
                    IsActive = true
                };

                Context.UserDetails.Add(newUser);
                await Context.SaveChangesAsync();

                AdminAddUserForm.UserName = "";
                AdminAddUserForm.Password = "";

                await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#AddUserModal");



                await LoadUsers(pagination.CurrentPage , pagination.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Editing a user
        public bool Editing { get; set; } = false;

        internal async Task HandleEdit(UserDetailDto user)
        {
            if (!Editing)
            {
                user.IsEdit = true;
                Editing = true;
            }
            else
            {
                await JS.InvokeVoidAsync("alert", "You can edit only one User at a time.");
            }
        }

        internal async Task HandleSubmit(UserDetailDto user)
        {
            user.IsEdit = false;
            Editing = false;

            var modified_user = Context.UserDetails.FirstOrDefault(u => u.UserId == user.UserId);

            if (modified_user != null)
            {
                modified_user.Password = user.Password;
                modified_user.ModifiedBy = 1;
                modified_user.ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
                modified_user.IsActive = user.IsActive;
                await Context.SaveChangesAsync();
            }

            await LoadUsers(pagination.CurrentPage , pagination.PageSize);
        }

        internal void HandleCancel(UserDetailDto user)
        {
            user.IsEdit = false;
            Editing = false;
        }

        // Toggling a user

        public UserDetailDto? DeleteConfirmationuser { get; set; } 

        internal async Task HandleInActive(UserDetailDto user)
        {
            var toggled_user = Context.UserDetails.FirstOrDefault(u => u.UserId == user.UserId);

            if (toggled_user != null)
            {
                toggled_user.IsActive = false;
                await Context.SaveChangesAsync();
            }

            await JS.InvokeVoidAsync("bootstrapInterop.hideModal", "#DeleteConfirmationModal");
            await LoadUsers(pagination.CurrentPage, pagination.PageSize);
        }

        internal async Task HandleActive(UserDetailDto user)
        {

            var toggled_user = Context.UserDetails.FirstOrDefault(u => u.UserId == user.UserId);

            if (toggled_user != null)
            {
                toggled_user.IsActive = true;
                await Context.SaveChangesAsync();
            }

            await LoadUsers(pagination.CurrentPage, pagination.PageSize);
        }
    }
}
