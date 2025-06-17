using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.DLL.Interface
{
    public interface IRegisterRepository
    {
        Task CreateUserAsync(UserDetail userDetail);
    }
}
