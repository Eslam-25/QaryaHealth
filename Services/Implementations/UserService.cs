using Domain.Entities;
using Domain.IRepositories;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;
using Services.Abstraction.Mappers;

namespace Services.Implementations;

public class UserService : Service<User, UserDto>, IUserService
{
    private readonly IRepository<User> _repository;
    public UserService(IRepository<User> repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await _repository.GetAsync(r => r.Email == email);
        return user != null ? user.ToDTO<User, UserDto>() : null;
    }

}
