using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.DLL.Interface
{
    public interface ISigninRepository
    {
        Task<UserDetail?> GetByNameAsync(string username);
        Task<List<int>> GetRolesByIdAsync(int id);
    }
}
