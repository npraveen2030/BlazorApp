using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private AuthDbContext _context { get; set; }
        public ProjectManagerRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProjectDto>> GetAllProjects()
        {
            return await _context.Projects
                                 .Select(u => new ProjectDto
                                 {
                                     ProjectId = u.ProjectId,
                                     ProjectName = u.ProjectName,
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
                                     IsActive = u.isActive ?? false
                                 })
                                 .ToListAsync();
        }

        public async Task<bool> AddProjectToDb(Project project)
        {
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditUserInDb(int ProjectId, int AdminId, string ProjectName, bool status)
        {
            var modified_project = _context.Projects.FirstOrDefault(u => u.ProjectId == ProjectId);

            if (modified_project != null)
            {
                modified_project.ProjectName = ProjectName;
                modified_project.ModifiedBy = AdminId;
                modified_project.ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
                modified_project.isActive = status;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> SetProjectStatus(int ProjectId, bool status)
        {
            var project = _context.Projects.FirstOrDefault(u => u.ProjectId == ProjectId);

            if (project != null)
            {
                project.isActive = status;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
