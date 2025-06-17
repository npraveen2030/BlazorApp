using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Pricing;

[Table("ExceptionInterest", Schema = "Pricing")]
public partial class ExceptionInterest
{
    public int Id { get; set; }

    public int Modelid { get; set; }

    public int ProductId { get; set; }

    public string? AccountNumber { get; set; }

    public decimal? AverageCollectedBalance { get; set; }

    public int? CampaignId { get; set; }

    public decimal? StandardInterestRate { get; set; }

    public decimal? AnnualizedStandardIneterestExpense { get; set; }

    public decimal? CurrentInteresetRate { get; set; }

    public decimal? AnnualizedCurrentInteresetExpense { get; set; }

    public int? FloatingId { get; set; }

    public decimal? FloatingRate { get; set; }

    public int? BonusId { get; set; }

    public DateTime? AppliedInterestExpires { get; set; }

    public decimal? AppliedInterestRate { get; set; }

    public decimal? AnnualizedInterestExpense { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual PricingModel Model { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
