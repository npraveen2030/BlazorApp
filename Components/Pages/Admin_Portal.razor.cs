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
        public List<UserDetailDto> AllUsers { get; set; } = new();

        public PaginatedList<UserDetailDto> PaginatedUsers { get; set; } = null!;
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 8;
        public string searchText = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            AllUsers = await Context.UserDetails
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
                .ToListAsync();

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = AllUsers
                .Where(u => string.IsNullOrWhiteSpace(searchText) || u.UserName.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            PaginatedUsers = PaginatedList<UserDetailDto>.CreateFromList(filtered, CurrentPage, PageSize);
            StateHasChanged();
        }

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

        // Pagination handlers
        private void GoToPage(int page)
        {
            CurrentPage = page;
            ApplyFilters();
        }

        private void PreviousPage()
        {
            if (PaginatedUsers.HasPreviousPage)
            {
                CurrentPage--;
                ApplyFilters();
            }
        }

        private void NextPage()
        {
            if (PaginatedUsers.HasNextPage)
            {
                CurrentPage++;
                ApplyFilters();
            }
        }

        // Search handler
        private void SearchUsers()
        {
            CurrentPage = 1;
            ApplyFilters();
        }

        public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }

            public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                AddRange(items);
            }

            public bool HasPreviousPage => PageIndex > 1;
            public bool HasNextPage => PageIndex < TotalPages;

            public static PaginatedList<T> CreateFromList(List<T> sourceList, int pageIndex, int pageSize)
            {
                var count = sourceList.Count;
                var items = sourceList
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }
        }
    }
}
