using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("EB90CE05-F550-4932-AA58-6406B8DD483C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionMovedDTO : IBaseDTO
    {
        double TransitionX { get; set; }
        double TransitionY { get; set; }
        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
