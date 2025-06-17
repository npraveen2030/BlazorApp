using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;

namespace BlazorApp.BLL.Service
{
    public class SigninService : ISigninService
    {
        private ISigninRepository _repository { get;set;}
        public SigninService(ISigninRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserDetail?> GetUserByNameAsync(string userName)
        {
            return await _repository.GetByNameAsync(userName);
        }

        public async Task<List<int>> GetUserAssociatedRolesByIdAsync(int id)
        {
            return await _repository.GetRolesByIdAsync(id);
        }
    }
}
