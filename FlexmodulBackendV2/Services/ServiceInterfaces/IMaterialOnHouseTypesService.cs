using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    //Todo: Update this service, this is not properly implemented
    public interface IMaterialOnHouseTypesService
    {
        Task<bool> CreateMaterialOnHouseTypeAsync(MaterialOnHouseType materialOnHouseType);
        Task<List<MaterialOnHouseType>> GetMaterialOnHouseTypesAsync();
        Task<MaterialOnHouseType> GetMaterialOnHouseTypeByIdAsync(Guid id);
        Task<bool> UpdateMaterialOnHouseTypeAsync(MaterialOnHouseType materialOnHouseType);
        Task<bool> DeleteMaterialOnHouseTypeAsync(Guid id);
    }
}
