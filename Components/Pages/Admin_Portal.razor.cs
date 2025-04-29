using BlazorApp.Models.Entities;
using BlazorApp.Models.Dtos;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Components.Pages
{
    public partial class Admin_Portal : ComponentBase
    {
        [Inject] public AuthDbContext Context { get; set; } = null!;
        [Inject] public NavigationManager NavManager { get; set; } = null!;
        [Inject] public IJSRuntime JS { get; set; } = null!;

        public UserDetailDto AdminAddUserForm { get; set; } = new();
        public List<UserDetailDto> UserDetails { get; set; } = new();

        

        // Page Based Users Retrival from DB
        private async Task LoadUsers()
        {
            UserDetails = await Context.UserDetails
                .Select(u => new UserDetailDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Password = u.Password,
                    CreatedBy = u.CreatedBy,
                    CreatedDate = u.CreatedDate,
                    ModifiedBy = u.ModifiedBy,
                    ModifiedDate = u.ModifiedDate,
                    IsActive = u.IsActive
                })
                .Skip((CurrentPage-1)*PageSize)
                .Take(PageSize)
                .ToListAsync();

        }

        public void PageNavigators()
        {
            if (CurrentPage == 1)
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

        protected override async Task OnInitializedAsync()
        {
            TotalPages = (await Context.UserDetails.CountAsync() / PageSize) + 1;
            PageNavigators();
            await LoadUsers();
        }

        // Pagination

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 8;

        public int TotalPages { get; set; }

        public bool IsPrevious { get; set; } 

        public bool IsNext { get; set; } 

        internal async Task ChangePage(int pageNumber)
        {
            try
            {
                CurrentPage = pageNumber;
                PageNavigators();
                await LoadUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message : "+ex);
            }
            
        }

        internal async Task PreviousPage()
        {
            try
            {
                if (IsPrevious)
                {
                    CurrentPage = CurrentPage - 1;
                    PageNavigators();
                    await LoadUsers();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message : " + ex);
            }
        }

        internal async Task NextPage()
        {
            try
            {
                if (IsNext)
                {
                    CurrentPage = CurrentPage + 1;
                    PageNavigators();
                    await LoadUsers();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message : " + ex);
            }
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

                await LoadUsers();
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
                modified_user.UserName = user.UserName;
                modified_user.Password = user.Password;
                modified_user.ModifiedBy = 1;
                modified_user.ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
                await Context.SaveChangesAsync();
            }

            await LoadUsers();
        }

        internal void HandleCancel(UserDetailDto user)
        {
            user.IsEdit = false;
            Editing = false;
        }

        
    }
}
