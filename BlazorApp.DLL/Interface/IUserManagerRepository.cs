using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.DLL.Interface
{
    public interface IUserManagerRepository
    {
        Task<List<UserDetailDto>> GetAllUsers();
        Task<bool> AddUserToDb(UserDetail user);
        Task<bool> EditUserInDb(int UserId, int AdminId, string Hashedpassword, bool status);
        Task<bool> SetUserStatus(int UserId, bool status);
    }
}
