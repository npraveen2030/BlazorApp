using BlazorApp.Model.Common;
using BlazorApp.Model.CustomModels.Core;

namespace BlazorApp.DLL.Interface
{
    public interface IUserAssociatedProjectsRepository
    {
        Task<string> GetNameFromIdAsync(int id);
        Task<List<UserProjectRoleAssociationDto>> GetAssociatedProjectsFromDb(int id);
    }
}
