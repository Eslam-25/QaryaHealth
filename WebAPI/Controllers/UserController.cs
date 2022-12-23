using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;

namespace WebAPI.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<UserDto> GetById(int id)
    {
        return await _userService.GetAsync(id);
    }

    [HttpGet("GetByEmail")]
    public async Task<UserDto> GetByEmail(string email)
    {
        return await _userService.GetByEmailAsync(email); ;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<UserDto>> GetAll()
    {
        return await _userService.GetListAsync();
    }

    [HttpPost]
    public async Task<bool> Post(UserDto userDto)
    {
        return await _userService.CreateAsync(userDto);
    }

    [HttpPut]
    public async Task<bool> Update(UserDto userDto)
    {
        return await _userService.UpdateAsync(userDto);
    }

}
