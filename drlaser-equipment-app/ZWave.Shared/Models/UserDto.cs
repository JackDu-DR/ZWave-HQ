using ZWave.Shared.Enums;

namespace ZWave.Shared.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? FullName { get; set; }
        public required string UserName { get; set; }
        public bool IsActive { get; set; }
        public required UserRole UserRole { get; set; }
    }
}
