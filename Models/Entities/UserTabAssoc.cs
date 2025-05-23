using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models.Entities;

[Table("UserTabAssoc")]
public partial class UserTabAssoc
{
    [Key]
    [Column("UTAId")]
    public int Utaid { get; set; }

    public int UserId { get; set; }

    public int TabId { get; set; }

    public int GroupId { get; set; }
}
