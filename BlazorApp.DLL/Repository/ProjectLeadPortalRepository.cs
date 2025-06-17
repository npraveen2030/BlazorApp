using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Personalization;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Entities.AuthDB.Personalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace BlazorApp.DLL.Repository
{
    public class ProjectLeadPortalRepository : IProjectLeadPortalRepository
    {
        private AuthDbContext _context { get; set; }
        public ProjectLeadPortalRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<(bool, Dictionary<string, GridMeta>)> GridPreferenceFetcherFromDb(int id)
        {
            var GridStatejson = await _context.UserGridAssocs
                                         .Where(ugs => ugs.UserId == id
                                                          && ugs.GroupId == 2
                                                          && ugs.TabId == 2)
                                         .Select(ugs => ugs.Preferences)
                                         .FirstOrDefaultAsync();

            if (GridStatejson is not null)
            {
                var deserialized = JsonSerializer.Deserialize<Dictionary<string, GridMeta>>(GridStatejson);
                if (deserialized is not null)
                {
                    return (true, deserialized);
                }

                return (false, new Dictionary<string, GridMeta>());
            }

            return (false, new Dictionary<string, GridMeta>());
        }

        public async Task<(bool, UserProjectRoleAssociationDto)> FetchUserDetailsFromIdAndRole(int id,int role)
        {
            var details = await _context.UserProjectRoleAssociations
                                          .Where(assoc => assoc.UserId == id
                                                       && assoc.RoleId == role)
                                          .Join(_context.UserDetails,a => a.UserId,u => u.UserId,
                                                (a,u) => new {assoc = a,user = u})
                                          .Join(_context.UserRoles,au => au.assoc.RoleId,r => r.RoleId,
                                                (au,r) => new {assoc = au.assoc,user = au.user,role = r })
                                          .Join(_context.Projects,aur => aur.assoc.ProjectId,p => p.ProjectId,
                                                (aur,p) => new UserProjectRoleAssociationDto
                                                {
                                                    UserName = aur.user.UserName ?? "",
                                                    RoleName = aur.role.RoleName ?? "",
                                                    ProjectId = p.ProjectId,
                                                    ProjectName = p.ProjectName ?? ""
                                                })
                                          .FirstOrDefaultAsync();
            if (details != null)
                return (true, details);
            return (false, new());
        }

        public async Task<List<UserProjectRoleAssociationDto>> FetchAssociationFromDb(int projectid)
        {
            return await _context.UserProjectRoleAssociations
                                        .Where(assoc => assoc.ProjectId == projectid
                                                     && assoc.RoleId != 11
                                                     && assoc.RoleId != 12)
                                        .Include(assoc => assoc.User)
                                        .Include(assoc => assoc.Role)
                                        .Select(assoc => new UserProjectRoleAssociationDto
                                        {
                                            UpraId = assoc.upraId,
                                            UserId = assoc.UserId,
                                            ProjectId = assoc.ProjectId,
                                            RoleId = assoc.RoleId,
                                            UserName = assoc.User.UserName,
                                            RoleName = assoc.Role.RoleName ?? "",
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
        public async Task<List<UserProjectRoleAssociationDto>> GetActiveUsersFromProjectId(int projectid)
        {
            var ActiveUsers = await _context.UserDetails
                                            .Where(u => u.IsActive)
                                            .Select(u => new UserProjectRoleAssociationDto
                                            {
                                                UserId = u.UserId,
                                                UserName = u.UserName
                                            })
                                            .ToListAsync();

            var ProjectAssociatedAuthorities = await _context.UserProjectRoleAssociations
                                                             .Where(assoc => assoc.ProjectId == projectid
                                                                          && assoc.IsActive)
                                                             .Select(assoc => assoc.UserId)
                                                             .ToListAsync();

            return ActiveUsers.Where(a => !ProjectAssociatedAuthorities.Contains(a.UserId)).ToList();
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetAvailableRolesFromDb()
        {
            return await _context.UserRoles
                                 .Where(assoc => assoc.isActive
                                              && assoc.RoleId != 1
                                              && assoc.RoleId != 11
                                              && assoc.RoleId != 12)
                                 .Select(assoc => new UserProjectRoleAssociationDto
                                 {
                                     RoleId = assoc.RoleId,
                                     RoleName = assoc.RoleName ?? ""
                                 })
                                 .ToListAsync();
        }

        public async Task<bool> AddAssociationToDb(UserProjectRoleAssociationDto AddAssociationForm)
        {
            try
            {
                var newUPRA = new UserProjectRoleAssociation
                {
                    UserId = AddAssociationForm.UserId,
                    RoleId = AddAssociationForm.RoleId,
                    ProjectId = AddAssociationForm.ProjectId,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = Convert.ToInt32(AddAssociationForm.CreatedBy),
                    IsActive = true
                };

                _context.UserProjectRoleAssociations.Add(newUPRA);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAssociationFromDb(int AssociationId,int UserId)
        {
            var deletedAssociation = await _context.UserProjectRoleAssociations
                                            .Where(assoc => assoc.upraId == AssociationId)
                                            .FirstOrDefaultAsync();

            if (deletedAssociation != null)
            {
                deletedAssociation.ModifiedBy = UserId;
                deletedAssociation.ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
                deletedAssociation.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> GridPreferenceSaverToDb(Dictionary<string, GridMeta> GridState,int userId,int GroupId,int TabId)
        {
            try
            {
                var userpref = await _context.UserGridAssocs
                                            .Where(ugs => ugs.UserId == userId
                                                          && ugs.GroupId == GroupId
                                                          && ugs.TabId == TabId)
                                            .FirstOrDefaultAsync();

                if (userpref == null)
                {
                    var newUGA = new UserGridAssoc
                    {
                        UserId = userId,
                        GroupId = GroupId,
                        TabId = TabId,
                        Preferences = JsonSerializer.Serialize(GridState)
                    };

                    _context.UserGridAssocs.Add(newUGA);
                }
                else
                {
                    userpref.Preferences = JsonSerializer.Serialize(GridState);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
