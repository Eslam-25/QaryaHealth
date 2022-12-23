using Domain.Entities;
using Domain.Enums;

namespace Domain.IRepositories;

public interface IMedicalStaffRepository : IRepository<MedicalStaff>
{
    Task<MedicalStaff> GetByIdAsync(int id);
    Task<List<MedicalStaff>> GetByTypeAsync(MedicalStaffType type);
}
