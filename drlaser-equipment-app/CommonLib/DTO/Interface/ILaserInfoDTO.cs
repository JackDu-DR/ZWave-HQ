using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("03C88646-6342-454E-9F58-C261B12F955D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ILaserInfoDTO : IBaseDTO
    {
        bool IsConnected { get; set; }

        LaserOperation Operation { get; set; }

        bool Emission { get; set; }

        double LaserPower { get; set; }

        double Energy { get; set; }

        LaserStatusRange LaserPowerRange { get; set; }

        double Frequency { get; set; }

        double PulseDivider { get; set; }

        LaserStatusRange FrequencyRange { get; set; }

        int WaveLength { get; set; }

        LaserStatusRange WaveLengthRange { get; set; }

        new void LoadDataFromJson(string json);
    }
}
