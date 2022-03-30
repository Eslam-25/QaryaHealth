using QaryaHealth.Core.Entities;
using QaryaHealth.Core.IRepositories;
using QaryaHealth.Service.Dtos;
using QaryaHealth.Service.Interfaces;
using QaryaHealth.Service.Mappers;
using QaryaHealth.Service.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QaryaHealth.Service.Implementations
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> LoginAsync(UserLoginModel loginModel) 
        {
            var users = await _repository.GetDistinctListAsync(r => r.PhoneNumber == loginModel.PhonNumber && r.Password == loginModel.Password);
            if (!users.Any()) return null;

            return users.FirstOrDefault().ToDTO<User, UserDto>();
        }
    }
}