using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IAdditionalCostsService
    {
        Task<bool> CreateAdditionalCostAsync(AdditionalCost additionalCost);
        Task<List<AdditionalCost>> GetAdditionalCostsAsync();
        Task<AdditionalCost> GetAdditionalCostByIdAsync(Guid additionalCostId);
        Task<bool> UpdateAdditionalCostAsync(AdditionalCost additionalCost);
        Task<bool> DeleteAdditionalCostAsync(Guid additionalCostId);
    }
}
