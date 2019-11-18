using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Services
{
    public class AdditionalCostsService : IAdditionalCostsService
    {
        private readonly ApplicationDbContext _dataContext;

        public AdditionalCostsService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateAdditionalCostAsync(AdditionalCost additionalCost)
        {
            await _dataContext.AddAsync(additionalCost);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<AdditionalCost>> GetAdditionalCostsAsync()
        {
            return await _dataContext.AdditionalCosts.ToListAsync();
        }

        public async Task<AdditionalCost> GetAdditionalCostByIdAsync(Guid additionalCostId)
        {
            return await _dataContext.AdditionalCosts
                .SingleOrDefaultAsync(ac => ac.Id == additionalCostId);
        }

        public async Task<bool> UpdateAdditionalCostAsync(AdditionalCost additionalCost)
        {
            _dataContext.AdditionalCosts.Update(additionalCost);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAdditionalCostAsync(Guid additionalCostId)
        {
            var additionalCost = await GetAdditionalCostByIdAsync(additionalCostId);

            if (additionalCost == null)
                return false;

            _dataContext.AdditionalCosts.Remove(additionalCost);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}