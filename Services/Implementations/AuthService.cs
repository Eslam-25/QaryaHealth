using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Dtos;
using Services.Abstraction.Interfaces;
using Services.Abstraction.Mappers;
using Services.Abstraction.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations;

public class AuthService : Service<User, UserDto>, IAuthService
{
    private readonly JwtModel _jwt;
    private readonly IUserRepository _userRepository;
    private readonly IDonorRepository _donorRepository;
    public AuthService(IUserRepository userRepository, IDonorRepository donorRepository, IOptions<JwtModel> jwt)
        : base(userRepository)
    {
        _jwt = jwt.Value;
        _userRepository = userRepository;
        _donorRepository = donorRepository;
    }

    public async Task<AuthModel> LoginAsync(LoginModel loginModel)
    {
        var user = await _userRepository.GetAsync(r => r.PhoneNumber == loginModel.PhoneNumber && r.Password == loginModel.Password);
        if (user is null)
        {
            return new AuthModel { Message = "تحقق رقم الهاتف", IsAuthenticated = false };
        }

        int userId = user.Role switch
        {
            Role.Donar => user!.Donor.Id,
            Role.MedicalStaff => user!.MedicalStaff.Id,
            _ => user.Id
        };

        AuthModel authModel = new AuthModel { Message = "تم الدخول بنجاح", IsAuthenticated = true };
        return AddTokenToAuthModel(authModel, userId, user);
    }

    public async Task<AuthModel> RegisterUserAsync(UserDto userDto)
    {
        var result = await UserIsRegistered(userDto.PhoneNumber);
        if (result != null) return result;

        var newUser = userDto.ToEntity<User, UserDto>();
        _userRepository.Add(newUser);

        bool isAdded = await _userRepository.SaveChangesAsync();
        return PrepareAuthModel(isAdded, newUser.Id, newUser);
    }

    public async Task<AuthModel> RegisterDonorAsync(DonorDto donorDto)
    {
        var result = await UserIsRegistered(donorDto.PhoneNumber);
        if (result != null) return result;

        var donor = donorDto.ToEntity<Donor, DonorDto>();
        _donorRepository.Add(donor);

        bool isAdded = await _donorRepository.SaveChangesAsync();
        return PrepareAuthModel(isAdded, donor.Id, donor.User);
    }

    private AuthModel PrepareAuthModel(bool isAdded, int id, User user)
    {
        AuthModel authModel = new AuthModel { Message = "" };

        if (isAdded)
        {
            authModel.Message = "تم اضافة المستخدم بنجاح";
            authModel.IsAuthenticated = true;
            authModel = AddTokenToAuthModel(authModel, id, user);
        }
        else
        {
            authModel.Message = "Some thing error happens";
        }

        return authModel;
    }

    private async Task<AuthModel> UserIsRegistered(string phoneNumber)
    {
        var user = await _userRepository.GetAsync(r => r.PhoneNumber == phoneNumber);

        if (user is not null && user.PhoneNumber == user.PhoneNumber)
        {
            return new AuthModel { Message = "رقم الهاتف مسجل بالفعل" };
        }

        return null;
    }

    private AuthModel AddTokenToAuthModel(AuthModel authModel, int id, User user)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
            new Claim("id" , id.ToString()),
            new Claim("name" , user.Name ?? ""),
            new Claim("phone" , user.PhoneNumber ?? ""),
            new Claim("role" , user.Role.ToString())
        };

        //var expireOn = DateTime.Now.AddDays(_jwt.DurationInDays);
        var expireOn = DateTime.Now.AddYears(30);
        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            expires: expireOn,
            claims: claims,
            signingCredentials: signingCredentials
            );

        authModel.ExpiresOn = expireOn;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(token);

        return authModel;
    }
}
