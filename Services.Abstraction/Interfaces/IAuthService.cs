using Domain.Entities;
using Services.Abstraction.Dtos;
using Services.Abstraction.Models;

namespace Services.Abstraction.Interfaces;

public interface IAuthService : IService<User, UserDto>
{
    Task<AuthModel> LoginAsync(LoginModel loginModel);
    Task<AuthModel> RegisterUserAsync(UserDto userDto);
    Task<AuthModel> RegisterDonorAsync(DonorDto donorDto);
}
