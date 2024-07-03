using System.Runtime.InteropServices;

namespace CommonLib.Enums
{
    [ComVisible(true)]
    [Guid("FC0D1895-AE03-446F-ACD3-9A7D2BE0972D")]
    public enum InspectionVisionPage
    {
        CalibrationPage = 5,
        ResultPage = 4,
        TeachPage = 3,
        LightingPage = 2,
        CameraPage = 1,
    }
}
