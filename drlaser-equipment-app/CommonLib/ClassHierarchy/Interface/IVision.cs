using CommonLib.ClassHierarchy.Implementation;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("7FDF4B10-6866-42BE-B572-1F7006E00527")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IVision
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        string Connect { get; }

        string Trigger { get; }

        string CameraParams { get; }

        string Exposure { get; }

        string Light { get; }

        string ImageDTO { get; }

        string ROIDTO { get; }
        string CircleROIResultId { get; }
        string RectangleROIResultId { get; }
        string EllipseROIResultId { get; }
        string SaveResultCommandId { get; }

        Camera Camera { get; }

        string MotionMovedId { get; }
    }
}
