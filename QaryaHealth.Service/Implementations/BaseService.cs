using QaryaHealth.Core.Entities;
using QaryaHealth.Core.IRepositories;
using QaryaHealth.Core.Utilities;
using QaryaHealth.Service.Dtos;
using QaryaHealth.Service.Interfaces;
using QaryaHealth.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QaryaHealth.Service.Implementations
{
    public abstract class BaseService<DTO, Entity> : IBaseService<DTO, Entity> where DTO : BaseDto where Entity : Base
    {
        private readonly IRepository<Entity> _repository;

        public BaseService(IRepository<Entity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> CreateAsync(DTO model)
        {
            var entity = model.ToEntity<Entity, DTO>();
            _repository.Add(entity);
            return await _repository.SaveChangesAsync();
        }
        public virtual async Task<bool> UpdateAsync(DTO model)
        {
            Entity entity = model.ToEntity<Entity, DTO>(_repository.Get(model.Id));
            _repository.Update(entity);
            return await _repository.SaveChangesAsync();
        }

        public virtual async Task<bool> SoftDeleteAsync(DTO model)
        {
            Entity entity = model.ToEntity<Entity, DTO>();
            _repository.Delete(entity);
            return await _repository.SaveChangesAsync();
        }
        public virtual async Task<bool> HardDeleteAsync(DTO model)
        {
            Entity entity = model.ToEntity<Entity, DTO>();
            _repository.HardDelete(entity);
            return await _repository.SaveChangesAsync();
        }

        public virtual async Task<DTO> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            return entity.ToDTO<Entity, DTO>();
        }
        public virtual async Task<PagedListResult<DTO>> GetPagedListAsync(QueryParams queryParams, Expression<Func<Entity, bool>> predicate = null)
        {
            queryParams = queryParams == null ? new QueryParams() : queryParams;
            var pagedList = await _repository.GetPageListAsync(
                predicate,
                queryParams.PageNumber,
                queryParams.PageSize);
            return new PagedListResult<DTO>(pagedList.ToPagedListDTO<Entity, DTO>());
        }

        public virtual async Task<List<DTO>> GetListAsync() => (await _repository.GetListAsync()).ToEnumerableDTO<Entity, DTO>();
        public virtual async Task<List<DTO>> GetListAsync(Expression<Func<Entity, bool>> predicate)
        {
            var list = await _repository.GetListAsync(predicate);
            return list.ToEnumerableDTO<Entity, DTO>();
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await _repository.SaveChangesAsync();
        }
    }
}
