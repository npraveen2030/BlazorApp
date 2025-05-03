using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

[Table("UserProjectRoleAssociation")]
public partial class UserProjectRoleAssociation
{
    [Key]
    [Column("upraId")]
    public int UpraId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public int ProjectId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("UserProjectRoleAssociations")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("UserProjectRoleAssociations")]
    public virtual UserRole Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserProjectRoleAssociations")]
    public virtual UserDetail User { get; set; } = null!;
}
