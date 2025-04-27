namespace BlazorApp.Session
{
    public class UserSession
    {
        public string UserName { get; set; } = null!;
        public int UserRoleId { get; set; } = 4;
        public bool IsAuthenticated { get; set; } = false;
    }
}