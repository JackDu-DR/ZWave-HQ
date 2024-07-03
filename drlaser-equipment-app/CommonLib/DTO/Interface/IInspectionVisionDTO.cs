using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("F0EB9FDC-014E-4345-A0D8-929A6CC9E36F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IInspectionVisionDTO : IBaseDTO
    {
        bool Connected { get; set; }
        bool Acquisition { get; set; }
        bool GrabImage { get; set; }
        bool GammaEnable { get; set; }
        double ExposureTime { get; set; }
        double Gain { get; set; }
        double Gamma { get; set; }
        double BlackLevel { get; set; }
        double Sharpness { get; set; }
        int ZoomValue { get; set; }
        int ScrollVertical { get; set; }
        int ScrollHorizontal { get; set; }
        ProSystemUpLookInspecVisionUIElement ProSystemUpLookInspecVisionUIElement { get; set; }
        CameraSelect CameraSelect { get; set; }
        InspectionVisionPage InspectionVisionPage { get; set; }

        bool NumLevelAutoNcc { get; set; }
        bool AngStepAuto { get; set; }
        double NumLevel { get; set; }
        double AngleStart { get; set; }
        double AngleExtent { get; set; }
        double AngleStep { get; set; }
        ModelSelect ModelSelect { get; set; }
        MetricSelect MetricSelect { get; set; }

        string CalibrationModelId { get; set; }
        string CalibrationModelIdSelection { get; set; }
        double StepSize { get; set; }
        double StepCount { get; set; }
        double TransactionX { get; set; }
        double TransactionY { get; set; }
        double Rotation { get; set; }
        double ScaleX { get; set; }
        double ScaleY { get; set; }
        double PixelX { get; set; }
        double PixelY { get; set; }
        double FovX { get; set; }
        double FovY { get; set; }
        ProSystemVCalibartionUIElement ProSystemVCalibartionUIElement { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
