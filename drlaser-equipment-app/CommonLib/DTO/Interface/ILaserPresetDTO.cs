using CommonLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DTO.Interface
{
    [Guid("3918078C-4E0F-4C81-ABE2-97BA65B38AC5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]

    public interface ILaserPresetDTO : IBaseDTO
    {
        public PresetControl presetIndex { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
