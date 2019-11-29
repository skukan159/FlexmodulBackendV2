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
    public class ProductionInformationService : IProductionInformationsService
    {
        private readonly ApplicationDbContext _dataContext;

        public ProductionInformationService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateProductionInformationAsync(ProductionInformation productionInformation)
        {
            await _dataContext.AddAsync(productionInformation);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<ProductionInformation>> GetProductionInformationsAsync()
        {
            return await _dataContext.ProductionInformations.ToListAsync();
        }

        public async Task<ProductionInformation> GetProductionInformationByIdAsync(Guid productionInformationId)
        {
            return await _dataContext.ProductionInformations
                .SingleOrDefaultAsync(pi => pi.Id == productionInformationId);
        }

        public async Task<bool> UpdateProductionInformationAsync(ProductionInformation productionInformation)
        {
            _dataContext.ProductionInformations.Update(productionInformation);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteProductionInformationAsync(Guid productionInformationId)
        {
            var productionInformation = await GetProductionInformationByIdAsync(productionInformationId);

            if (productionInformation == null)
                return false;

            _dataContext.ProductionInformations.Remove(productionInformation);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}