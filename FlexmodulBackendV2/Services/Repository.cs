using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexmodulBackendV2.Services
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext DbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> GetAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<bool> CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            var created = await DbContext.SaveChangesAsync();
            return created > 0;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            var updated = await DbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            var deleted = await DbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
