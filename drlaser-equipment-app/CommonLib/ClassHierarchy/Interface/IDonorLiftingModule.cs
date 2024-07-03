using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("0E6D46BC-8C64-4593-9C95-10CA6174BB34")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IDonorLiftingModule
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        string Fetch { get; }
        string Content { get; }

        string Move { get; }
        string Action { get; }
        string CameraChange { get; }

        string DonorChuckVacuumOutput { get; }
        string DonorLiftingModuleDTO { get; }
    }
}
