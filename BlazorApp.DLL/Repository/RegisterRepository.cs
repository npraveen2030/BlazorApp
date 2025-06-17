using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.DLL.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private AuthDbContext _context { get; set; }
        public RegisterRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(UserDetail userDetail)
        {
            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();
        }
    }
}
