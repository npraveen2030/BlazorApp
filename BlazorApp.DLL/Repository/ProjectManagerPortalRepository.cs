using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class ProjectManagerPortalRepository : IProjectManagerPortalRepository
    {
        private AuthDbContext _context { get; set; }
        public ProjectManagerPortalRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<string> GetUserByIdFromDbAsync(int id)
        {
            return await _context.UserDetails
                                 .Where(u => u.UserId == id)
                                 .Select(u => u.UserName)
                                 .FirstOrDefaultAsync() ?? "";
        }

        public async Task<List<UserProjectRoleAssociationDto>> FetchAssociationsFromDb(int id)
        {
            return await _context.UserProjectRoleAssociations
                                 .Where(assoc => assoc.CreatedBy == id
                                                              && assoc.RoleId == 12)
                                 .Include(assoc => assoc.User)
                                 .Include(assoc => assoc.Project)
                                 .Select(assoc => new UserProjectRoleAssociationDto
                                 {
                                     UpraId = assoc.upraId,
                                     UserId = assoc.UserId,
                                     ProjectId = assoc.ProjectId,
                                     RoleId = assoc.RoleId,
                                     UserName = assoc.User.UserName,
                                     ProjectName = assoc.Project.ProjectName ?? "",
                                     CreatedDate = assoc.CreatedDate,
                                     CreatedBy = _context.UserDetails
                                                        .Where(u => u.UserId == assoc.CreatedBy)
                                                        .Select(u => u.UserName)
                                                        .FirstOrDefault(),
                                     ModifiedDate = assoc.ModifiedDate,
                                     ModifiedBy = _context.UserDetails
                                                        .Where(u => u.UserId == assoc.ModifiedBy)
                                                        .Select(u => u.UserName)
                                                        .FirstOrDefault(),
                                     IsActive = assoc.IsActive,

                                 })
                                 .ToListAsync();
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetActiveUserFromId(int id)
        {
            return await _context.UserDetails
                                 .Where(u => u.IsActive
                                          && u.UserId != id)
                                 .Select(u => new UserProjectRoleAssociationDto
                                 {
                                     UserId = u.UserId,
                                     UserName = u.UserName
                                 })
                                 .ToListAsync();
        }

        public async Task<List<int>> GetassociatedUsers()
        {
            return await _context.UserProjectRoleAssociations
                                 .Where(assoc => assoc.RoleId == 12
                                              && assoc.IsActive)
                                 .Select(assoc => assoc.UserId)
                                 .ToListAsync();
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetAvailableProjects(int UserId, int RoleId)
        {
            return await _context.UserProjectRoleAssociations
                                 .Where(assoc => assoc.UserId == UserId
                                              && assoc.RoleId == RoleId
                                              && assoc.IsActive)
                                 .Include(assoc => assoc.Project)
                                 .Select(assoc => new UserProjectRoleAssociationDto
                                 {
                                     ProjectId = assoc.ProjectId,
                                     ProjectName = assoc.Project.ProjectName ?? ""
                                 })
                                 .ToListAsync();
        }

        public async Task<List<int>> GetAssociatedProjects(int id)
        {
            return await _context.UserProjectRoleAssociations
                                 .Where(assoc => assoc.RoleId == 12
                                              && assoc.CreatedBy == id
                                              && assoc.IsActive)
                                 .Select(assoc => assoc.ProjectId)
                                 .ToListAsync();
        }

        public async Task<bool> AddAssociationToDb(UserProjectRoleAssociation newUPRA)
        {
            try
            {
                _context.UserProjectRoleAssociations.Add(newUPRA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAssociationFromDb(int id)
        {
            var deletedAssociation = await _context.UserProjectRoleAssociations
                                                   .Where(assoc => assoc.upraId == id)
                                                   .FirstOrDefaultAsync();

            if (deletedAssociation != null)
            {
                deletedAssociation.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
