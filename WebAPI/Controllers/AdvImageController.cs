using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;

namespace WebAPI.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class AdvImageController : ControllerBase
{
    private readonly IAdvImageService _advImageService;

    public AdvImageController(IAdvImageService advImageService)
    {
        _advImageService = advImageService;
    }

    [HttpGet]
    public async Task<List<AdvImageDto>> GetAll()
    {
        return await _advImageService.GetListAsync();
    }

    [HttpPost]
    public async Task<bool> Post(AdvImageDto advImageDto)
    {
        return await _advImageService.CreateAsync(advImageDto);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _advImageService.HardDeleteAsync(id);
    }

}
