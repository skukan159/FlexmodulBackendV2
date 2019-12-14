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
    public class ProductionInformationService : Repository<ProductionInformation>
    {
        public ProductionInformationService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<List<ProductionInformation>> GetAsync()
        {
            if (DbContext.ProductionInformations != null)
            {
                return await DbContext.ProductionInformations
                    .Include(pi => pi.Customer)
                    .Include(pi => pi.House)
                    .Include(pi => pi.AdditionalCosts)
                    .ToListAsync();
            }
            else return new List<ProductionInformation>();

        }

        public override async Task<ProductionInformation> GetByIdAsync(Guid productionInformationId)
        {
            return await DbContext.ProductionInformations
                .Include(pi => pi.Customer)
                .Include(pi => pi.House)
                .Include(pi => pi.AdditionalCosts)
                .SingleOrDefaultAsync(pi => pi.Id == productionInformationId);
        }
    }
}