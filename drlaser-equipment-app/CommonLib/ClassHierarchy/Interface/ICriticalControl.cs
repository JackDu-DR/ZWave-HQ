using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("26E7490B-D8C8-440F-A3A7-AD715F9777DB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ICriticalControl
    {
        [ComVisible(false)]
        string BasePath { get; }
        string Fetch { get; }
        string Update { get; }
    }
}