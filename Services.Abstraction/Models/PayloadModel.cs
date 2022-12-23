using System.ComponentModel.DataAnnotations;

namespace Services.Abstraction.Models;

public class PayloadModel
{
    [Required]
    public required string To { get; set; }
    [Required]
    public required string Subject { get; set; }
    [Required]
    public required string Body { get; set; }
}
