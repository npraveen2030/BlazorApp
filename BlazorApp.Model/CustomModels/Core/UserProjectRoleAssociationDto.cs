using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Model.CustomModels.Core
{
    public class UserProjectRoleAssociationDto
    {
        public int UpraId { get; set; }

        [Required(ErrorMessage = "User selection is required.")]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Role selection is required.")]
        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Project selection is required.")]
        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }

        public string UserName { get; set; } = "";

        public string RoleName { get; set; } = "";

        public string ProjectName { get; set; } = "";

        public DateOnly? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateOnly? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
