using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("6C79D934-E8AE-42F2-970E-09D8B029D01F")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class AxisConfigDTO : IAxisConfigDTO
    {
        public string AxisName { get; set; } = string.Empty;
        public double PositionRel { get; set; }
        public double PositionAbs { get; set; }
        public double PositionMin { get; set; }
        public double PositionMax { get; set; }
        public double Velocity { get; set; }
        public double VelocityMin { get; set; }
        public double VelocityMax { get; set; }
        public double Accel { get; set; }
        public double AccelMin { get; set; }
        public double AccelMax { get; set; }
        public double Jerk { get; set; }
        public double JerkMin { get; set; }
        public double JerkMax { get; set; }
        public string Unit { get; set; } = string.Empty;

        public void LoadDataFromJson(string json)
        {
            var data = JsonConvert.DeserializeObject<AxisConfigDTO>(json);

            if (data == null) return;
            
            AxisName = data.AxisName;
            PositionRel = data.PositionRel;
            PositionAbs = data.PositionAbs;
            PositionMin = data.PositionMin;
            PositionMax = data.PositionMax;
            Velocity = data.Velocity;
            VelocityMin = data.VelocityMin;
            VelocityMax = data.VelocityMax;
            Accel = data.Accel;
            AccelMin = data.AccelMin;
            AccelMax = data.AccelMax;
            Jerk = data.Jerk;
            JerkMin = data.JerkMin;
            JerkMax = data.JerkMax;
            Unit = data.Unit;
        }
    }
}
