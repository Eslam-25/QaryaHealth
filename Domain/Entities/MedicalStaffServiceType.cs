namespace Domain.Entities;

public class MedicalStaffServiceType : Base
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Cost { get; set; }

    public int StaffId { get; set; }
    public MedicalStaff? MedicalStaff { get; set; }
}
