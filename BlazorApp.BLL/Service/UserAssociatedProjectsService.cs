using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;

namespace BlazorApp.BLL.Service
{
    public class UserAssociatedProjectsService : IUserAssociatedProjectsService
    {
        private IUserAssociatedProjectsRepository _repository { get; set; }
        public UserAssociatedProjectsService(IUserAssociatedProjectsRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> GetUserNameFromIdAsync(int id)
        {
            return await _repository.GetNameFromIdAsync(id);
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetAssociatedProjects(int id)
        {
            return await _repository.GetAssociatedProjectsFromDb(id);
        }
    }
}
