namespace ZWave.Shared.Models
{
    public class AlarmGuidelineDto
    {
        public required int AlarmCode { get; set; }
        public string Description { get; set; } = string.Empty;
        public string RecoveryGuideline { get; set; } = string.Empty;
    }
}
