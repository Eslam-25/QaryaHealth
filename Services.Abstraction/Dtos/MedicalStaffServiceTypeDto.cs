using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Services.Abstraction.Dtos;

public class MedicalStaffServiceTypeDto : BaseDto
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public decimal? Cost { get; set; }
    public int StaffId { get; set; }
}
