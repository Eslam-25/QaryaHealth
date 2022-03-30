using QaryaHealth.Core.Enums;

namespace QaryaHealth.Core.Entities
{
    public class User: Base
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public BloodType BloodType { get; set; }
    }
}
