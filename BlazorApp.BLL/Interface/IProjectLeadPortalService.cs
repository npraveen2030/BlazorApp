using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Personalization;

namespace BlazorApp.BLL.Interface
{
    public interface IProjectLeadPortalService
    {
        Task<(bool, Dictionary<string, GridMeta>)> GridPreferenceFetcher(int id);
        Task<(bool, UserProjectRoleAssociationDto)> FetchUserDetailsFromIdAndRole(int id, int role);
        Task<List<UserProjectRoleAssociationDto>> FetchAssociation(int projectid);
        Task<List<UserProjectRoleAssociationDto>> GetActiveUsersFromProjectId(int projectid);
        Task<List<UserProjectRoleAssociationDto>> GetAvailableRoles();
        Task<bool> AddAssociation(UserProjectRoleAssociationDto AddAssociationForm);
        Task<bool> DeleteAssociation(int AssociationId, int UserId);
        Task<bool> GridPreferenceSaver(Dictionary<string, GridMeta> GridState, int userId, int GroupId, int TabId);
    }
}
