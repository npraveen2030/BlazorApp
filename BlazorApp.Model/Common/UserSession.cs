namespace BlazorApp.Model.Common
{
    public class UserSession
    {
        public int UserId { get; set; } 

        public string UserName { get;set;} = "";

        public List<string> UserRoles {  get; set; } = [];
    }
}