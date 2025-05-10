using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models.Dtos
{
    public class UserAssociatedProjectsDto
    {
        public int UpraId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public int ProjectId { get; set; }

        public string UserName { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public string ProjectName { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
