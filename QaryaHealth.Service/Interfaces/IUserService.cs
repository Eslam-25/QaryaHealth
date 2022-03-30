using QaryaHealth.Core.Entities;
using QaryaHealth.Service.Dtos;
using QaryaHealth.Service.Models;
using System.Threading.Tasks;

namespace QaryaHealth.Service.Interfaces
{
    public interface IUserService : IBaseService<UserDto, User>
    {
        Task<UserDto> LoginAsync(UserLoginModel loginModel);
    }
}