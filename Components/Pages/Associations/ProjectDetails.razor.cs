using System;
using BlazorApp.Models.Dtos;
using BlazorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Components.Pages.Associations
{
    public partial class ProjectDetails : ComponentBase
    {
        [Parameter] public int ProjectId { get; set; }
        [Inject] internal AuthDbContext Context { get; set; } = null!;

        List<UserDetail> Users = null!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Users = await Context.UserDetails.ToListAsync();
        }

    }

} 