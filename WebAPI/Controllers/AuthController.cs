using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;
using Services.Abstraction.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;

    public AuthController(IAuthService authService, IEmailService emailService)
    {
        _authService = authService;
        _emailService = emailService;
    }

    [HttpPost("send-email")]
    public IActionResult SendEmail([FromBody] PayloadModel payloadModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(payloadModel);
        }

        return Ok(_emailService.SendAsync(payloadModel));
    }

    [HttpPost("RegisterDonor")]
    public async Task<IActionResult> RegisterDonor([FromBody] DonorDto donorDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(donorDto);
        }

        return Ok(await _authService.RegisterDonorAsync(donorDto));
    }

    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(userDto);
        }

        return Ok(await _authService.RegisterUserAsync(userDto));
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(loginModel);
        }

        return Ok(await _authService.LoginAsync(loginModel));
    }

}
