using Domain.Entities;
using Domain.Enums;

namespace Domain.IRepositories;

public interface IDonorRepository : IRepository<Donor>
{
    Task<Donor> GetByIdAsync(int id);
    Task<List<Donor>> GetByBloodTypeAsync(BloodType bloodType);
}
