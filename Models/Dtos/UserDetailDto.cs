using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Dtos
{
    public class UserDetailDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = null!;

        public DateOnly? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateOnly? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        // For Editing the user
        public bool IsEdit { get; set; } = false;

        public bool IsActive { get; set; } = true;

    }
}
