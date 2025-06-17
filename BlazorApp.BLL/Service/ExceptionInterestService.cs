using BlazorApp.BLL.Interface;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using BlazorApp.Model.Entities.AuthDB.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.BLL.Service
{
    public class ExceptionInterestService : IExceptionInterestService
    {
        private readonly IExceptionInterestRepository _repository;

        public ExceptionInterestService(IExceptionInterestRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ProductDto>> GetAllProductDetails() => _repository.GetAllProductDetails();

        public Task<List<ExceptionInterestDto>> GetExceptionInterestDetails(int ModelId) =>
            _repository.GetExceptionInterestDetails(ModelId);

        public Task<bool> AddEIToDb(ExceptionInterest EI) => _repository.AddEIToDb(EI);

        public Task<bool> EditEIInDb(int ModelId, List<ExceptionInterestDto> ExceptionInterestDetails) =>
            _repository.EditEIInDb(ModelId, ExceptionInterestDetails);

        public Task<bool> SetEIStatus(int Id) => _repository.SetEIStatus(Id);
    }
}
