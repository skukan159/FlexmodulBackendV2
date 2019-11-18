using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IFmHousesService
    {
        Task<bool> CreateFmHouseAsync(FmHouse fmHouse);
        Task<List<FmHouse>> GetFmHousesAsync();
        Task<FmHouse> GetFmHouseByIdAsync(Guid fmHouseId);
        Task<bool> UpdateFmHouseAsync(FmHouse fmHouseToUpdate);
        Task<bool> DeleteFmHouseAsync(Guid fmHouseId);
    }
}
