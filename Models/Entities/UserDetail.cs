using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

[Index("UserName", Name = "IX_UserDetails", IsUnique = true)]
public partial class UserDetail
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(256)]
    public string Password { get; set; } = null!;

    public DateOnly? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserProjectRoleAssociation> UserProjectRoleAssociations { get; set; } = new List<UserProjectRoleAssociation>();
}
