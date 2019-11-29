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
    public class MaterialService : IMaterialsService
    {
        private readonly ApplicationDbContext _dataContext;

        public MaterialService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateMaterialAsync(Material material)
        {
            await _dataContext.AddAsync(material);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<Material>> GetMaterialsAsync()
        {
            return await _dataContext.Materials.ToListAsync();
        }

        public async Task<Material> GetMaterialByIdAsync(Guid materialId)
        {
            return await _dataContext.Materials
                .SingleOrDefaultAsync(m => m.Id == materialId);
        }

        public async Task<bool> UpdateMaterialAsync(Material material)
        {
            _dataContext.Materials.Update(material);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteMaterialAsync(Guid materialId)
        {
            var material = await GetMaterialByIdAsync(materialId);

            if (material == null)
                return false;

            _dataContext.Materials.Remove(material);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}