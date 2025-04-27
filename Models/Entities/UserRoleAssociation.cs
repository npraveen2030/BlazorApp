using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

public partial class UserRoleAssociation
{
    [Key]
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserRoleAssociations")]
    public virtual UserRole Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserRoleAssociations")]
    public virtual UserDetail User { get; set; } = null!;
}
