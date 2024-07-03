using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("981E16A1-D926-4A90-BD2F-CA59F92F1B0A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IAlarmRetryResultDTO : IBaseDTO
    {
        bool IsSuccess { get; set; }
        string AlarmId { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
