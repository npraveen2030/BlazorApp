using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class UserManagerRepository : IUserManagerRepository
    { 
        private AuthDbContext _context { get; set; }
        public UserManagerRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserDetailDto>> GetAllUsers()
        {
            return await _context.UserDetails
                                 .Select(u => new UserDetailDto
                                 {
                                     UserId = u.UserId,
                                     UserName = u.UserName,
                                     Password = "XXXXXXXXX",
                                     CreatedBy = _context.UserDetails
                                                 .Where(c => c.UserId == u.CreatedBy)
                                                 .Select(c => c.UserName)
                                                 .FirstOrDefault(),
                                     CreatedDate = u.CreatedDate,
                                     ModifiedBy = _context.UserDetails
                                                 .Where(c => c.UserId == u.ModifiedBy)
                                                 .Select(c => c.UserName)
                                                 .FirstOrDefault(),
                                     ModifiedDate = u.ModifiedDate,
                                     IsActive = u.IsActive
                                 })
                                 .ToListAsync();
        }

        public async Task<bool> AddUserToDb(UserDetail user)
        {
            try
            {
                _context.UserDetails.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditUserInDb(int UserId,int AdminId,string Hashedpassword,bool status)
        {
            var modified_user = _context.UserDetails.FirstOrDefault(u => u.UserId == UserId);

            if (modified_user != null)
            {
                modified_user.Password = Hashedpassword;
                modified_user.ModifiedBy = AdminId;
                modified_user.ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
                modified_user.IsActive = status;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> SetUserStatus(int UserId,bool status)
        {
            var user = _context.UserDetails.FirstOrDefault(u => u.UserId == UserId);

            if (user != null)
            {
                user.IsActive = status;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
