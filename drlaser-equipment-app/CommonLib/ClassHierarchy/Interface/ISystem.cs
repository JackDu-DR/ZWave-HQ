using CommonLib.ClassHierarchy.Implementation;
using System;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("0767DABD-9DB6-4481-8914-57E527C90190")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ISystem
    {
        [ComVisible(false)]
        string BasePath { get; set; }
        Laser Laser { get; }
        CausewaySystem CausewaySystem { get; }
        ProcessSystem ProcessSystem { get; }
        Vision Vision { get; }
        Motion Motion { get; }
    }
}
