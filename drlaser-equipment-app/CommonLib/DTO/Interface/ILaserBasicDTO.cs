using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("E5C560A8-2E34-4DF1-B23C-871917948FAD")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaserBasicDTO : IBaseDTO
    {
        bool IsOutputEnabled { get; set; }

        double AttenuatorPercentage { get; set; }

        double PPDivider { get; set; }

        PresetControl PresetControl { get; set; }

        double PulseDuration { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
