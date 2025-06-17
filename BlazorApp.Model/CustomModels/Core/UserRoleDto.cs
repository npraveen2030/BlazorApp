using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model.CustomModels.Core
{
    public class UserRoleDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public DateOnly? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateOnly? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public int RolePriority { get; set; }

    }
}
