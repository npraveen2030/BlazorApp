using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Core;

[Table("Project", Schema = "Core")]
public partial class Project
{
    public int ProjectId { get; set; }

    public string? ProjectName { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? isActive { get; set; }

    public virtual ICollection<UserProjectRoleAssociation> UserProjectRoleAssociations { get; set; } = new List<UserProjectRoleAssociation>();
}
