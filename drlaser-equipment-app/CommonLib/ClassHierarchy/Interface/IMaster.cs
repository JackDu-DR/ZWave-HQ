using System;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("907A04F2-3B5D-426B-9F08-A67BA1AA5873")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IMaster
    {
        [ComVisible(false)]
        string BasePath { get; }
        string Message { get; }
        Implementation.System System { get; }
        Implementation.Configuration Configuration { get; }
        Implementation.Alarm Alarm { get; }
        Implementation.CriticalControl CriticalControl { get; }
        string Content { get; }

    }
}
