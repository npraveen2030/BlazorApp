using System;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.UI.Components.Pages.Associations
{
    public partial class ProjectDetails : ComponentBase
    {
        [Parameter] public int ProjectId { get; set; }

    }

} 