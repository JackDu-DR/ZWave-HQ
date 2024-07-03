using CommonLib.DTO.Interface;
using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("3DDDDE69-63C9-4905-80DF-A652A7C67DC4")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CriticalActionDTO : ICriticalActionDTO
    {
        public CriticalType CriticalType { get; set; }
        public bool IsProcessing { get; set; }
        public int Timeout { get; set; }
    }
}
