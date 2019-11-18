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
    //TODO: Implement this service properly
    public class MaterialOnHouseTypesService //: IMaterialOnHouseTypesService
    {
        private readonly ApplicationDbContext _dataContext;

        public MaterialOnHouseTypesService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        /*public async Task<bool> CreateMaterialOnHouseTypeAsync(MaterialOnHouseType materialOnHouseType)
        {
            await _dataContext.AddAsync(materialOnHouseType);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<MaterialOnHouseType>> GetMaterialOnHouseTypesAsync()
        {
            return await _dataContext.MaterialOnHouseTypes.ToListAsync();
        }

        //Todo: Implement this properly
        public async Task<MaterialOnHouseType> GetMaterialOnHouseTypeByIdAsync(Guid materialOnHouseTypeId)
        {
            return await _dataContext.MaterialOnHouseTypes
                .SingleOrDefaultAsync(m => m.MaterialId == materialOnHouseTypeId);
        }

        public async Task<bool> UpdateMaterialOnHouseTypeAsync(MaterialOnHouseType materialOnHouseType)
        {
            _dataContext.Materials.Update(material);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteMaterialOnHouseTypeAsync(Guid materialOnHouseTypeId)
        {
            var material = await GetMaterialByIdAsync(materialId);

            if (material == null)
                return false;

            _dataContext.Materials.Remove(material);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }*/
    }
}
