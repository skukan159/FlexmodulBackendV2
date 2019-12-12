using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Services.ServiceInterfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetById(int id);
        Task<List<T>> Get();
        Task<List<T>> Get(Expression<Func<T, bool>> predicate);
        Task<bool> Create(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);

    }
}
