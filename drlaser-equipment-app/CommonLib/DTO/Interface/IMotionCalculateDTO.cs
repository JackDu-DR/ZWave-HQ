using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("EA4A1EEB-4E07-43D2-919E-7EBC584A86F9")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionCalculateDTO : IBaseDTO
    {
        double PointOneEstTime { get; set; }
        double PointTwoEstTime { get; set; }
        double TotalEstCycleTime { get; set; }
        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
