namespace BlazorApp.BLL.Interface
{
    public interface IForgotPasswordService
    {
        Task<bool> AddTokenByName(string UserName, string Email);
        Task<bool> ChangePasswordByToken(string? ResetToken,string Password);
    }
}
