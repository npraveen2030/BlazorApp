using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.DLL.Interface
{
    public interface IProjectManagerPortalRepository
    {
        Task<string> GetUserByIdFromDbAsync(int id);
        Task<List<UserProjectRoleAssociationDto>> FetchAssociationsFromDb(int id);
        Task<List<UserProjectRoleAssociationDto>> GetActiveUserFromId(int id);
        Task<List<int>> GetassociatedUsers();
        Task<List<UserProjectRoleAssociationDto>> GetAvailableProjects(int UserId, int RoleId);
        Task<List<int>> GetAssociatedProjects(int id);
        Task<bool> AddAssociationToDb(UserProjectRoleAssociation newUPRA);
        Task<bool> DeleteAssociationFromDb(int id);
    }
}
