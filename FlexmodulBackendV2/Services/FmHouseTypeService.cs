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
        public async Task<FmHouseType> GetFmHouseTypeByTypeAsync(int houseType)
        {
            return await DbContext.FmHouseTypes
                .SingleOrDefaultAsync(ht => ht.HouseType == houseType);
        }
    }
}