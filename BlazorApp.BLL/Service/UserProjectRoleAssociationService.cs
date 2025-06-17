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
    public class UserProjectRoleAssociationService : IUserProjectRoleAssociationService
    {
        private readonly IUserProjectRoleAssociationRepository _repository;

        public UserProjectRoleAssociationService(IUserProjectRoleAssociationRepository repository)
        {
            _repository = repository;
        }

        public Task<List<UserProjectRoleAssociationDto>> GetAllUpras() =>
            _repository.GetAllUpras();

        public Task<List<UserProjectRoleAssociationDto>> GetActiveUsers() =>
            _repository.GetActiveUsers();

        public Task<List<UserProjectRoleAssociationDto>> GetActiveProjects() =>
            _repository.GetActiveProjects();

        public Task<List<int>> GetAssociatedProjects() =>
            _repository.GetAssociatedProjects();

        public Task<bool> AddUpraToDb(UserProjectRoleAssociation upra) =>
            _repository.AddUpraToDb(upra);

        public Task<bool> SetUpraStatus(int UpraId, bool status) =>
            _repository.SetUpraStatus(UpraId, status);
    }
}
