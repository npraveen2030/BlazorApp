using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.Entities.AuthDB.Pricing;

[Table("Product", Schema = "Pricing")]
public partial class Product
{
    public int ID { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public decimal? StandardInteresetRate { get; set; }

    public decimal? AnnualizedStandardInterestExpense { get; set; }

    public decimal? CurrentInterestRate { get; set; }

    public string? AnnualizedCurrentInterestExpense { get; set; }

    public decimal? AppliedInterestRate { get; set; }

    public decimal? AnnualizedInterestExpense { get; set; }

    public virtual ICollection<ExceptionInterest> ExceptionInterests { get; set; } = new List<ExceptionInterest>();
}
