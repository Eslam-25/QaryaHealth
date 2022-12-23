using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Services.Abstraction.Dtos;

public class DonorDto : BaseDto
{
    [Required]
    public string? Name { get; set; }
    public string? Address { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public Role Role { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public BloodType? BloodType { get; set; }
    public Gender? Gender { get; set; }
    public bool ReadyToDonor { get; set; }
    public int UserId { get; set; }
}
