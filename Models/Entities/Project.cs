using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

public partial class Project
{
    [Key]
    public int ProjectId { get; set; }
    public int UserId { get; set; }

    [StringLength(100)]
    public string? ProjectName { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<UserProjectRoleAssociation> UserProjectRoleAssociations { get; set; } = new List<UserProjectRoleAssociation>();
}
