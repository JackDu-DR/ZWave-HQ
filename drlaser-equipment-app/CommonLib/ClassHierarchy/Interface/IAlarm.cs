using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("650a532e-8b7d-41c9-882e-d7c9257ae0b5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IAlarm
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        string Fetch { get; }
        string AcknowlegedAlarm { get; }
        string CancelledAlarm { get; }
        string RetriedAlarm { get; }
        string New { get; }
        string Ack { get; }
        string Retry { get; }
        string Cancel { get; }
    }
}
