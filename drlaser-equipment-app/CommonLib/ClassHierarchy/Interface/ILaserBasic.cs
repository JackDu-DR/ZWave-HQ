using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("6945BF15-4867-4E06-AD46-7FCBD431F7CC")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaserBasic
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        string Fetch { get; }
        string Apply { get; }
        string Connect { get; }
        string StandBy { get; }
        string Content { get; }
        string Output { get; }

        string LaserBasicDTO { get; }
    }
}
