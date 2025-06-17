using BlazorApp.Model.CustomModels.Core;

namespace BlazorApp.BLL.Interface
{
    public interface IUserAssociatedProjectsService
    {
        Task<string> GetUserNameFromIdAsync(int id);
        Task<List<UserProjectRoleAssociationDto>> GetAssociatedProjects(int id);
    }
}
