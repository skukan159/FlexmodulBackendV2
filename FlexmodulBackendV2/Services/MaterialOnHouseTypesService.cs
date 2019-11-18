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

    public class MaterialOnHouseTypesService : IMaterialOnHouseTypesService
    {
        private readonly ApplicationDbContext _dataContext;

        public MaterialOnHouseTypesService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateMaterialOnHouseTypeAsync(MaterialOnHouseType materialOnHouseType)
        {
            await _dataContext.AddAsync(materialOnHouseType);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<MaterialOnHouseType>> GetMaterialOnHouseTypesAsync()
        {
            return await _dataContext.MaterialOnHouseTypes.ToListAsync();
        }

        public async Task<MaterialOnHouseType> GetMaterialOnHouseTypeByIdAsync(Guid id)
        {
            return await _dataContext.MaterialOnHouseTypes
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> UpdateMaterialOnHouseTypeAsync(MaterialOnHouseType materialOnHouseType)
        {
            _dataContext.MaterialOnHouseTypes.Update(materialOnHouseType);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteMaterialOnHouseTypeAsync(Guid id)
        {
            var materialOnHouseType = await GetMaterialOnHouseTypeByIdAsync(id);

            if (materialOnHouseType == null)
                return false;

            _dataContext.MaterialOnHouseTypes.Remove(materialOnHouseType);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
