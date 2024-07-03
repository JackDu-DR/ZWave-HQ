using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("16c039b0-408f-4563-94ae-5b054512f920")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class AlarmDTO : IAlarmDTO
    {
        public required string ErrorId { get; set; }
        public double TimeSpan { get; set; }
        public AlarmSeverity Severity { get; set; }
        public int ErrorCode { get; set; }
        public int WaitResp { get; set; }
        public int Ack { get; set; }
        public int Retry { get; set; }
        public int Cancel { get; set; }

        public bool WaitForResponse => WaitResp != 0;
        public bool AckEnabled => Ack != 0;
        public bool RetryEnabled => Retry != 0;
        public bool CancelEnabled => Cancel != 0;

        public void LoadDataFromJson(string json)
        {
            var data = JsonConvert.DeserializeObject<AlarmDTO>(json);

            if (data != null)
            {
                TimeSpan = data.TimeSpan;
                Severity = data.Severity;
                ErrorCode = data.ErrorCode;
                WaitResp = data.WaitResp;
                Ack = data.Ack;
                Retry = data.Retry;
                Cancel = data.Cancel;
            }
        }
    }
}
