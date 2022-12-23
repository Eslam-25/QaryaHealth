using Domain.Enums;

namespace Domain.Entities;

public class MedicalStaff : Base
{
    public MedicalStaff()
    {
        User = new User();
        MedicalStaffServices = new List<MedicalStaffServiceType>();
    }

    public MedicalStaffType Type { get; set; }
    public string? Bio { get; set; }
    public string? WorkingDescription { get; set; }
    public string? ImagePath { get; set; }
    public bool ReadyToWork { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<MedicalStaffServiceType> MedicalStaffServices { get; set; }
}
