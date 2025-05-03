using System.ComponentModel.DataAnnotations;
using BlazorApp.Models.Entities;

namespace BlazorApp.Models.Dtos;

public partial class ProjectDto
{
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Project name is required")]
    public string? ProjectName { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public string? CreatedBy { get; set; } 

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; } = true;
    
    public bool IsEdit {get ; set;} = false;
}
