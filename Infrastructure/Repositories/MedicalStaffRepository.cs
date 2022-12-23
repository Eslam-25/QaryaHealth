using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MedicalStaffRepository : Repository<MedicalStaff>, IMedicalStaffRepository
{
    private readonly IQueryable<MedicalStaff> _query;
    public MedicalStaffRepository(ADbContext aDbContext) : base(aDbContext)
    {
        _query = aDbContext.Set<MedicalStaff>()
                           .AsQueryable()
                           .Include(r => r.User)
                           .Include(r => r.MedicalStaffServices);
    }

    public Task<MedicalStaff> GetByIdAsync(int id)
    {
        return _query.FirstOrDefaultAsync(r => r.Id == id);
    }

    public Task<List<MedicalStaff>> GetByTypeAsync(MedicalStaffType type)
    {
        return _query.Where(r => 
                            r.Type == type && 
                            r.User.IsActive
                            ).ToListAsync();
    }
}
