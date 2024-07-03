using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("2486000E-A3AB-4800-8977-3000E9CFB615")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class LaserBurstApplyDTO : BaseDTO, ILaserBurstApplyDTO
    {
        public double P { get; set; }

        public double N { get; set; }

        public double EnvelopeControlP { get; set; }

        public double EnvelopeControlN { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<LaserBurstApplyDTO>(json);

            P = newObject.P;
            N = newObject.N;
            EnvelopeControlP = newObject.EnvelopeControlP;
            EnvelopeControlN = newObject.EnvelopeControlN;
        }
    }
}
