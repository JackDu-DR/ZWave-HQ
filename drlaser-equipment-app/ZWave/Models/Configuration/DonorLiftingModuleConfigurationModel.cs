using CommonLib.DTO.Implementation.Configuration;

namespace ZWave.Models
{
    public class DonorLiftingModuleConfigurationModel : BaseModel
    {
        private string _axisName;
        public string AxisName
        {
            get => _axisName;
            set => SetProperty(ref _axisName, value);
        }

        private double _arrowContentLeft1DefaultValue;
        public double ArrowContentLeft1DefaultValue
        {
            get => _arrowContentLeft1DefaultValue;
            set => SetProperty(ref _arrowContentLeft1DefaultValue, value);
        }

        private double _arrowContentLeft2DefaultValue;
        public double ArrowContentLeft2DefaultValue
        {
            get => _arrowContentLeft2DefaultValue;
            set => SetProperty(ref _arrowContentLeft2DefaultValue, value);
        }

        private double _arrowContentLeft3DefaultValue;
        public double ArrowContentLeft3DefaultValue
        {
            get => _arrowContentLeft3DefaultValue;
            set => SetProperty(ref _arrowContentLeft3DefaultValue, value);
        }

        private double _arrowContentRight1DefaultValue;
        public double ArrowContentRight1DefaultValue
        {
            get => _arrowContentRight1DefaultValue;
            set => SetProperty(ref _arrowContentRight1DefaultValue, value);
        }

        private double _arrowContentRight2DefaultValue;
        public double ArrowContentRight2DefaultValue
        {
            get => _arrowContentRight2DefaultValue;
            set => SetProperty(ref _arrowContentRight2DefaultValue, value);
        }

        private double _arrowContentRight3DefaultValue;
        public double ArrowContentRight3DefaultValue
        {
            get => _arrowContentRight3DefaultValue;
            set => SetProperty(ref _arrowContentRight3DefaultValue, value);
        }

        private double _entryValue_Min;
        public double EntryValue_Min
        {
            get => _entryValue_Min;
            set => SetProperty(ref _entryValue_Min, value);
        }

        private double _entryValue_Max;
        public double EntryValue_Max
        {
            get => _entryValue_Max;
            set => SetProperty(ref _entryValue_Max, value);
        }

        public static DonorLiftingModuleConfigurationModel GetDonorLiftingModuleConfigurationModelFromDTO(DonorLiftingModuleConfigurationDTO donorLiftingModuleConfigurationDTO)
        {
            return new DonorLiftingModuleConfigurationModel
            {
                AxisName = donorLiftingModuleConfigurationDTO.AxisName,
                ArrowContentLeft1DefaultValue = donorLiftingModuleConfigurationDTO.ArrowContentLeft1DefaultValue,
                ArrowContentLeft2DefaultValue = donorLiftingModuleConfigurationDTO.ArrowContentLeft2DefaultValue,
                ArrowContentLeft3DefaultValue = donorLiftingModuleConfigurationDTO.ArrowContentLeft3DefaultValue,
                ArrowContentRight1DefaultValue = donorLiftingModuleConfigurationDTO.ArrowContentRight1DefaultValue,
                ArrowContentRight2DefaultValue = donorLiftingModuleConfigurationDTO.ArrowContentRight2DefaultValue,
                ArrowContentRight3DefaultValue = donorLiftingModuleConfigurationDTO.ArrowContentRight3DefaultValue,
                EntryValue_Min = donorLiftingModuleConfigurationDTO.EntryValue_Min,
                EntryValue_Max = donorLiftingModuleConfigurationDTO.EntryValue_Max,
            };
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
        }
    }
}