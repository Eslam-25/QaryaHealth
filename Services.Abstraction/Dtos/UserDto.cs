using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Services.Abstraction.Dtos;

public class UserDto : BaseDto
{
    [Required]
    public string? Name { get; set; }
    public string? Address { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [Required]
    public Role Role { get; set; }
}
