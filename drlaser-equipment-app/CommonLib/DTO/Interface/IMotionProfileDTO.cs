using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("F401AFA0-D7D2-49A4-8AB8-E57DDE516216")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionProfileDTO : IBaseDTO
    {
        string Name { get; set; }
        double Distance { get; set; }
        double Time { get; set; }
        double Velocity { get; set; }
        double Acceleration { get; set; }
        double Jerk { get; set; }
        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
