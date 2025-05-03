namespace BlazorApp.Models.Dtos
{
    public class ActiveUsers
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
    }

    public class ActiveProjects
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
    }

    public class UPRAForm
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public int ProjectId { get; set; }
    }

    public class UserProjectRoleAssociationDto
    {
        public int UpraId { get; set; }

        public string UserName { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public string ProjectName { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}