using ZWave.Shared.Enums;

namespace ZWave.Shared.Models
{
    public class UserForCreateDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public string? FullName { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public required UserRole UserRole { get; set; }
        public required Guid MachineId { get; set; }
    }
}
