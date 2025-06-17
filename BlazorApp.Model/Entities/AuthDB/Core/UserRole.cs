using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Core;

[Table("UserRole", Schema = "Core")]
public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public DateOnly? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public bool isActive { get; set; }

    public int RolePriority { get; set; }

    public virtual ICollection<AdminRoleAssociation> AdminRoleAssociations { get; set; } = new List<AdminRoleAssociation>();

    public virtual ICollection<UserProjectRoleAssociation> UserProjectRoleAssociations { get; set; } = new List<UserProjectRoleAssociation>();
}
