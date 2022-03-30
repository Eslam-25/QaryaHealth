using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QaryaHealth.Core.Entities;
using QaryaHealth.Core.Utilities;
using QaryaHealth.Service.Dtos;

namespace QaryaHealth.Service.Interfaces
{
    public interface IBaseService<T, Entity> where T : BaseDto where Entity : Base
    {
        Task<bool> CreateAsync(T model);
        Task<bool> UpdateAsync(T model);
        
        Task<bool> SoftDeleteAsync(T model);
        Task<bool> HardDeleteAsync(T model);
        
        Task<T> GetAsync(int id);
        Task<PagedListResult<T>> GetPagedListAsync(QueryParams queryParams, Expression<Func<Entity, bool>> predicate = null);

        Task<List<T>> GetListAsync();
        Task<List<T>> GetListAsync(Expression<Func<Entity, bool>> predicate);

        Task<bool> SaveAsync();
    }
}