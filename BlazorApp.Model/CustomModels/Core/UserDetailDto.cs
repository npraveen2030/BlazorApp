using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model.CustomModels.Core
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

        public bool IsEdit { get; set; } = false;

        public bool IsActive { get; set; } = true;

    }

    public class ConfirmPasswordDto : UserDetailDto
    {
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get;set; } = null!;
    }
}
