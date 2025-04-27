using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Dtos
{
    public class UserDetailDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateOnly? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateOnly? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        // For Editing the user
        public bool IsEdit { get; set; } = false;

        public bool IsActive { get; set; }
    }
}
