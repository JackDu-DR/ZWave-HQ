using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("9DB83651-38CE-4658-AB61-537DA2913E64")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMotionAxisControlDTO : IBaseDTO
    {
        double MovePos { get; set; }
        double VelPos { get; set; }
        double AcclPos { get; set; }
        double JerkPos { get; set; }
        double ActualMovePos { get; set; }
        double ActualVelPos { get; set; }
        double ActualAcclPos { get; set; }
        double ActualJerkPos { get; set; }
        bool IsRelPos { get; set; }
        bool EnableBtnIsActive { get; set; }
        MotionUIElement MotionUIElement { get; set; }
        MotionUIElement ExecutingBtn { get; set; }
        AxisSelection AxisSelection { get; set; }
        MotionCmd MotionCmd { get; set; }

        new void LoadDataFromJson(string json);
    }
}
