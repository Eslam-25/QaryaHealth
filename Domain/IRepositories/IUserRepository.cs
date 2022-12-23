using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.IRepositories;

public interface IUserRepository : IRepository<User>
{
    new Task<User?> GetAsync(Expression<Func<User, bool>> predicate);
    Task<User?> GetByIdAsync(int id);
}
