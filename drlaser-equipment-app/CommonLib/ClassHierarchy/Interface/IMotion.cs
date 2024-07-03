using CommonLib.ClassHierarchy.Implementation;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("71B1A35B-8828-4B54-8542-BA28E8D25CC9")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IMotion
    {
        [ComVisible(false)]
        string BasePath { get; set; }
        MotionControl MotionControl { get; }

        MotionAxisControl MotionAxisControl { get; }

        string Info { get; }

        string Fetch { get; }
    }
}
