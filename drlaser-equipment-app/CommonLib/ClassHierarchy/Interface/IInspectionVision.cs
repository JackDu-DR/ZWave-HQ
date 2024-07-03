using CommonLib.ClassHierarchy.Implementation;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("DE7B4804-668A-43E6-AEBA-3A0884224479")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IInspectionVision
    {
        [ComVisible(false)]
        string BasePath { get; set; }
        string Fetch { get; }
        string Content { get; }

        string Connect { get; }
        string Live { get; }
        string Trigger { get; }
        string Save { get; }
        string ExposureTime { get; }
        string Gain { get; }
        string Gamma { get; }
        string GammaEnable { get; }
        string BlackLevel { get; }
        string Sharpness { get; }
        string ZoomValue { get; }
        string ScrollVertical { get; }
        string ScrollHorizontal { get; }
        string InspectionVisionDTO { get; }
        string ImageDTO { get; }
        string ROIDTO { get; }
        string RegionDTO { get; }
        string CircleROIResultId { get; }
        string RectangleROIResultId { get; }
        string EllipseROIResultId { get; }
        string SaveResultCommandId { get; }
        string MotionMovedId { get; }

        string Teach { get; }
        string NumLevelAutoNcc { get; }
        string AngStepAuto { get; }
        string NumLevel { get; }
        string AngleStart { get; }
        string AngleExtent { get; }
        string AngleStep { get; }

        string CalibrationAction { get; }
        string CalibrationResult { get; }
        string CalibrationModelId { get; }
        string CalibrationModelIdSelection { get; }
        string StepSize { get; }
        string StepCount { get; }
        string TransactionX { get; }
        string TransactionY { get; }
        string Rotation { get; }
        string ScaleX { get; }
        string ScaleY { get; }
        string PixelX { get; }
        string PixelY { get; }
        string FovX { get; }
        string FovY { get; }
        //Camera Camera { get; }
    }
}
