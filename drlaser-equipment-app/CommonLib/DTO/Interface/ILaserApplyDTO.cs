using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("9EFC7C56-E4C7-4788-BE4E-1D6C401EBFCC")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaserApplyDTO : IBaseDTO
    {
        double AttenuatorPercentage { get; set; }

        double PPDivider { get; set; }

        PresetControl PresetControl { get; set; }

        double PulseDuration { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }

    [Guid("549B5E28-BC6F-4C25-9A3E-8C0627896971")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IVisionParamsDTO : IBaseDTO
    {
        double AttenuatorPercentage { get; set; }

        double PPDivider { get; set; }

        PresetControl PresetControl { get; set; }

        double PulseDuration { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }

    [Guid("5DF65319-101F-48A8-B52D-E809CEB82D8B")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class VisionParamsDTO : BaseDTO, IVisionParamsDTO
    {
        public double AttenuatorPercentage { get; set; }

        public double PPDivider { get; set; }

        public double PulseDuration { get; set; }

        public PresetControl PresetControl { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<VisionParamsDTO>(json);

            PresetControl = newObject.PresetControl;
            AttenuatorPercentage = newObject.AttenuatorPercentage;
            PPDivider = newObject.PPDivider;
            PulseDuration = newObject.PulseDuration;
        }
    }
}
