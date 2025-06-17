using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model.CustomModels.Pricing
{
    public class ProductDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? StandardInteresetRate { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AnnualizedStandardInterestExpense { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CurrentInterestRate { get; set; }

        [StringLength(10)]
        public string? AnnualizedCurrentInterestExpense { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AppliedInterestRate { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal? AnnualizedInterestExpense { get; set; }
    }
}
