using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface.Configuration
{
    [Guid("24EA0C0B-81D8-4170-A3B7-5F1D8FE47928")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionAxisControlConfigurationDTO : IBaseDTO
    {
        string AxisName { get; set; }
        double PositionREL_DefaultValue { get; set; }
        double PositionABS_DefaultValue { get; set; }
        double Velocity_DefaultValue { get; set; }
        double Accel_DefaultValue { get; set; }
        double Jerk_DefaultValue { get; set; }
        new void LoadDataFromJson(string json);
    }
}
