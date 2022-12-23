using Domain.Enums;

namespace Domain.Entities;

public class Donor : Base
{
    public Donor()
    {
        User = new User();
    }
    public DateTime? BirthDate { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public BloodType? BloodType { get; set; }
    public Gender? Gender { get; set; }
    public bool ReadyToDonor { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
