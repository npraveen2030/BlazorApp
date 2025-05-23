using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

[Keyless]
[Table("preferencesMasterTable")]
public partial class PreferencesMasterTable
{
    public int GroupId { get; set; }

    [StringLength(50)]
    public string GroupName { get; set; } = null!;
}
