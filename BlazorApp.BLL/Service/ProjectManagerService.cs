using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.BLL.Service
{
    public class ProjectManagerService : IProjectManagerService
    {
        private readonly IProjectManagerRepository _repository;

        public ProjectManagerService(IProjectManagerRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ProjectDto>> GetAllProjects() =>
            _repository.GetAllProjects();

        public Task<bool> AddProjectToDb(Project project) =>
            _repository.AddProjectToDb(project);

        public Task<bool> EditUserInDb(int ProjectId, int AdminId, string ProjectName, bool status) =>
            _repository.EditUserInDb(ProjectId, AdminId, ProjectName, status);

        public Task<bool> SetProjectStatus(int ProjectId, bool status) =>
            _repository.SetProjectStatus(ProjectId, status);
    }
}
