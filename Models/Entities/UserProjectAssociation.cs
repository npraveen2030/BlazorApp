using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

[Table("UserProjectAssociation")]
public partial class UserProjectAssociation
{
    [Key]
    public int UserProjectId { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("UserProjectAssociations")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserProjectAssociations")]
    public virtual UserDetail User { get; set; } = null!;
}
