using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("C16D926D-86F2-4D69-BFD4-5FA69E8589D3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IBaseDTO
    {
        void LoadDataFromJson(string json);
    }
}
