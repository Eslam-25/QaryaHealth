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
public class MedicalStaffController : ControllerBase
{
    private readonly IMedicalStaffService _medicalStaffService;

    public MedicalStaffController(IMedicalStaffService medicalStaffService)
    {
        _medicalStaffService = medicalStaffService;
    }

    [HttpGet("{id}")]
    public async Task<MedicalStaffDto> GetById(int id)
    {
        return await _medicalStaffService.GetByIdAsync(id);
    }

    [HttpGet("ByType/{type}")]
    public async Task<IEnumerable<MedicalStaffDto>> GetByBloodType(MedicalStaffType type)
    {
        return await _medicalStaffService.GetByTypeAsync(type);
    }

    [HttpGet("ToggleWorking/{id}")]
    public async Task<bool> ToggleDonor(int id)
    {
        return await _medicalStaffService.ToggleWorkingAsyc(id);
    }

    [HttpPost]
    public async Task<bool> Post(MedicalStaffDto medicalStaffDto)
    {
        return await _medicalStaffService.CreateAsync(medicalStaffDto);
    }

    [HttpPut]
    public async Task<bool> Update(MedicalStaffDto medicalStaffDto)
    {
        return await _medicalStaffService.UpdateAsync(medicalStaffDto);
    }

}
