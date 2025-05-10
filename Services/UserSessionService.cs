namespace BLazorApp.Services
{
    public class UserSessionService
    {
        public bool IsInitialized { get; private set; } = false;
        public bool IsAuthenticated { get; private set; } = false;
        public string UserName { get; private set; } = string.Empty;

        public void Login(string username)
        {
            // Simulate login
            UserName = username;
            IsAuthenticated = true;
            IsInitialized = true;
        }

        public void Logout()
        {
            UserName = string.Empty;
            IsAuthenticated = false;
            IsInitialized = true;
        }
    }
}