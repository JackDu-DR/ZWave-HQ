namespace ZWave.Shared.Models
{
    public class ChangePasswordDto
    {
        public required Guid UserId { get; set; }
        public required string NewPasswordHash { get; set; }
    }
}
