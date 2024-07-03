using CommonLib.DTO.Interface.Configuration;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation.Configuration
{
    [Guid("2C4CD941-73D7-46DF-836C-FDB7D272EA3E")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class ProcessTableConfigurationDTO : BaseDTO, IProcessTableConfigurationDTO
    {
        public string AxisName { get; set; } = string.Empty;
        public double ArrowContentLeft1DefaultValue { get; set; }
        public double ArrowContentLeft2DefaultValue { get; set; }
        public double ArrowContentLeft3DefaultValue { get; set; }
        public double ArrowContentRight1DefaultValue { get; set; }
        public double ArrowContentRight2DefaultValue { get; set; }
        public double ArrowContentRight3DefaultValue { get; set; }
        public double EntryValue_Min { get; set; }
        public double EntryValue_Max { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<ProcessTableConfigurationDTO>(json);

            if (newObject == null) return;

            AxisName = newObject.AxisName;
            ArrowContentLeft1DefaultValue = newObject.ArrowContentLeft1DefaultValue;
            ArrowContentLeft2DefaultValue = newObject.ArrowContentLeft2DefaultValue;
            ArrowContentLeft3DefaultValue = newObject.ArrowContentLeft3DefaultValue;
            ArrowContentRight1DefaultValue = newObject.ArrowContentRight1DefaultValue;
            ArrowContentRight2DefaultValue = newObject.ArrowContentRight2DefaultValue;
            ArrowContentRight3DefaultValue = newObject.ArrowContentRight3DefaultValue;
            EntryValue_Min = newObject.EntryValue_Min;
            EntryValue_Max = newObject.EntryValue_Max;
        }
    }
}
