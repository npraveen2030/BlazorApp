using BlazorApp.Model.CustomModels.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.BLL.Interface
{
    public interface IModelsService
    {
        Task<List<ModelDto>> GetAllPricingModels();
        Task<List<TabDto>> GetExceptionInterestDetails();
    }
}
