using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

[Table("UserGridAssoc")]
public partial class UserGridAssoc
{
    [Key]
    [Column("UGAId")]
    public int Ugaid { get; set; }

    public int UserId { get; set; }

    public int GroupId { get; set; }

    public int TabId { get; set; }

    public string? Preferences { get; set; }
}
