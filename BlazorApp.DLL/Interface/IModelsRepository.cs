using BlazorApp.Model.CustomModels.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.DLL.Interface
{
    public interface IModelsRepository
    {
        Task<List<ModelDto>> GetAllPricingModels();
        Task<List<TabDto>> GetExceptionInterestDetails();
    }
}
