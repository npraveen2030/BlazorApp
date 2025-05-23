using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

[Keyless]
[Table("MasterTabAssocaition")]
public partial class MasterTabAssocaition
{
    [Column("MTAId")]
    public int Mtaid { get; set; }

    public int GroupId { get; set; }

    public int TabId { get; set; }

    [StringLength(50)]
    public string TabName { get; set; } = null!;
}
