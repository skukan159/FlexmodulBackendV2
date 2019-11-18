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
    public class FmHousesService : IFmHousesService
    {
        private readonly ApplicationDbContext _dataContext;

        public FmHousesService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateFmHouseAsync(FmHouse fmHouse)
        {
            await _dataContext.AddAsync(fmHouse);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<FmHouse>> GetFmHousesAsync()
        {
            return await _dataContext.FmHouses.ToListAsync();
        }

        public async Task<FmHouse> GetFmHouseByIdAsync(Guid fmHouseId)
        {
            return await _dataContext.FmHouses
                .SingleOrDefaultAsync(h => h.Id == fmHouseId);
        }

        public async Task<bool> UpdateFmHouseAsync(FmHouse fmHouseToUpdate)
        {
            _dataContext.FmHouses.Update(fmHouseToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteFmHouseAsync(Guid fmHouseId)
        {
            var fmHouse = await GetFmHouseByIdAsync(fmHouseId);

            if (fmHouse == null)
                return false;

            _dataContext.FmHouses.Remove(fmHouse);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}