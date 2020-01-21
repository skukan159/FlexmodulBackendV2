using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Services
{
    public class FmHouseTypeService : Repository<FmHouseType>, IFmHouseTypesService
    {
        public FmHouseTypeService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<FmHouseType> GetByIdAsync(Guid fmHouseTypeId)
        {
            return await DbContext.FmHouseTypes
                .Include(ht => ht.MaterialsOnHouse)
                .SingleOrDefaultAsync(h => h.Id == fmHouseTypeId);
        }

        public override async Task<List<FmHouseType>> GetAsync()
        {
            return await DbContext.FmHouseTypes
                .Include(ht => ht.MaterialsOnHouse)
                //.Include(ht => ht.HouseType)
                .ToListAsync();
        }
        public async Task<FmHouseType> GetFmHouseTypeByTypeAsync(int houseType)
        {
            return await DbContext.FmHouseTypes
                .Include(ht => ht.MaterialsOnHouse)
                .SingleOrDefaultAsync(ht => ht.HouseType == houseType);
        }
        
    }
}