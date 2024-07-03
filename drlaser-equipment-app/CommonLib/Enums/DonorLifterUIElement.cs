using System.Runtime.InteropServices;

namespace CommonLib.Enums
{
    [ComVisible(true)]
    [Guid("E69E72A4-DC97-42E2-AE66-A6A256D1C009")]
    public enum DonorLifterUIElement
    {
        Donor_Camera_Change = 9,
        Donor_Home_All = 8,
        Donor_Unloading = 7,
        Donor_Loading = 6,
        Donor_Vacuum_Chuck_Off = 5,
        Donor_Vacuum_Chuck_On = 4,
        Donor_Axis_Z = 3,
        Donor_Axis_Y = 2,
        Donor_Axis_X = 1
    }
}
