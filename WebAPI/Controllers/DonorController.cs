using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;
using Services.Implementations;

namespace WebAPI.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _donorService;

    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [HttpGet("{id}")]
    public async Task<DonorDto> GetById(int id)
    {
        return await _donorService.GetByIdAsync(id);
    }

    [HttpGet("ByBloodType/{bloodType}")]
    public async Task<IEnumerable<DonorDto>> GetByBloodType(BloodType bloodType)
    {
        return await _donorService.GetByTypeAsync(bloodType);
    }

    [HttpGet("ToggleDonor/{id}")]
    public async Task<bool> ToggleDonor(int id)
    {
        return await _donorService.ToggleDonorAsyc(id);
    }

    [HttpPost]
    public async Task<bool> Post(DonorDto donorDto)
    {
        return await _donorService.CreateAsync(donorDto);
    }

    [HttpPut]
    public async Task<bool> Update(DonorDto donorDto)
    {
        return await _donorService.UpdateAsync(donorDto);
    }

}
