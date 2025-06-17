using BlazorApp.DLL.DBContext;
using BlazorApp.DLL.Interface;
using BlazorApp.Model.CustomModels.Pricing;
using BlazorApp.Model.Entities.AuthDB.Pricing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.DLL.Repository
{
    public class ModelsRepository : IModelsRepository
    {
        private AuthDbContext _context { get; set; }
        public ModelsRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<List<ModelDto>> GetAllPricingModels()
        {
            return await _context.Models
                                  .Where(m => m.IsActive)
                                  .Select(m => new ModelDto
                                  {
                                      Modelid = m.Modelid,
                                      Name = m.Name,
                                      CreatedDate = m.CreatedDate,
                                      CreatedBy = m.CreatedBy,
                                      ModifiedDate = m.ModifiedDate,
                                      ModifiedBy = m.ModifiedBy,
                                      IsActive = m.IsActive
                                  })
                                  .ToListAsync();
        }

        public async Task<List<TabDto>> GetExceptionInterestDetails()
        {
            return await _context.Tabs
                                .Where(t => t.IsActive)
                                .Select(t => new TabDto
                                {
                                    TabId = t.TabId,
                                    TabName = t.TabName,
                                    IsActive = t.IsActive,
                                })
                                .ToListAsync();
        }
    }
}
