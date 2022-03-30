using QaryaHealth.Core.Enums;

namespace QaryaHealth.Service.Dtos
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public BloodType BloodType { get; set; }
    }
}