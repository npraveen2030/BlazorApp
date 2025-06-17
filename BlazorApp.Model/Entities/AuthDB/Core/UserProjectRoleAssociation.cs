using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Core;

[Table("UserProjectRoleAssociation", Schema = "Core")]
public partial class UserProjectRoleAssociation
{
    public int upraId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public int ProjectId { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual UserRole Role { get; set; } = null!;

    public virtual UserDetail User { get; set; } = null!;
}
