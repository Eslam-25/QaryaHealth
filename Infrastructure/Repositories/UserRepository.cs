using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IQueryable<User> _query;
    public UserRepository(ADbContext aDbContext) : base(aDbContext)
    {
        _query = aDbContext.Set<User>()
                           .AsQueryable()
                           .Include(r => r.Donor)
                           .Include(r => r.MedicalStaff);
    }

    public new Task<User?> GetAsync(Expression<Func<User, bool>> predicate)
    {
        return _query.Where(r => r.IsActive).FirstOrDefaultAsync(predicate);
    }

    public Task<User?> GetByIdAsync(int id) 
    {
        return _query.FirstOrDefaultAsync(r => r.Id == id);
    }
}
