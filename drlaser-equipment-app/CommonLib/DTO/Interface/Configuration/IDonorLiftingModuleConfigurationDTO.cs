using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface.Configuration
{
    [Guid("63C0B2D7-07EB-428A-8D8B-6F7BEB8137D3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IDonorLiftingModuleConfigurationDTO : IBaseDTO
    {
        string AxisName { get; set; }
        double ArrowContentLeft1DefaultValue { get; set; }
        double ArrowContentLeft2DefaultValue { get; set; }
        double ArrowContentLeft3DefaultValue { get; set; }
        double ArrowContentRight1DefaultValue { get; set; }
        double ArrowContentRight2DefaultValue { get; set; }
        double ArrowContentRight3DefaultValue { get; set; }
        double EntryValue_Min { get; set; }
        double EntryValue_Max { get; set; }
        new void LoadDataFromJson(string json);
    }
}
