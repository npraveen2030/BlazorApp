using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.BLL.Service
{
    public class RegisterService : IRegisterService
    {
        private IRegisterRepository _repository { get; set; }
        public RegisterService(IRegisterRepository repository)
        {
            _repository = repository;
        }

        public async Task AddUserAsync(UserDetail userDetail)
        {
            await _repository.CreateUserAsync(userDetail);
        }
    }
}
