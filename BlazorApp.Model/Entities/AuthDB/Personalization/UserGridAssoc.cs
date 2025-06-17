using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Personalization;

[Table("UserGridAssoc", Schema = "Personalization")]
public partial class UserGridAssoc
{
    public int UGAId { get; set; }

    public int UserId { get; set; }

    public int GroupId { get; set; }

    public int TabId { get; set; }

    public string? Preferences { get; set; }
}
