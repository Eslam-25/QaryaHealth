using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadImageController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    public UploadImageController(IWebHostEnvironment environment)
	{
        _environment = environment;
	}

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        try
        {
            string fName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(_environment.ContentRootPath, "Images", fName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { FileName = fName });
        }
        catch
        {
            return StatusCode(500, $"Internal server error");
        }
    }

}
