using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class AdminPortalRepository : IAdminPortalRepository
    {
        private AuthDbContext _context { get; set; }
        public AdminPortalRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetUserPreferenceTabById(int id)
        {
            var item = await _context.UserTabAssocs.Where(uta => uta.UserId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                return item.TabId;
            }
            return 0;
        }

        public async Task<bool> SaveUserPreferences(int UserId,int TabId)
        {
            var user = await _context.UserTabAssocs.Where(uta => uta.UserId == UserId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.TabId = TabId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
