using System.Runtime.InteropServices;

namespace CommonLib.Enums
{
    [ComVisible(true)]
    [Guid("D2FC95D4-71B4-4CF3-B9D8-F370AAFB92CB")]
    public enum LaserStatusRange
    {
        WithinRange = 0, //Green
        Warning = 1, //Yellow
        OutOfRange = 2, //Red
        Unknown = 3 //Gray
    }
}
