using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface.Configuration
{
    [Guid("91CF460D-3133-4541-991B-827635AFF712")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IProcessTableConfigurationDTO : IBaseDTO
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
