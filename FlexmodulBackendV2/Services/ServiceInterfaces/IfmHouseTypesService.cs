using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IFmHouseTypesService
    {
        Task<bool> CreateFmHouseTypeAsync(FmHouseType fmHouseType);
        Task<List<FmHouseType>> GetFmHouseTypesAsync();
        Task<FmHouseType> GetFmHouseTypeByIdAsync(Guid fmHouseTypeId);
        Task<bool> UpdateFmHouseTypeAsync(FmHouseType fmHouseTypeToUpdate);
        Task<bool> DeleteFmHouseTypeAsync(Guid fmHouseTypeId);
    }
}
