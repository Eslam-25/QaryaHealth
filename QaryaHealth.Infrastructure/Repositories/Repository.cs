using Microsoft.EntityFrameworkCore;
using QaryaHealth.Core.Entities;
using QaryaHealth.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace QaryaHealth.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        protected readonly DbSet<T> _entities;
        private readonly QaryaHealthDbContext _dbContext;
        virtual protected IQueryable<T> _query { get => _entities; }
        public Repository(QaryaHealthDbContext qaryaHealthDbContext) 
        {
            _dbContext = qaryaHealthDbContext;
            _entities = qaryaHealthDbContext.Set<T>();
        }
        virtual public Task<List<T>> GetListAsync() => _query.Where(r => r.IsActive).ToListAsync();
        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _query.Where(r => r.IsActive).Where(predicate).ToListAsync() :
              _entities.ToListAsync();
        }
        
        public Task<List<T>> GetDistinctListAsyncAsNoTrack(Expression<Func<T, bool>> predicate) => _query.Where(r => r.IsActive).Where(predicate).ToListAsync();
        public Task<List<T>> GetDistinctListAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? _query.Where(r => r.IsActive).Where(predicate).Distinct().ToListAsync() : _query.Distinct().ToListAsync();
        }

        public Task<IPagedList<T>> GetPageListAsync(Expression<Func<T, bool>> predicate, int page, int pageSize)
        {
            IQueryable<T> query = _query;
            if (predicate != null)
                query = query.Where(predicate);

            return query.Where(r => r.IsActive).ToPagedListAsync(page, pageSize);
        }

        public T Get(int id) => _query.FirstOrDefault(e => e.Id == id && e.IsActive);

        public Task<T> GetAsync(int id, Expression<Func<T, object>>[] includes = null)
        {
            return Include(includes).FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.IsActive = true;
            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsActive = false;
            Update(entity);
        }
        public void HardDelete(T entity)
        {
            _entities.Remove(entity);
        }

        public void Update(T entity) => _entities.Update(entity);

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        private IQueryable<T> Include(Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _query;
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query;
        }
    }
}
