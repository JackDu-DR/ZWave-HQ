using CommonLib.DTO.Implementation;
using CommonLib.DTO.Implementation.Configuration;

namespace ZWave.Models
{
    public class InspectionVisionConfigurationModel : BaseModel
    {
        private string _cameraName;
        public string CameraName
        {
            get => _cameraName;
            set => SetProperty(ref _cameraName, value);
        }

        private int _zoomValue_Min;
        public int ZoomValue_Min
        {
            get => _zoomValue_Min;
            set => SetProperty(ref _zoomValue_Min, value);
        }

        private int _zoomValue_Max;
        public int ZoomValue_Max
        {
            get => _zoomValue_Max;
            set => SetProperty(ref _zoomValue_Max, value);
        }

        private double _cameraMaxSize_X;
        public double CameraMaxSize_X
        {
            get => _cameraMaxSize_X;
            set => SetProperty(ref _cameraMaxSize_X, value);
        }

        private double _cameraMaxSize_Y;
        public double CameraMaxSize_Y
        {
            get => _cameraMaxSize_Y;
            set => SetProperty(ref _cameraMaxSize_Y, value);
        }

        private double _exposureTime_Min;
        public double ExposureTime_Min
        {
            get => _exposureTime_Min;
            set => SetProperty(ref _exposureTime_Min, value);
        }

        private double _exposureTime_Max;
        public double ExposureTime_Max
        {
            get => _exposureTime_Max;
            set => SetProperty(ref _exposureTime_Max, value);
        }

        private string exposureTime_ValueType;
        public string ExposureTime_ValueType
        {
            get => exposureTime_ValueType;
            set => SetProperty(ref exposureTime_ValueType, value);
        }

        private double _gain_Min;
        public double Gain_Min
        {
            get => _gain_Min;
            set => SetProperty(ref _gain_Min, value);
        }

        private double _gain_Max;
        public double Gain_Max
        {
            get => _gain_Max;
            set => SetProperty(ref _gain_Max, value);
        }

        private string _gain_ValueType;
        public string Gain_ValueType
        {
            get => _gain_ValueType;
            set => SetProperty(ref _gain_ValueType, value);
        }

        private double _gamma_Min;
        public double Gamma_Min
        {
            get => _gamma_Min;
            set => SetProperty(ref _gamma_Min, value);
        }

        private double _gamma_Max;
        public double Gamma_Max
        {
            get => _gamma_Max;
            set => SetProperty(ref _gamma_Max, value);
        }

        private string _gamma_ValueType;
        public string Gamma_ValueType
        {
            get => _gamma_ValueType;
            set => SetProperty(ref _gamma_ValueType, value);
        }

        private double _blackLevel_Min;
        public double BlackLevel_Min
        {
            get => _blackLevel_Min;
            set => SetProperty(ref _blackLevel_Min, value);
        }

        private double _blackLevel_Max;
        public double BlackLevel_Max
        {
            get => _blackLevel_Max;
            set => SetProperty(ref _blackLevel_Max, value);
        }

        private string _blackLevel_ValueType;
        public string BlackLevel_ValueType
        {
            get => _blackLevel_ValueType;
            set => SetProperty(ref _blackLevel_ValueType, value);
        }

        private double _sharpness_Min;
        public double Sharpness_Min
        {
            get => _sharpness_Min;
            set => SetProperty(ref _sharpness_Min, value);
        }

        private double _sharpness_Max;
        public double Sharpness_Max
        {
            get => _sharpness_Max;
            set => SetProperty(ref _sharpness_Max, value);
        }

        private string _sharpness_ValueType;
        public string Sharpness_ValueType
        {
            get => _sharpness_ValueType;
            set => SetProperty(ref _sharpness_ValueType, value);
        }

        private double _phi_Min;
        public double Phi_Min
        {
            get => _phi_Min;
            set => SetProperty(ref _phi_Min, value);
        }

        private double _phi_Max;
        public double Phi_Max
        {
            get => _phi_Max;
            set => SetProperty(ref _phi_Max, value);
        }

        private string phi_ValueType;
        public string Phi_ValueType
        {
            get => phi_ValueType;
            set => SetProperty(ref phi_ValueType, value);
        }

        private bool _showSharpness;
        public bool ShowSharpness
        {
            get => _showSharpness;
            set => SetProperty(ref _showSharpness, value);
        }

        public static InspectionVisionConfigurationModel GetInspectionVisionConfigurationModelFromDTO(InspectionVisionConfigurationDTO inspectionVisionConfigurationDTO)
        {
            return new InspectionVisionConfigurationModel
            {
                CameraName = inspectionVisionConfigurationDTO.CameraName,
                ZoomValue_Min = inspectionVisionConfigurationDTO.ZoomValue_Min,
                ZoomValue_Max = inspectionVisionConfigurationDTO.ZoomValue_Max,
                CameraMaxSize_X = inspectionVisionConfigurationDTO.CameraMaxSize_X,
                CameraMaxSize_Y = inspectionVisionConfigurationDTO.CameraMaxSize_Y,
                ExposureTime_Min = inspectionVisionConfigurationDTO.ExposureTime_Min,
                ExposureTime_Max = inspectionVisionConfigurationDTO.ExposureTime_Max,
                ExposureTime_ValueType = inspectionVisionConfigurationDTO.ExposureTime_ValueType,
                Gain_Min = inspectionVisionConfigurationDTO.Gain_Min,
                Gain_Max = inspectionVisionConfigurationDTO.Gain_Max,
                Gain_ValueType = inspectionVisionConfigurationDTO.Gain_ValueType,
                Gamma_Min = inspectionVisionConfigurationDTO.Gamma_Min,
                Gamma_Max = inspectionVisionConfigurationDTO.Gamma_Max,
                Gamma_ValueType = inspectionVisionConfigurationDTO.Gamma_ValueType,
                BlackLevel_Min = inspectionVisionConfigurationDTO.BlackLevel_Min,
                BlackLevel_Max = inspectionVisionConfigurationDTO.BlackLevel_Max,
                BlackLevel_ValueType = inspectionVisionConfigurationDTO.BlackLevel_ValueType,
                Sharpness_Min = inspectionVisionConfigurationDTO.Sharpness_Min,
                Sharpness_Max = inspectionVisionConfigurationDTO.Sharpness_Max,
                Sharpness_ValueType = inspectionVisionConfigurationDTO.Sharpness_ValueType,
                Phi_Min = inspectionVisionConfigurationDTO.Phi_Min,
                Phi_Max = inspectionVisionConfigurationDTO.Phi_Max,
                Phi_ValueType = inspectionVisionConfigurationDTO.Phi_ValueType,
                ShowSharpness = inspectionVisionConfigurationDTO.ShowSharpness,
            };
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
        }
    }
}