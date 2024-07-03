using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("AB5B0576-FA34-47C7-8B20-F99ED717A055")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface ICameraParamsDTO : IBaseDTO
    {
        double Exposure { get; set; }

        bool Light { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);

        byte[] ConvertToBytes(object obj);
    }
}
