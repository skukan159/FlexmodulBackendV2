using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IRentsService
    {
        Task<bool> CreateRentAsync(Rent rent);
        Task<List<Rent>> GetRentsAsync();
        Task<Rent> GetRentByIdAsync(Guid rentId);
        Task<bool> UpdateRentAsync(Rent rentToUpdate);
        Task<bool> DeleteRentAsync(Guid rentId);
    }
}
