using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;

namespace WebAPI.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class AdvImageController : ControllerBase
{
    private readonly string ImagePath;
    private readonly IAdvImageService _advImageService;

    public AdvImageController(IAdvImageService advImageService, IConfiguration config)
    {
        _advImageService = advImageService;
        ImagePath = config["appUrl"] + "images/";
    }

    [HttpGet]
    public async Task<List<AdvImageDto>> GetAll()
    {
        var images = await _advImageService.GetListAsync();
        foreach (var image in images)
        {
            image.ImagePath = ImagePath + image.ImagePath; 
        }
        return images;
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
