using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface.Configuration
{
    [Guid("2F3DB086-CCE4-4350-A0AE-28354C30F404")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IInspectionVisionConfigurationDTO : IBaseDTO
    {
        string CameraName { get; set; }
        int ZoomValue_Min { get; set; }
        int ZoomValue_Max { get; set; }
        double CameraMaxSize_X { get; set; }
        double CameraMaxSize_Y { get; set; }
        double ExposureTime_Min { get; set; }
        double ExposureTime_Max { get; set; }
        string ExposureTime_ValueType { get; set; }
        double Gain_Min { get; set; }
        double Gain_Max { get; set; }
        string Gain_ValueType { get; set; }
        double Gamma_Min { get; set; }
        double Gamma_Max { get; set; }
        string Gamma_ValueType { get; set; }
        double BlackLevel_Min { get; set; }
        double BlackLevel_Max { get; set; }
        string BlackLevel_ValueType { get; set; }
        double Sharpness_Min { get; set; }
        double Sharpness_Max { get; set; }
        string Sharpness_ValueType { get; set; }
        double Phi_Min { get; set; }
        double Phi_Max { get; set; }
        string Phi_ValueType { get; set; }
        bool ShowSharpness { get; set; }
        new void LoadDataFromJson(string json);
    }
}
