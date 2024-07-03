using CommonLib.DTO.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("DD179A46-2F6C-47FF-9680-DCF0F30E9DA9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionKinematicsDTO : IMotionKinematicsDTO
    {
        public double Velocity { get; set; }
        public double Acceleration { get; set; }
        public double Jerk { get; set; }
    }
}
