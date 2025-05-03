using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

public partial class UserRole
{
    [Key]
    public int RoleId { get; set; }

    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    public DateOnly? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    public int RolePriority { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<UserProjectRoleAssociation> UserProjectRoleAssociations { get; set; } = new List<UserProjectRoleAssociation>();
}
