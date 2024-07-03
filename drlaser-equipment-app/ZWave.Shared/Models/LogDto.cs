using ZWave.Shared.Enums;

namespace ZWave.Shared.Models
{
    public class LogDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public LogAction Action { get; set; }

        public string Message { get; set; }

        public bool IsMachineLog { get; set; }

        public Guid? UserId { get; set; }

        public string? UserName { get; set; }
    }
}
