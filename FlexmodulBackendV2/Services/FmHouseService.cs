using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Services
{
    public class FmHouseService : Repository<FmHouse>
    {
        public FmHouseService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<FmHouse> GetByIdAsync(Guid fmHouseId)
        {
            return await DbContext.FmHouses
                .Include(house => house.HouseType)
                .SingleOrDefaultAsync(h => h.Id == fmHouseId);
        }

        public override async Task<List<FmHouse>> GetAsync()
        {
            return await DbContext.FmHouses
                .Include(house => house.HouseType)
                .ToListAsync();
        }
    }
}