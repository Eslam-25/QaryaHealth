using Microsoft.AspNetCore.Mvc;
using QaryaHealth.Core.Utilities;
using QaryaHealth.Service.Dtos;
using QaryaHealth.Service.Interfaces;
using QaryaHealth.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QaryaHealth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<UserDto>> Get()
        {
            return await _userService.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetById(int id)
        {
            return await _userService.GetAsync(id);
        }

        [HttpGet("paged")]
        public async Task<PagedListResult<UserDto>> GetPagedList([FromQuery] QueryParams queryParams)
        {
            return await _userService.GetPagedListAsync(queryParams);
        }

        [HttpPost]
        public async Task<bool> Post(UserDto userDto)
        {
            return await _userService.CreateAsync(userDto);
        }

        [HttpPost("login")]
        public async Task<UserDto> Login(UserLoginModel loginModel)
        {
            return await _userService.LoginAsync(loginModel);
        }

        [HttpPut]
        public async Task<bool> Update(UserDto userDto)
        {
            return await _userService.UpdateAsync(userDto);
        }

        [HttpDelete("soft-delete")]
        public async Task<bool> SoftDelete(UserDto userDto)
        {
            return await _userService.SoftDeleteAsync(userDto);
        }

        [HttpDelete("hard-delete")]
        public async Task<bool> HardDelete(UserDto userDto)
        {
            return await _userService.HardDeleteAsync(userDto);
        }
    }
}
