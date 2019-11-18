using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IMaterialsService
    {
        Task<bool> CreateMaterialAsync(Material material);
        Task<List<Material>> GetMaterialsAsync();
        Task<Material> GetMaterialByIdAsync(Guid materialId);
        Task<bool> UpdateMaterialAsync(Material material);
        Task<bool> DeleteMaterialAsync(Guid materialId);
    }
}
