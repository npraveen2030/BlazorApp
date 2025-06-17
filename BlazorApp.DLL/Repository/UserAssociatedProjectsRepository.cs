using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class UserAssociatedProjectsRepository : IUserAssociatedProjectsRepository
    {
        private AuthDbContext _context { get; set; }
        public UserAssociatedProjectsRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<string> GetNameFromIdAsync(int id)
        {
            return await _context.UserDetails
                                 .Where(u => u.UserId == id)
                                 .Select(u => u.UserName)
                                 .FirstOrDefaultAsync() ?? "";
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetAssociatedProjectsFromDb(int id)
        {
            return await _context.UserProjectRoleAssociations
                                 .Where(a => a.IsActive && a.UserId == id)
                                 .Join(_context.Projects,
                                       assoc => assoc.ProjectId,
                                       p => p.ProjectId,
                                       (assoc,p)=> new UserProjectRoleAssociationDto
                                       {
                                           UserId = assoc.UserId,
                                           RoleId = assoc.RoleId,
                                           ProjectId = assoc.ProjectId,
                                           ProjectName = p.ProjectName ?? ""
                                       })
                                 .ToListAsync();
        }
    }
}
