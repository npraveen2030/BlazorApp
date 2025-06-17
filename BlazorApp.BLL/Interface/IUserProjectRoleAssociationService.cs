using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.BLL.Interface
{
    public interface IUserProjectRoleAssociationService
    {
        Task<List<UserProjectRoleAssociationDto>> GetAllUpras();
        Task<List<UserProjectRoleAssociationDto>> GetActiveUsers();
        Task<List<UserProjectRoleAssociationDto>> GetActiveProjects();
        Task<List<int>> GetAssociatedProjects();
        Task<bool> AddUpraToDb(UserProjectRoleAssociation upra);
        Task<bool> SetUpraStatus(int UpraId, bool status);
    }
}
