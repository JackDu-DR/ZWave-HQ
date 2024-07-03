using CommonLib.DTO.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("8E1CDBD0-E697-4E95-BB95-C01A3A288C77")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionTestStateDTO : IMotionTestStateDTO
    {
        public double CurrentCycles { get; set; }
        public double TotalCycles { get; set; }
        public double CommandPosition { get; set; }
        public double EncoderPosition { get; set; }
        public double MissingSteps { get; set; }
    }
}
