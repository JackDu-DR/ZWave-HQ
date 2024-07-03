using CommonLib.DTO.Interface.Configuration;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation.Configuration
{
    [Guid("0C8888D7-0B06-48AA-B7A2-CC3937D94144")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class InspectionVisionConfigurationDTO : BaseDTO, IInspectionVisionConfigurationDTO
    {
        public string CameraName { get; set; } = string.Empty;
        public int ZoomValue_Min { get; set; }
        public int ZoomValue_Max { get; set; }
        public double CameraMaxSize_X { get; set; }
        public double CameraMaxSize_Y { get; set; }
        public double ExposureTime_Min { get; set; }
        public double ExposureTime_Max { get; set; }
        public string ExposureTime_ValueType { get; set; } = string.Empty;
        public double Gain_Min { get; set; }
        public double Gain_Max { get; set; }
        public string Gain_ValueType { get; set; } = string.Empty;
        public double Gamma_Min { get; set; }
        public double Gamma_Max { get; set; }
        public string Gamma_ValueType { get; set; } = string.Empty;
        public double BlackLevel_Min { get; set; }
        public double BlackLevel_Max { get; set; }
        public string BlackLevel_ValueType { get; set; } = string.Empty;
        public double Sharpness_Min { get; set; }
        public double Sharpness_Max { get; set; }
        public string Sharpness_ValueType { get; set; } = string.Empty;
        public double Phi_Min { get; set; }
        public double Phi_Max { get; set; }
        public string Phi_ValueType { get; set; } = string.Empty;
        public bool ShowSharpness { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<InspectionVisionConfigurationDTO>(json);

            if (newObject == null) return;

            CameraName = newObject.CameraName;
            ZoomValue_Min = newObject.ZoomValue_Min;
            ZoomValue_Max = newObject.ZoomValue_Max;
            CameraMaxSize_X = newObject.CameraMaxSize_X;
            CameraMaxSize_Y = newObject.CameraMaxSize_Y;
            ExposureTime_Min = newObject.ExposureTime_Min;
            ExposureTime_Max = newObject.ExposureTime_Max;
            ExposureTime_ValueType = newObject.ExposureTime_ValueType;
            Gain_Min = newObject.Gain_Min;
            Gain_Max = newObject.Gain_Max;
            Gain_ValueType = newObject.Gain_ValueType;
            Gamma_Min = newObject.Gamma_Min;
            Gamma_Max = newObject.Gamma_Max;
            Gamma_ValueType = newObject.Gamma_ValueType;
            BlackLevel_Min = newObject.BlackLevel_Min;
            BlackLevel_Max = newObject.BlackLevel_Max;
            BlackLevel_ValueType = newObject.BlackLevel_ValueType;
            Sharpness_Min = newObject.Sharpness_Min;
            Sharpness_Max = newObject.Sharpness_Max;
            Sharpness_ValueType = newObject.Gain_ValueType;
            Phi_Min = newObject.Phi_Min;
            Phi_Max = newObject.Phi_Max;
            Phi_ValueType = newObject.Phi_ValueType;
            ShowSharpness = newObject.ShowSharpness;
        }
    }
}
