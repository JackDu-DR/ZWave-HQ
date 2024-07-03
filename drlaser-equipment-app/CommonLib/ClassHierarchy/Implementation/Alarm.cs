using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("f198643e-f54c-4687-8b90-9259ca6e005d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Alarm : IAlarm
    {
        public string BasePath { get; set; }

        public Alarm(string path, string key = null)
        {
            BasePath = path + nameof(Alarm) + key + ".";
        }
        public string Fetch => BasePath + nameof(Fetch);
        public string AcknowlegedAlarm => BasePath + nameof(AcknowlegedAlarm);
        public string CancelledAlarm => BasePath + nameof(CancelledAlarm);
        public string RetriedAlarm => BasePath + nameof(RetriedAlarm);
        public string New => BasePath + nameof(New);
        public string Ack => BasePath + nameof(Ack);
        public string Retry => BasePath + nameof(Retry);
        public string Cancel => BasePath + nameof(Cancel);
    }
}
