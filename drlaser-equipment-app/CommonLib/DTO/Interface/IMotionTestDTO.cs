using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("13E3C1DB-4FC6-4713-8CE5-FC719DE5B84F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionTestDTO : IBaseDTO
    {
        string Name { get; set; }
        double PointOne { get; set; }
        double PointOneDelay { get; set; }
        double PointTwo { get; set; }
        double PointTwoDelay { get; set; }
        double NoOfCycles { get; set; }
        double Velocity { get; set; }
        double Acceleration { get; set; }
        double Jerk { get; set; }
        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
