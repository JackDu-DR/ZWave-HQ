using CommonLib.DTO.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("46EBCF03-ECE7-466E-8700-09B8660BAA6E")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionProfileDTO : IMotionProfileDTO
    {
        public required string Name { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public double Velocity { get; set; }
        public double Acceleration { get; set; }
        public double Jerk { get; set; }

        public void LoadDataFromJson(string json)
        {
            throw new NotImplementedException();
        }
    }
}
