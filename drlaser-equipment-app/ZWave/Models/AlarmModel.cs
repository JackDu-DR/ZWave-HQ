using CommonLib.Enums;

namespace ZWave.Models
{
    public class AlarmModel : AlarmGuidelineModel
    {
        public string Id { get; set; }
        public string TimeSpan { get; set; }
        public AlarmSeverity Severity { get; set; }
        public string DisplaySeverity => Enum.GetName(typeof(AlarmSeverity), Severity);
        public bool WaitForResponse { get; set; }
        public bool AckEnabled { get; private set; }
        public bool RetryEnabled { get; private set; }
        public bool CancelEnabled { get; private set; }

        public AlarmModel() : base()
        {
            Id = null;
            TimeSpan = string.Empty;
            Severity = AlarmSeverity.Low;
            WaitForResponse = false;
            AckEnabled = false;
            RetryEnabled = false;
            CancelEnabled = false;
        }

        public AlarmModel(string id, double timeSpan, AlarmSeverity severity, bool waitForResponse, bool ackEnabled, bool retryEnabled, bool cancelEnabled, int alarmCode, string description, string recoveryGuideline) 
            : base(alarmCode, description, recoveryGuideline)
        {
            Id = id;
            TimeSpan = DateTime.UnixEpoch.AddSeconds(timeSpan).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");
            Severity = severity;
            WaitForResponse = waitForResponse;
            AckEnabled = ackEnabled;
            RetryEnabled = retryEnabled;
            CancelEnabled = cancelEnabled;
        }
    }
}
