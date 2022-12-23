using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Services.Abstraction.Dtos;

public class MedicalStaffDto : BaseDto
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
    public MedicalStaffType Type { get; set; }
    public string? Bio { get; set; }
    public string? WorkingDescription { get; set; }
    public string? ImagePath { get; set; }
    public bool ReadyToWork { get; set; }

    public int UserId { get; set; }

    public List<MedicalStaffServiceTypeDto> Services { get; set; }
}
