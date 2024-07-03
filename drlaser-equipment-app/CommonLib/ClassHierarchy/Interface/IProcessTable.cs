using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("E48D773F-A494-4AD5-B2D6-95624AB80EEB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IProcessTable
    {
        [ComVisible(false)]
        string BasePath { get; set; }
        string Fetch { get; }
        string Content { get; }

        string Move { get; }
        string Action { get; }
        string CameraChange { get; }

        string SubstrateChuckVacuumOutput { get; }

        string ProcessTableDTO { get; }
    }
}
