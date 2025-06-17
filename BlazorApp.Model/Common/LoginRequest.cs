namespace BlazorApp.Model.Common
{
    public class LoginRequest
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = null!;
    }
}
