using Domain.Entities;
using Domain.Enums;
using Services.Abstraction.Dtos;

namespace Services.Abstraction.Interfaces;

public interface IMedicalStaffService : IService<MedicalStaff, MedicalStaffDto>
{
    Task<MedicalStaffDto> GetByIdAsync(int id);
    Task<bool> ToggleWorkingAsyc(int id);
    Task<IEnumerable<MedicalStaffDto>> GetByTypeAsync(MedicalStaffType type);
}
