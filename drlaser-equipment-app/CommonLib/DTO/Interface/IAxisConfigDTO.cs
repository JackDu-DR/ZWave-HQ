using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("55105C56-BEAC-4A16-A07C-C47E587D4A98")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IAxisConfigDTO : IBaseDTO
    {
        string AxisName { get; set; }
        double PositionRel { get; set; }
        double PositionAbs { get; set; }
        double PositionMin { get; set; }
        double PositionMax { get; set; }
        double Velocity { get; set; }
        double VelocityMin { get; set; }
        double VelocityMax { get; set; }
        double Accel { get; set; }
        double AccelMin { get; set; }
        double AccelMax { get; set; }
        double Jerk { get; set; }
        double JerkMin { get; set; }
        double JerkMax { get; set; }
        string Unit { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
