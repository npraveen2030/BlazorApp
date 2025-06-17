using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class UserProjectRoleAssociationRepository : IUserProjectRoleAssociationRepository
    {
        private AuthDbContext _context { get; set; }
        public UserProjectRoleAssociationRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserProjectRoleAssociationDto>> GetAllUpras()
        {
            return await _context.UserProjectRoleAssociations
                                 .Include(a => a.User)
                                 .Include(a => a.Project)
                                 .Include(a => a.Role)
                                 .Select(u => new UserProjectRoleAssociationDto
                                 {
                                     UpraId = u.upraId,
                                     UserName = u.User.UserName,
                                     RoleName = u.Role.RoleName,
                                     ProjectName = u.Project.ProjectName ?? "-",
                                     CreatedBy = _context.UserDetails
                                        .Where(c => c.UserId == u.CreatedBy)
                                        .Select(c => c.UserName)
                                        .FirstOrDefault() ?? "-",
                                     CreatedDate = u.CreatedDate,
                                     ModifiedBy = _context.UserDetails
                                        .Where(c => c.UserId == u.ModifiedBy)
                                        .Select(c => c.UserName)
                                        .FirstOrDefault() ?? "-",
                                     ModifiedDate = u.ModifiedDate,
                                     IsActive = u.IsActive
                                 })
                                 .ToListAsync();
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetActiveUsers()
        {
            return await _context.UserDetails
                                 .Where(u => u.IsActive && u.UserId != 1)
                                 .Select(u => new UserProjectRoleAssociationDto
                                 {
                                     UserId = u.UserId,
                                     UserName = u.UserName
                                 })
                                 .ToListAsync();
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetActiveProjects()
        {
            return await _context.Projects
                                 .Where(p => p.ProjectName != null)
                                 .Select(p => new UserProjectRoleAssociationDto
                                 {
                                     ProjectId = p.ProjectId,
                                     ProjectName = p.ProjectName ?? ""
                                 })
                                 .ToListAsync();
        }

        

        public async Task<List<int>> GetAssociatedProjects()
        {
            return await _context.UserProjectRoleAssociations
                                                  .Where(a => a.RoleId == 11
                                                           && a.IsActive)
                                                  .Select(a => a.ProjectId)
                                                  .ToListAsync();
        }

        public async Task<bool> AddUpraToDb(UserProjectRoleAssociation upra)
        {
            try
            {
                _context.UserProjectRoleAssociations.Add(upra);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetUpraStatus(int UpraId, bool status)
        {
            var upra = _context.UserProjectRoleAssociations.FirstOrDefault(u => u.upraId == UpraId);

            if (upra != null)
            {
                upra.IsActive = status;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
