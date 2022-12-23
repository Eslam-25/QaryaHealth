using Domain.Enums;

namespace Domain.Entities;

public class User : Base
{
    public User()
    {
        AdvImages = new List<AdvImage>();
    }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }

    public Role Role { get; set; }
    public Donor? Donor { get; set; }
    public MedicalStaff? MedicalStaff { get; set; }

    public ICollection<AdvImage> AdvImages { get; set; }
}
