using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.BLL.Interface
{
    public interface IAdminPortalService
    {
        Task<int> GetUserPreferenceTabById(int id);
        Task<bool> SaveUserPreferences(int UserId, int TabId);
    }
}
