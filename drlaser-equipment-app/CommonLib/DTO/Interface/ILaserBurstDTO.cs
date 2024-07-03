using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("2B734C43-3679-4B39-8ED6-FCBD57CA4E14")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaserBurstDTO : IBaseDTO
    {
        bool IsPowerlockEnabled { get; set; }

        double P { get; set; }

        double N { get; set; }

        double EnvelopeControlP { get; set; }

        double EnvelopeControlN { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
