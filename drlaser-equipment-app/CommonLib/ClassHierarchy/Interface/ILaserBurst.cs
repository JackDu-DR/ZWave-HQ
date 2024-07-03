using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("8FA0F66C-CB3C-419C-B83E-0A44BADB4074")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaserBurst
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        string Fetch { get; }
        string Apply { get; }
        string Content { get; }
        string Powerlock { get; }

        string LaserBurstDTO { get; }
    }
}
