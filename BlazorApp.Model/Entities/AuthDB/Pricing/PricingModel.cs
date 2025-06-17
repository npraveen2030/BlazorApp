using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Pricing;

[Table("Models", Schema = "Pricing")]
public partial class PricingModel
{
    public int Modelid { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ExceptionInterest> ExceptionInterests { get; set; } = new List<ExceptionInterest>();
}
