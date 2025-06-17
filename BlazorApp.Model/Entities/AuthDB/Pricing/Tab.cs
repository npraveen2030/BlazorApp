using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Pricing;

[Table("Tab", Schema = "Pricing")]
public partial class Tab
{
    public int TabId { get; set; }

    public string TabName { get; set; } = null!;

    public bool IsActive { get; set; }
}
