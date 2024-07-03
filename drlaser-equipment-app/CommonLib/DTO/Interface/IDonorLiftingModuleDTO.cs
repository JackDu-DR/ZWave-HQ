using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("D4F9588B-DBC7-4B99-8B4F-8A0590A9612A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IDonorLiftingModuleDTO : IBaseDTO
    {
        double XAxisPosition { get; set; }
        double YAxisPosition { get; set; }
        double ZAxisPosition { get; set; }
        bool IsDonorChuckVacuumEnabled { get; set; }
        DonorLifterUIElement DonorLifterUIElement { get; set; }
        MoveDirection MoveDirection { get; set; }
        MotionCmd MotionCmd { get; set; }
        CameraSelect CameraSelect { get; set; }
        double MoveRel { get; set; }

        new void LoadDataFromJson(string json);
    }
}
