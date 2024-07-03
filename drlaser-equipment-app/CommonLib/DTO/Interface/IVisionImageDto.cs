using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("27C2F164-F85D-4822-BC28-F4DDAAC162D1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IVisionImageDto : IBaseDTO
    {
        double FullImageWidth { get; set; }
        double FullImageHeight { get; set; }
        double ImageVisualWidth { get; set; }
        double ImageVisualHeight { get; set; }
        double ScrollX { get; set; }
        double ScrollY { get; set; }
        int Ratio { get; set; }
    }
}
