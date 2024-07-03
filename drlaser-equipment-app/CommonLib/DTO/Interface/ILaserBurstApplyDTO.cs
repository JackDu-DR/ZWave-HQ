using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("E27BA3E7-3030-4579-905F-D4B23B7396A8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaserBurstApplyDTO : IBaseDTO
    {
        double P { get; set; }

        double N { get; set; }

        double EnvelopeControlP { get; set; }

        double EnvelopeControlN { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
