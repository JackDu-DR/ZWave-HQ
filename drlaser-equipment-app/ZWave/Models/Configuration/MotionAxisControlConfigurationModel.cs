using CommonLib.DTO.Implementation.Configuration;

namespace ZWave.Models
{
    public class MotionAxisControlConfigurationModel : BaseModel
    {
        private string _axisName;
        public string AxisName
        {
            get => _axisName;
            set => SetProperty(ref _axisName, value);
        }

        private double _positionREL_DefaultValue;
        public double PositionREL_DefaultValue
        {
            get => _positionREL_DefaultValue;
            set => SetProperty(ref _positionREL_DefaultValue, value);
        }

        private double _positionABS_DefaultValue;
        public double PositionABS_DefaultValue
        {
            get => _positionABS_DefaultValue;
            set => SetProperty(ref _positionABS_DefaultValue, value);
        }

        private double _velocity_DefaultValue;
        public double Velocity_DefaultValue
        {
            get => _velocity_DefaultValue;
            set => SetProperty(ref _velocity_DefaultValue, value);
        }

        private double _accel_DefaultValue;
        public double Accel_DefaultValue
        {
            get => _accel_DefaultValue;
            set => SetProperty(ref _accel_DefaultValue, value);
        }

        private double _jerk_DefaultValue;
        public double Jerk_DefaultValue
        {
            get => _jerk_DefaultValue;
            set => SetProperty(ref _jerk_DefaultValue, value);
        }

        public static MotionAxisControlConfigurationModel GetMotionAxisControlConfigurationModelFromDTO(MotionAxisControlConfigurationDTO motionAxisControlConfigurationDTO)
        {
            return new MotionAxisControlConfigurationModel
            {
                AxisName = motionAxisControlConfigurationDTO.AxisName,
                PositionREL_DefaultValue = motionAxisControlConfigurationDTO.PositionREL_DefaultValue,
                PositionABS_DefaultValue = motionAxisControlConfigurationDTO.PositionABS_DefaultValue,
                Velocity_DefaultValue = motionAxisControlConfigurationDTO.Velocity_DefaultValue,
                Accel_DefaultValue = motionAxisControlConfigurationDTO.Accel_DefaultValue,
                Jerk_DefaultValue = motionAxisControlConfigurationDTO.Jerk_DefaultValue,
            };
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
        }
    }
}