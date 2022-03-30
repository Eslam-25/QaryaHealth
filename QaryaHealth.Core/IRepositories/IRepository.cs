using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace QaryaHealth.Core.IRepositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetListAsync();
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetDistinctListAsync(Expression<Func<T, bool>> predicate);
        Task<IPagedList<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, int page, int pageSize);

        Task<List<T>> GetDistinctListAsyncAsNoTrack(Expression<Func<T, bool>> predicate);
        T Get(int id);
        Task<T> GetAsync(int id, Expression<Func<T, object>>[] includes = null);
        void Add(T entity);
        void Delete(T entity);
        void HardDelete(T entity);
        void Update(T entity);
        public Task<bool> SaveChangesAsync();

    }
}
