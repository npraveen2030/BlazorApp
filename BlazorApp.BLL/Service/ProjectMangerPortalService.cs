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
    public class ProjectMangerPortalService : IProjectManagerPortalService
    {
        private IProjectManagerPortalRepository _repository { get; set; }
        public ProjectMangerPortalService(IProjectManagerPortalRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> GetUserByIdAsync(int userId)
        {
            return await _repository.GetUserByIdFromDbAsync(userId);
        }

        public async Task<List<UserProjectRoleAssociationDto>> FetchAssociations(int id)
        {
            return await _repository.FetchAssociationsFromDb(id);
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetActiveUserFromId(int id)
        {
            return await _repository.GetActiveUserFromId(id);
        }

        public async Task<List<int>> GetAssociatedUsers()
        {
            return await _repository.GetassociatedUsers();
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetAvailableProjects(int userId, int roleId)
        {
            return await _repository.GetAvailableProjects(userId, roleId);
        }

        public async Task<List<int>> GetAssociatedProjects(int id)
        {
            return await _repository.GetAssociatedProjects(id);
        }

        public async Task<bool> AddAssociation(UserProjectRoleAssociation newUPRA)
        {
            return await _repository.AddAssociationToDb(newUPRA);
        }

        public async Task<bool> DeleteAssociation(int id)
        {
            return await _repository.DeleteAssociationFromDb(id);
        }
    }
}
