using Domain.Enums;

namespace Domain.Entities;

public class AdvImage : Base
{
    public string ImagePath { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
