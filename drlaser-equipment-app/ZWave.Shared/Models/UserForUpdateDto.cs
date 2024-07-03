using ZWave.Shared.Enums;

namespace ZWave.Shared.Models
{
    public class UserForUpdateDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedTime { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }
        public required UserRole UserRole { get; set; }
    }
}