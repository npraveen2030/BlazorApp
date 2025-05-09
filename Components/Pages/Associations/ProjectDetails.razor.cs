using System;
using BlazorApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Components.Pages.Associations
{
    public partial class ProjectDetails : ComponentBase
    {
        [Parameter] public int ProjectId { get; set; }
        [Inject] internal AuthDbContext Context { get; set; } = null!;

        RadzenDataGrid<UserAssociatedProjectsDto> ordersGrid = null!;
        IEnumerable<UserDetailDto> Users = null!;
        IEnumerable<ProjectDto> Project = null!;
        IEnumerable<UserRoleDto> Roles = null!;

        DataGridEditMode editMode = DataGridEditMode.Single;

        List<UserAssociatedProjectsDto> ordersToInsert = new List<UserAssociatedProjectsDto>();
        List<UserAssociatedProjectsDto> ordersToUpdate = new List<UserAssociatedProjectsDto>();

        void Reset()
        {
            ordersToInsert.Clear();
            ordersToUpdate.Clear();
        }

        void Reset(UserAssociatedProjectsDto order)
        {
            ordersToInsert.Remove(order);
            ordersToUpdate.Remove(order);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            customers = dbContext.Customers;
            employees = dbContext.Employees;

            orders = dbContext.Orders.Include("Customer").Include("Employee");
        }

        async Task EditRow(Order order)
        {
            if (!ordersGrid.IsValid) return;

            if (editMode == DataGridEditMode.Single)
            {
                Reset();
            }

            ordersToUpdate.Add(order);
            await ordersGrid.EditRow(order);
        }

        void OnUpdateRow(Order order)
        {
            Reset(order);
            dbContext.Update(order);

            dbContext.SaveChanges();
        }

        async Task SaveRow(Order order)
        {
            await ordersGrid.UpdateRow(order);
        }

        void CancelEdit(Order order)
        {
            Reset(order);

            ordersGrid.CancelEditRow(order);

            var orderEntry = dbContext.Entry(order);
            if (orderEntry.State == EntityState.Modified)
            {
                orderEntry.CurrentValues.SetValues(orderEntry.OriginalValues);
                orderEntry.State = EntityState.Unchanged;
            }
        }

        async Task DeleteRow(Order order)
        {
            Reset(order);

            if (orders.Contains(order))
            {
                dbContext.Remove<Order>(order);

                dbContext.SaveChanges();

                await ordersGrid.Reload();
            }
            else
            {
                ordersGrid.CancelEditRow(order);
                await ordersGrid.Reload();
            }
        }

        async Task InsertRow()
        {
            if (!ordersGrid.IsValid) return;

            if (editMode == DataGridEditMode.Single)
            {
                Reset();
            }

            var order = new Order();
            ordersToInsert.Add(order);
            await ordersGrid.InsertRow(order);
        }

        async Task InsertAfterRow(Order row)
        {
            if (!ordersGrid.IsValid) return;

            if (editMode == DataGridEditMode.Single)
            {
                Reset();
            }

            var order = new Order();
            ordersToInsert.Add(order);
            await ordersGrid.InsertAfterRow(order, row);
        }

        void OnCreateRow(Order order)
        {
            dbContext.Add(order);

            dbContext.SaveChanges();

            ordersToInsert.Remove(order);
        }
    }

} 