using System.ComponentModel.DataAnnotations;

namespace Services.Abstraction.Models;

public class LoginModel
{
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Password { get; set; }
}
