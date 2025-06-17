using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.Entities.AuthDB.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class SigninRepository : ISigninRepository
    {
        private AuthDbContext _context { get;set;}
        public SigninRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<UserDetail?> GetByNameAsync(string username)
        {
              return await _context.UserDetails.FirstOrDefaultAsync(u => u.UserName == username);  
        }

        public async Task<List<int>> GetRolesByIdAsync(int id)
        {
            return await _context.UserProjectRoleAssociations
                                 .Where(assoc => assoc.UserId == id && assoc.IsActive)
                                 .Select(assoc => assoc.RoleId)
                                 .Distinct()
                                 .ToListAsync();
        }
    }
}
