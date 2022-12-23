using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;

namespace WebAPI.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class MedicalStaffServiceController : ControllerBase
{
    private readonly IMedicalStaffServiceTypeService _medicalStaffService;

    public MedicalStaffServiceController(IMedicalStaffServiceTypeService medicalStaffService)
    {
        _medicalStaffService = medicalStaffService;
    }

    [HttpGet("{id}")]
    public async Task<MedicalStaffServiceTypeDto> GetById(int id)
    {
        return await _medicalStaffService.GetAsync(id);
    }

    [HttpPost]
    public async Task<bool> Post(MedicalStaffServiceTypeDto medicalStaffDto)
    {
        return await _medicalStaffService.CreateAsync(medicalStaffDto);
    }

    [HttpPut]
    public async Task<bool> Update(MedicalStaffServiceTypeDto medicalStaffDto)
    {
        return await _medicalStaffService.UpdateAsync(medicalStaffDto);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _medicalStaffService.SoftDeleteAsync(id);
    }

}
