using CommonLib.DTO.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("291D093E-9336-44AB-B4A9-708ADD4E1D20")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public abstract class BaseDTO : IBaseDTO
    {
        public abstract void LoadDataFromJson(string json);
    }
}
