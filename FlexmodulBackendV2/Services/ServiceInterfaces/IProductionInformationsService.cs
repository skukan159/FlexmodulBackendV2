using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IProductionInformationsService
    {
        Task<bool> CreateProductionInformationAsync(ProductionInformation productionInformation);
        Task<List<ProductionInformation>> GetProductionInformationsAsync();
        Task<ProductionInformation> GetProductionInformationByIdAsync(Guid productionInformationId);
        Task<bool> UpdateProductionInformationAsync(ProductionInformation productionInformation);
        Task<bool> DeleteProductionInformationAsync(Guid productionInformationId);
    }
}
