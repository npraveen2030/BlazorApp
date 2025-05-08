namespace BlazorApp.Session
{
    public class UserSession
    {
        public int UserId { get; set; } 

        public int RoleId {  get; set; }

        public bool IsAuthenticated { get; set; } = false;
    }
}