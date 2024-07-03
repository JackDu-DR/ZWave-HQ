using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("33E5E57B-5C55-4E6E-877E-A9E76BC843C2")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ICamera
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        string Exposure { get; }

        string Light { get; }
    }
}
