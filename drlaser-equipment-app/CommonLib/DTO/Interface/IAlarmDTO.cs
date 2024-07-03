using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("c30e197a-1db5-4ee9-93bf-a8cd27914eb8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IAlarmDTO : IBaseDTO
    {
        string ErrorId { get; set; }
        double TimeSpan { get; set; }
        AlarmSeverity Severity { get; set; }
        int ErrorCode { get; set; }
        int WaitResp {  get; set; }
        int Ack { get; set; }
        int Retry { get; set; }
        int Cancel { get; set; }

        [ComVisible(false)]
        bool WaitForResponse { get; }
        [ComVisible(false)]
        bool AckEnabled { get; }
        [ComVisible(false)]
        bool RetryEnabled { get; }
        [ComVisible(false)]
        bool CancelEnabled { get; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
