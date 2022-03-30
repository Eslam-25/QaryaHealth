using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QaryaHealth.Core.Entities;
using QaryaHealth.Core.IRepositories;
using X.PagedList;

namespace QaryaHealth.GoogleSheet.Repositories
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        virtual protected IQueryable<T> _query { get; }
        public virtual IEnumerable<T> GetList() => _query.AsEnumerable();
        public virtual IEnumerable<T> GetList(Func<T, bool> predicate) => _query.Where(predicate);
        public virtual Task<List<T>> GetListAsync() => _query.ToListAsync();
        public virtual Task<List<T>> GetListAsync(Func<T, bool> predicate)
        {
            return _query.Where(predicate).ToListAsync();
        }
        public virtual IPagedList<T> GetPageList(string sortDirection, int page = 1, int pageSize = 10)
        {
            IQueryable<T> query = _query;
            return query.ToPagedList(page, pageSize);
        }
        public virtual void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
        }
        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
        }
        public virtual async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

        }
        public virtual Task<IPagedList<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, string sortColumn, string sortDirection, int page, int pageSize)
        {
            IQueryable<T> query = _query;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToPagedListAsync(page, pageSize);
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null ? _query.Where(predicate).ToListAsync() : _query.ToListAsync();
        }
        public virtual Task<List<T>> GetDistinctListAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _query.Where(predicate).Distinct().ToListAsync() : _query.Distinct().ToListAsync();
        }
        public virtual Task AddAsync(IEnumerable<T> entities) => null;

        public virtual Task<List<T>> GetDistinctListAsyncAsNoTrack(Expression<Func<T, bool>> predicate)
        {
            return _query.Where(predicate).ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _query.FirstOrDefault(e => e.Id == id);
        }

        public virtual Task<T> GetAsync(int id, Expression<Func<T, object>>[] includes = null)
        {
            return null;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return true;
        }
    }
}
