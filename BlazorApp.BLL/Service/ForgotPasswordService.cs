using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.DLL.Interface;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.BLL.Service
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private IForgotPasswordRepository _repository { get; set; }
        private NavigationManager _navigationManager { get; set; }
        private EmailService _emailService { get; set; }
        public ForgotPasswordService(IForgotPasswordRepository repository, NavigationManager navigationManager,
                                     EmailService emailService)
        {
            _repository = repository;
            _navigationManager = navigationManager;
            _emailService = emailService;
        }

        public async Task<bool> AddTokenByName(string UserName,string Email)
        {
            var token = await _repository.AddTokenToDbByName(UserName);
            if (token != "")
            {
                string resetLink = _navigationManager.BaseUri + $"reset-password?token={token}";
                await _emailService.SendAsync(Email, "Password Reset", $"Click to reset: {resetLink}");
                return true;
            }
            return false;
        }

        public async Task<bool> ChangePasswordByToken(string? ResetToken, string Password)
        {
            if (ResetToken == null || Password == null) 
                return false; 
            var HashedPassword = PasswordHelper.HashPassword(Password);
            var success = await _repository.ChangePasswordInDbByToken(ResetToken, HashedPassword);
            if(success)
            {
                return true;
            }
            return false;
        }
    }
}
