using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.BLL.Interface
{
    public interface IProjectManagerPortalService
    {
        Task<string> GetUserByIdAsync(int userId);
        Task<List<UserProjectRoleAssociationDto>> FetchAssociations(int id);
        Task<List<UserProjectRoleAssociationDto>> GetActiveUserFromId(int id);
        Task<List<int>> GetAssociatedUsers();
        Task<List<UserProjectRoleAssociationDto>> GetAvailableProjects(int userId, int roleId);
        Task<List<int>> GetAssociatedProjects(int id);
        Task<bool> AddAssociation(UserProjectRoleAssociation newUPRA);
        Task<bool> DeleteAssociation(int id);
    }
}
