using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("7E0A7FC3-B0DD-4CB8-8436-E7AFC325B913")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class LaserApplyDTO : BaseDTO, ILaserApplyDTO
    {
        public double AttenuatorPercentage { get; set; }

        public double PPDivider { get; set; }

        public double PulseDuration { get; set; }

        public PresetControl PresetControl { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<LaserApplyDTO>(json);

            PresetControl = newObject.PresetControl;
            AttenuatorPercentage = newObject.AttenuatorPercentage;
            PPDivider = newObject.PPDivider;
            PulseDuration = newObject.PulseDuration;
        }
    }
}
