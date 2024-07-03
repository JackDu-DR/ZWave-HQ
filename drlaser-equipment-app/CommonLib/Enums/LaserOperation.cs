using System.Runtime.InteropServices;

namespace CommonLib.Enums
{
    [ComVisible(true)]
    [Guid("52DCA470-E6EB-4773-A975-D5E6F1C19B22")]
    public enum LaserOperation
    {
        Operation = 0,
        Standby = 1,
        Housekeeping = 2
    }
}
