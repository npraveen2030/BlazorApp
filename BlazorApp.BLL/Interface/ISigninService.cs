using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.BLL.Interface
{
    public interface ISigninService
    {
        Task<UserDetail?> GetUserByNameAsync(string userName);
        Task<List<int>> GetUserAssociatedRolesByIdAsync(int id);
    }
}
