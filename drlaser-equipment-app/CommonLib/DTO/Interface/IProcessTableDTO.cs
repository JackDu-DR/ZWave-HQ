using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("63273AC8-D932-4B3C-9D70-0BC76F805E3F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IProcessTableDTO : IBaseDTO
    {
        double XAxisPosition { get; set; }
        double YAxisPosition { get; set; }
        double TXAxisPosition { get; set; }
        double TYAxisPosition { get; set; }
        double ZAxisPosition { get; set; }
        bool IsSubstrateChuckVacuumEnabled { get; set; }
        ProTableUIElement ProTableUIElement { get; set; }
        MoveDirection MoveDirection { get; set; }
        MotionCmd MotionCmd { get; set; }
        CameraSelect CameraSelect { get; set; }
        double MoveRel { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
