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
    public class RentService : IRentsService
    {
        private readonly ApplicationDbContext _dataContext;

        public RentService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateRentAsync(Rent rent)
        {
            await _dataContext.AddAsync(rent);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<Rent>> GetRentsAsync()
        {
            return await _dataContext.Rents.ToListAsync();
        }

        public async Task<Rent> GetRentByIdAsync(Guid rentId)
        {
            return await _dataContext.Rents
                .SingleOrDefaultAsync(r => r.Id == rentId);
        }

        public async Task<bool> UpdateRentAsync(Rent rentToUpdate)
        {
            _dataContext.Rents.Update(rentToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteRentAsync(Guid rentId)
        {
            var rent = await GetRentByIdAsync(rentId);

            if (rent == null)
                return false;

            _dataContext.Rents.Remove(rent);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}


