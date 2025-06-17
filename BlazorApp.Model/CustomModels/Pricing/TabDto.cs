using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model.CustomModels.Pricing
{
    public class TabDto
    {
        public int TabId { get; set; }

        public string TabName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
