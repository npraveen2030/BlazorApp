using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {
        private AuthDbContext _context { get; set; }
        public ForgotPasswordRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddTokenToDbByName(string UserName)
        {
            try
            {
                var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserName == UserName);
                if (user != null)
                {
                    string token = Guid.NewGuid().ToString();
                    user.ResetToken = token;
                    user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1);
                    await _context.SaveChangesAsync();
                    return token;
                }
                else
                {
                    return "";
                }
            }
            catch 
            {
                return "";
            }
        }

        public async Task<bool> ChangePasswordInDbByToken(string ResetToken, string HashedPassword)
        {
            try
            {
                var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.ResetToken == ResetToken &&
                                                                          u.ResetTokenExpiration > DateTime.UtcNow);
                if (user != null)
                {
                    user.Password = HashedPassword;
                    user.ResetToken = null;
                    user.ResetTokenExpiration = null;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
