using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Core;
using BlazorApp.Model.CustomModels.Pricing;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Entities.AuthDB.Pricing;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DLL.Repository
{
    public class ExceptionInterestRepository : IExceptionInterestRepository
    {
        private AuthDbContext _context { get; set; }
        public ExceptionInterestRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProductDetails()
        {
            return await _context.Products
                                 .Where(p => p.IsActive)
                                 .Select(p => new ProductDto
                                 {
                                     Id = p.ID,
                                     Name = p.Name,
                                     StandardInteresetRate = p.StandardInteresetRate,
                                     AnnualizedStandardInterestExpense = p.AnnualizedStandardInterestExpense,
                                     CurrentInterestRate = p.CurrentInterestRate,
                                     AnnualizedCurrentInterestExpense = p.AnnualizedCurrentInterestExpense,
                                     AppliedInterestRate = p.AppliedInterestRate,
                                     AnnualizedInterestExpense = p.AnnualizedInterestExpense
                                 })
                                 .ToListAsync();
        }

        public async Task<List<ExceptionInterestDto>> GetExceptionInterestDetails(int ModelId)
        {
            return await _context.ExceptionInterests
                                 .Where(EI => EI.Modelid == ModelId && EI.IsActive == true)
                                 .Include(EI => EI.Product)
                                 .Select(EI => new ExceptionInterestDto
                                 {
                                     Id = EI.Id,
                                     Modelid = EI.Modelid,
                                     ProductId = EI.ProductId,
                                     ProductName = EI.Product.Name,
                                     AccountNumber = EI.AccountNumber,
                                     AverageCollectedBalance = EI.AverageCollectedBalance,
                                     CampaignId = EI.CampaignId,
                                     StandardInterestRate = EI.StandardInterestRate,
                                     AnnualizedStandardIneterestExpense = EI.AnnualizedStandardIneterestExpense,
                                     CurrentInteresetRate = EI.CurrentInteresetRate,
                                     AnnualizedCurrentInteresetExpense = EI.AnnualizedCurrentInteresetExpense,
                                     FloatingId = EI.FloatingId,
                                     FloatingRate = EI.FloatingRate,
                                     BonusId = EI.BonusId,
                                     AppliedInterestExpires = EI.AppliedInterestExpires,
                                     AppliedInterestRate = EI.AppliedInterestRate,
                                     AnnualizedInterestExpense = EI.AnnualizedInterestExpense,
                                     CreatedDate = EI.CreatedDate,
                                     CreatedBy = EI.CreatedBy,
                                     ModifiedDate = EI.ModifiedDate,
                                     ModifiedBy = EI.ModifiedBy,
                                     IsActive = EI.IsActive
                                 })
                                 .ToListAsync();
        }

        public async Task<bool> AddEIToDb(ExceptionInterest EI)
        {
            try
            {
                _context.ExceptionInterests.Add(EI);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditEIInDb(int ModelId, List<ExceptionInterestDto> ExceptionInterestDetails)
        {
            try
            {
                var EDs = await _context.ExceptionInterests.Where(EI => EI.Modelid == ModelId).ToListAsync();

                foreach (var details in ExceptionInterestDetails)
                {
                    var ED = EDs.Where(e => e.Id == details.Id).FirstOrDefault();

                    if (ED != null)
                    {
                        ED.AverageCollectedBalance = details.AverageCollectedBalance;
                        ED.CampaignId = details.CampaignId;
                        ED.FloatingId = details.FloatingId;
                        ED.FloatingRate = details.FloatingRate;
                        ED.BonusId = details.BonusId;
                        ED.AppliedInterestExpires = details.AppliedInterestExpires;
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetEIStatus(int Id)
        {
            var ED = await _context.ExceptionInterests.Where(EI => EI.Id == Id).FirstOrDefaultAsync();

            if (ED != null)
            {
                ED.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
