
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("59FDCC88-E411-4CD8-B0B2-7FD3C028E41F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionTestStateDTO
    {
        double CurrentCycles { get; set; }
        double TotalCycles { get; set; }
        double CommandPosition { get; set; }
        double EncoderPosition { get; set; }
        double MissingSteps { get; set; }
    }
}
