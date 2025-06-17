using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.BLL.Service
{
    public class ModelsService : IModelsService
    {
        private readonly IModelsRepository _repository;

        public ModelsService(IModelsRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ModelDto>> GetAllPricingModels() =>
            _repository.GetAllPricingModels();

        public Task<List<TabDto>> GetExceptionInterestDetails() =>
            _repository.GetExceptionInterestDetails();
    }
}
