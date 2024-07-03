using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("DF411358-DBE8-452D-8F7B-AAF53376DF70")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMasterDTO : IBaseDTO
    {
        string MessageContent { get; set; }
        new void LoadDataFromJson(string json);
    }
}
