using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Personalization;


namespace BlazorApp.BLL.Service
{
    public class ProjectLeadPortalService : IProjectLeadPortalService
    {
        private IProjectLeadPortalRepository _repository { get; set; }
        public ProjectLeadPortalService(IProjectLeadPortalRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool, Dictionary<string, GridMeta>)> GridPreferenceFetcher(int id)
        {
            return await _repository.GridPreferenceFetcherFromDb(id);   
        }

        public async Task<(bool, UserProjectRoleAssociationDto)> FetchUserDetailsFromIdAndRole(int id, int role)
        {
            return await _repository.FetchUserDetailsFromIdAndRole(id,role);
        }

        public async Task<List<UserProjectRoleAssociationDto>> FetchAssociation(int projectid)
        {
            return await _repository.FetchAssociationFromDb(projectid);
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetActiveUsersFromProjectId(int projectid)
        {
            return await _repository.GetActiveUsersFromProjectId(projectid);
        }

        public async Task<List<UserProjectRoleAssociationDto>> GetAvailableRoles()
        {
            return await _repository.GetAvailableRolesFromDb();
        }

        public async Task<bool> AddAssociation(UserProjectRoleAssociationDto AddAssociationForm)
        {
            var success = await _repository.AddAssociationToDb(AddAssociationForm);
            if (success) 
                return true;
            return false;
        }

        public async Task<bool> DeleteAssociation(int AssociationId, int UserId)
        {
            var success = await _repository.DeleteAssociationFromDb(AssociationId, UserId);
            if (success)
                return true;
            return false;
        }

        public async Task<bool> GridPreferenceSaver(Dictionary<string, GridMeta> GridState, int userId, int GroupId, int TabId)
        {
            var success = await _repository.GridPreferenceSaverToDb(GridState, userId, GroupId, TabId);
            if (success)
                return true;
            return false;
        }
    }
}
