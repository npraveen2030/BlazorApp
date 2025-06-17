using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;


namespace BlazorApp.BLL.Interface
{
    public interface IUserManagerService
    {
        Task<List<UserDetailDto>> GetAllUsers();
        Task<bool> AddUserToDb(UserDetail user);
        Task<bool> EditUserInDb(int UserId, int AdminId, string password, bool status);
        Task<bool> SetUserStatus(int UserId, bool status);
    }
}
