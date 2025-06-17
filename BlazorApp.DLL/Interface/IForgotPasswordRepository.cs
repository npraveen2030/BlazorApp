namespace BlazorApp.DLL.Interface
{
    public interface IForgotPasswordRepository
    {
        Task<string> AddTokenToDbByName(string UserName);
        Task<bool> ChangePasswordInDbByToken(string ResetToken, string Password);
    }
}
