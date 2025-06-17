using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Personalization;

[Table("UserTabAssoc", Schema = "Personalization")]
public partial class UserTabAssoc
{
    public int UTAId { get; set; }

    public int UserId { get; set; }

    public int TabId { get; set; }

    public int GroupId { get; set; }
}
