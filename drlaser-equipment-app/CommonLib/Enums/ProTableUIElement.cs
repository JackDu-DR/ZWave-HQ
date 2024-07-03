using System.Runtime.InteropServices;

namespace CommonLib.Enums
{
    [ComVisible(true)]
    [Guid("F98FA989-9C39-4054-BF65-1656B21869B6")]
    public enum ProTableUIElement
    {
        Pro_Table_Camera_Change = 12,
        Pro_Table_Home_All = 11,
        Pro_Table_Unloading = 10,
        Pro_Table_Loading = 9,
        Pro_Table_Vacuum_Chuck_Off = 8,
        Pro_Table_Vacuum_Chuck_On = 7,
        Pro_Table_Axis_X = 6,
        Pro_Table_Axis_Y = 5,
        Pro_Table_Tip_Tilt_TX = 4,
        Pro_Table_Tip_Tilt_TY = 3,
        Pro_Table_Tip_Tilt_Z = 2,
        Pro_Table_Tip_Tilt_T = 1
    }
}
