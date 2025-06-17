using BlazorApp.BLL.Interface;
using BlazorApp.BLL.Utilities;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.BLL.Service
{
    public class UserManagerService : IUserManagerService
    {
        private IUserManagerRepository _repository { get; set; }
        public UserManagerService(IUserManagerRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<UserDetailDto>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }

        public async Task<bool> AddUserToDb(UserDetail user)
        {
            return await _repository.AddUserToDb(user);
        }

        public async Task<bool> EditUserInDb(int UserId, int AdminId, string password, bool status)
        {
            var Hashedpassword = PasswordHelper.HashPassword(password);

            return await _repository.EditUserInDb(UserId, AdminId, Hashedpassword, status);
        }

        public async Task<bool> SetUserStatus(int UserId, bool status)
        {
            return await _repository.SetUserStatus(UserId, status);
        }
    }
}
