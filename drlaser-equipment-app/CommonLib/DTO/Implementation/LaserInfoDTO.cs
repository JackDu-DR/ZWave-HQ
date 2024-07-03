using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("21E54B04-641A-42C7-8091-03023CEBE502")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class LaserInfoDTO : ILaserInfoDTO
    {
        public bool IsConnected { get ; set ; }
        public LaserOperation Operation { get ; set ; }
        public bool Emission { get; set; }
        public double LaserPower { get; set; }
        public double Energy { get; set; }
        public LaserStatusRange LaserPowerRange { get; set; }
        public double Frequency { get; set; }
        public double PulseDivider { get; set; }
        public LaserStatusRange FrequencyRange { get; set; }
        public int WaveLength { get; set; }
        public LaserStatusRange WaveLengthRange { get; set; }

        public void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<LaserInfoDTO>(json);

            IsConnected = newObject.IsConnected;
            Operation = newObject.Operation;
            Emission = newObject.Emission;
            LaserPower = newObject.LaserPower;
            Energy = newObject.Energy;
            LaserPowerRange = newObject.LaserPowerRange;
            Frequency = newObject.Frequency;
            PulseDivider = newObject.PulseDivider;
            FrequencyRange = newObject.FrequencyRange;
            WaveLength = newObject.WaveLength;
            WaveLengthRange = newObject.WaveLengthRange;
        }
    }
}
