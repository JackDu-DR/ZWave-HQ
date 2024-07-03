using System.Runtime.InteropServices;
using CommonLib.Enums;

namespace CommonLib.DTO.Interface
{
    [Guid("EC81DAEE-90D9-4932-A072-0D85E845450C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface ICriticalActionDTO
    {
        CriticalType CriticalType { get; set; }
        bool IsProcessing { get; set; }

        /// <summary>
        /// Timeout in miliseconds
        /// </summary>
        int Timeout { get; set; }
    }
}
