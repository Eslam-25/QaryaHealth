using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;
using Services.Abstraction.Mappers;

namespace Services.Implementations;

public class MedicalStaffService : Service<MedicalStaff, MedicalStaffDto>, IMedicalStaffService
{
    private readonly IMedicalStaffRepository _repository;
    public MedicalStaffService(IMedicalStaffRepository repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<MedicalStaffDto> GetByIdAsync(int id)
    {
        var staff = await _repository.GetByIdAsync(id);
        return staff.ToDTO<MedicalStaff, MedicalStaffDto>();

    }
    public async Task<bool> ToggleWorkingAsyc(int id)
    {
        var staff = await _repository.GetByIdAsync(id);
        if (staff is null) return false;

        staff.ReadyToWork = !staff.ReadyToWork;
        return await _repository.SaveChangesAsync();

    }

    public async Task<IEnumerable<MedicalStaffDto>> GetByTypeAsync(MedicalStaffType type)
    {
        var staffes = await _repository.GetByTypeAsync(type);
        return staffes.ToEnumerableDTO<MedicalStaff, MedicalStaffDto>();
    }
}
