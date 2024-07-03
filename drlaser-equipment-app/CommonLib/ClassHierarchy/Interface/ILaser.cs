using CommonLib.ClassHierarchy.Implementation;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("F59A39EC-D116-414C-ACC2-FABBB17F7B42")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaser
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        LaserBurst LaserBurst { get; }

        LaserBasic LaserBasic { get; }

        string Info { get; }

        string Fetch { get; }
    }
}
