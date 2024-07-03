using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("29BF8684-C05F-4FCB-83FB-EC8CBB93E3E8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionKinematicsDTO
    {
        double Velocity { get; set; }
        double Acceleration { get; set; }
        double Jerk { get; set; }
    }
}
