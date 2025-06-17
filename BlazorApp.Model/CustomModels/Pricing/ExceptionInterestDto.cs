using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model.CustomModels.Pricing
{
    public class ExceptionInterestDto
    {
        public int? Id { get; set; }

        public int? Modelid { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; } = "";

        [StringLength(50)]
        public string? AccountNumber { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AverageCollectedBalance { get; set; }

        public int? CampaignId { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? StandardInterestRate { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AnnualizedStandardIneterestExpense { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CurrentInteresetRate { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AnnualizedCurrentInteresetExpense { get; set; }

        public int? FloatingId { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? FloatingRate { get; set; }

        public int? BonusId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? AppliedInterestExpires { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AppliedInterestRate { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AnnualizedInterestExpense { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
