using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Personalization;

[Table("preferencesMasterTable", Schema = "Personalization")]
public partial class preferencesMasterTable
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;
}
