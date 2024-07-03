using CommonLib.DTO.Implementation;

namespace ZWave.Models
{
    public class AxisControlModel : BaseModel
    {
        private string _axisName = string.Empty;
        public string AxisName
        {
            get => _axisName;
            set => SetProperty(ref _axisName, value);
        }

        private double _positionRel;
        public double PositionRel
        {
            get => _positionRel;
            set
            {
                SetProperty(ref _positionRel, value);
                OnPropertyChanged(nameof(PositionRelValidateErrorMessage));
            }
        }

        private double _positionAbs;
        public double PositionAbs
        {
            get => _positionAbs;
            set
            {
                SetProperty(ref _positionAbs, value);
                OnPropertyChanged(nameof(PositionAbsValidateErrorMessage));
            }
        }

        private double _positionMin;
        public double PositionMin
        {
            get => _positionMin;
            set => SetProperty(ref _positionMin, value);
        }

        private double _positionMax;
        public double PositionMax
        {
            get => _positionMax;
            set => SetProperty(ref _positionMax, value);
        }

        private double _velocity;
        public double Velocity
        {
            get => _velocity;
            set
            {
                SetProperty(ref _velocity, value);
                OnPropertyChanged(nameof(VelocityValidateErrorMessage));
            }
        }

        private double _velocityMin;
        public double VelocityMin
        {
            get => _velocityMin;
            set => SetProperty(ref _velocityMin, value);
        }

        private double _velocityMax;
        public double VelocityMax
        {
            get => _velocityMax;
            set => SetProperty(ref _velocityMax, value);
        }

        private double _accel;
        public double Accel
        {
            get => _accel;
            set
            {
                SetProperty(ref _accel, value);
                OnPropertyChanged(nameof(AccelValidateErrorMessage));
            }
        }

        private double _accelMin;
        public double AccelMin
        {
            get => _accelMin;
            set => SetProperty(ref _accelMin, value);
        }

        private double _accelMax;
        public double AccelMax
        {
            get => _accelMax;
            set => SetProperty(ref _accelMax, value);
        }

        private double _jerk;
        public double Jerk
        {
            get => _jerk;
            set
            {
                SetProperty(ref _jerk, value);
                OnPropertyChanged(nameof(JerkValidateErrorMessage));
            }
        }

        private double _jerkMin;
        public double JerkMin
        {
            get => _jerkMin;
            set => SetProperty(ref _jerkMin, value);
        }

        private double _jerkMax;
        public double JerkMax
        {
            get => _jerkMax;
            set => SetProperty(ref _jerkMax, value);
        }
        public string Unit { get; set; } = "mm";
        public string PositionUnit => Unit;
        public string VelocityUnit => Unit;
        public string AccelUnit => Unit + "/s" + '\u00B2';
        public string JerkUnit => Unit + "/s" + '\u00B3';

        public string PositionRelValidateErrorMessage
            => OutOfRangeValidateMessge(PositionRel, PositionMin, PositionMax);
        public string PositionAbsValidateErrorMessage
            => OutOfRangeValidateMessge(PositionAbs, PositionMin, PositionMax);
        public string VelocityValidateErrorMessage
            => OutOfRangeValidateMessge(Velocity, VelocityMin, VelocityMax);
        public string AccelValidateErrorMessage
            => OutOfRangeValidateMessge(Accel, AccelMin, AccelMax);
        public string JerkValidateErrorMessage
            => OutOfRangeValidateMessge(Jerk, JerkMin, JerkMax);

        public static AxisControlModel GetAxisControlModelFromDTO(AxisConfigDTO axisConfigDTO)
        {
            return new AxisControlModel
            {
                AxisName = axisConfigDTO.AxisName,
                PositionRel = axisConfigDTO.PositionRel,
                PositionAbs = axisConfigDTO.PositionAbs,
                PositionMin = axisConfigDTO.PositionMin,
                PositionMax = axisConfigDTO.PositionMax,
                Velocity = axisConfigDTO.Velocity,
                VelocityMin = axisConfigDTO.VelocityMin,
                VelocityMax = axisConfigDTO.VelocityMax,
                Accel = axisConfigDTO.Accel,
                AccelMin = axisConfigDTO.AccelMin,
                AccelMax = axisConfigDTO.AccelMax,
                Jerk = axisConfigDTO.Jerk,
                JerkMin = axisConfigDTO.JerkMin,
                JerkMax = axisConfigDTO.JerkMax,
                Unit = axisConfigDTO.Unit,
            };
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
        }

        // TODO: Localization for error message - Bug: https://sioux.atlassian.net/browse/DRL-302
        private string OutOfRangeValidateMessge(double value, double valueMin, double valueMax)
        {
            if (value < valueMin || value > valueMax)
            {
                return "Value must be between " + valueMin + " - " + valueMax;
            }
            return string.Empty;
        }
    }
}