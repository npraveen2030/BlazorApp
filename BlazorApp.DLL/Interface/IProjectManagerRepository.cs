using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.DLL.Interface
{
    public interface IProjectManagerRepository
    {
        Task<List<ProjectDto>> GetAllProjects();
        Task<bool> AddProjectToDb(Project project);
        Task<bool> EditUserInDb(int ProjectId, int AdminId, string ProjectName, bool status);
        Task<bool> SetProjectStatus(int ProjectId, bool status);
    }
}
