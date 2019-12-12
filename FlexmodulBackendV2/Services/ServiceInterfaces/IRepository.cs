using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAsync();
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<bool> CreateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);

    }
}
