using BlazorApp.Model.CustomModels.Pricing;
using BlazorApp.Model.Entities.AuthDB.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.DLL.Interface
{
    public interface IExceptionInterestRepository
    {
        Task<List<ProductDto>> GetAllProductDetails();
        Task<List<ExceptionInterestDto>> GetExceptionInterestDetails(int ModelId);
        Task<bool> AddEIToDb(ExceptionInterest EI);
        Task<bool> EditEIInDb(int ModelId, List<ExceptionInterestDto> ExceptionInterestDetails);
        Task<bool> SetEIStatus(int Id);
    }
}
