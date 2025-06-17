using BlazorApp.Model.Entities.AuthDB.Core;

namespace BlazorApp.BLL.Interface
{
    public interface IRegisterService
    {
        Task AddUserAsync(UserDetail userDetail);
    }
}
