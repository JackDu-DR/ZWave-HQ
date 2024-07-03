using CommonLib.DTO.Interface.Configuration;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation.Configuration
{
    [Guid("05687414-5B91-466E-91B0-0D24AEB1ADBB")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionAxisControlConfigurationDTO : BaseDTO, IMotionAxisControlConfigurationDTO
    {
        public string AxisName { get; set; } = string.Empty;
        public double PositionREL_DefaultValue { get; set; }
        public double PositionABS_DefaultValue { get; set; }
        public double Velocity_DefaultValue { get; set; }
        public double Accel_DefaultValue { get; set; }
        public double Jerk_DefaultValue { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<MotionAxisControlConfigurationDTO>(json);

            if (newObject == null) return;

            AxisName = newObject.AxisName;
            PositionREL_DefaultValue = newObject.PositionREL_DefaultValue;
            PositionABS_DefaultValue = newObject.PositionABS_DefaultValue;
            Velocity_DefaultValue = newObject.Velocity_DefaultValue;
            Accel_DefaultValue = newObject.Accel_DefaultValue;
            Jerk_DefaultValue = newObject.Jerk_DefaultValue;
        }
    }
}
