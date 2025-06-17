using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Personalization;

[Table("MasterTabAssocaition", Schema = "Personalization")]
public partial class MasterTabAssocaition
{
    public int MTAId { get; set; }

    public int GroupId { get; set; }

    public int TabId { get; set; }

    public string TabName { get; set; } = null!;
}
