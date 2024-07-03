using System;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("DA0770A2-2FBD-4023-94AC-A227231751DB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IMessage : IBaseDTO
    {
        string Id { get; set; }

        object Data { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
