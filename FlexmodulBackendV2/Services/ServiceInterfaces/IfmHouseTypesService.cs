using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IFmHouseTypesService : IRepository<FmHouseType>
    {
        Task<FmHouseType> GetFmHouseTypeByTypeAsync(int houseType);
    }
}
