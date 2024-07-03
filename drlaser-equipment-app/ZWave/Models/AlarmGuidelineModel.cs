namespace ZWave.Models
{
    public class AlarmGuidelineModel
    {
        public int AlarmCode { get; set; }
        public string Description { get; set; }
        public string RecoveryGuideline { get; set; }

        public AlarmGuidelineModel()
        {
            AlarmCode = -1;
            Description = string.Empty;
            RecoveryGuideline = string.Empty;
        }

        public AlarmGuidelineModel(int alarmCode, string description, string recoveryGuideline)
        {
            AlarmCode = alarmCode;
            Description = description;
            RecoveryGuideline = recoveryGuideline;
        }
    }
}
