using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;

namespace BlazorApp.BLL.Service
{
    public class AdminPortalService : IAdminPortalService
    {
        private IAdminPortalRepository _repository { get; set; }
        public AdminPortalService(IAdminPortalRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetUserPreferenceTabById(int id)
        {
            return await _repository.GetUserPreferenceTabById(id);
        }

        public async Task<bool> SaveUserPreferences(int UserId, int TabId)
        {
            return await _repository.SaveUserPreferences(UserId, TabId);
        }
    }
}
