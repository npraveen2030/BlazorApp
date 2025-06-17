using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Personalization;

namespace BlazorApp.DLL.Interface
{
    public interface IProjectLeadPortalRepository
    {
        Task<(bool, Dictionary<string, GridMeta>)> GridPreferenceFetcherFromDb(int id);
        Task<(bool, UserProjectRoleAssociationDto)> FetchUserDetailsFromIdAndRole(int id, int role);
        Task<List<UserProjectRoleAssociationDto>> FetchAssociationFromDb(int projectid);
        Task<List<UserProjectRoleAssociationDto>> GetActiveUsersFromProjectId(int projectid);
        Task<List<UserProjectRoleAssociationDto>> GetAvailableRolesFromDb();
        Task<bool> AddAssociationToDb(UserProjectRoleAssociationDto AddAssociationForm);
        Task<bool> DeleteAssociationFromDb(int AssociationId, int UserId);
        Task<bool> GridPreferenceSaverToDb(Dictionary<string, GridMeta> GridState, int userId, int GroupId, int TabId);
    }
}
