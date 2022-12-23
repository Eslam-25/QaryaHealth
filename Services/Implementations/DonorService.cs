using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;
using Services.Abstraction.Mappers;

namespace Services.Implementations;

public class DonorService : Service<Donor, DonorDto>, IDonorService
{
    private readonly IDonorRepository _repository;
    public DonorService(IDonorRepository repository)
        : base(repository)
    {
        _repository = repository;
    } 

    public async Task<bool> ToggleDonorAsyc(int id)
    {
        var donor = await _repository.GetByIdAsync(id);
        if (donor is null) return false;

        donor.ReadyToDonor = !donor.ReadyToDonor;
        return await _repository.SaveChangesAsync();
    }

    public async Task<DonorDto> GetByIdAsync(int id)
    {
        var donor = await _repository.GetByIdAsync(id);
        return donor.ToDTO<Donor, DonorDto>();
    }

    public async Task<IEnumerable<DonorDto>> GetByTypeAsync(BloodType bloodType)
    {
        var donors = await _repository.GetByBloodTypeAsync(bloodType);
        return donors.ToEnumerableDTO<Donor, DonorDto>();
    }

}
