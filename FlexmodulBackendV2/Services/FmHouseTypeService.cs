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
    public class FmHouseTypeService : IFmHouseTypesService
    {
        private readonly ApplicationDbContext _dataContext;

        public FmHouseTypeService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateFmHouseTypeAsync(FmHouseType fmHouseType)
        {
            await _dataContext.AddAsync(fmHouseType);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<FmHouseType>> GetFmHouseTypesAsync()
        {
            return await _dataContext.FmHouseTypes.ToListAsync();
        }

        public async Task<FmHouseType> GetFmHouseTypeByIdAsync(Guid fmHouseTypeId)
        {
            return await _dataContext.FmHouseTypes
                .SingleOrDefaultAsync(ac => ac.Id == fmHouseTypeId);
        }

        public async Task<bool> UpdateFmHouseTypeAsync(FmHouseType fmHouseType)
        {
            _dataContext.FmHouseTypes.Update(fmHouseType);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteFmHouseTypeAsync(Guid fmHouseTypeId)
        {
            var fmHouseType = await GetFmHouseTypeByIdAsync(fmHouseTypeId);

            if (fmHouseType == null)
                return false;

            _dataContext.FmHouseTypes.Remove(fmHouseType);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}