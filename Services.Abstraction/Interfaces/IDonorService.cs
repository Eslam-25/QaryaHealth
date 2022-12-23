using Domain.Entities;
using Domain.Enums;
using Services.Abstraction.Dtos;

namespace Services.Abstraction.Interfaces;

public interface IDonorService : IService<Donor, DonorDto>
{
    Task<bool> ToggleDonorAsyc(int id);
    Task<DonorDto> GetByIdAsync(int id);
    Task<IEnumerable<DonorDto>> GetByTypeAsync(BloodType bloodType);
}
