using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DonorRepository : Repository<Donor>, IDonorRepository
{
    private readonly IQueryable<Donor> _query;
    public DonorRepository(ADbContext aDbContext) : base(aDbContext)
    {
        _query = aDbContext.Set<Donor>()
                           .AsQueryable()
                           .Include(r => r.User);
    }

    public Task<Donor> GetByIdAsync(int id)
    {
        return _query.FirstOrDefaultAsync(r => r.Id == id);
    }

    public Task<List<Donor>> GetByBloodTypeAsync(BloodType bloodType)
    {
        return _query.Where(r => 
                            r.BloodType == bloodType && 
                            r.ReadyToDonor &&
                            r.User.IsActive &&
                            (r.LastDonationDate.HasValue ? r.LastDonationDate.Value.AddMonths(3) <= DateTime.Now : r.IsActive)
                            ).ToListAsync();
    }
}
