using System.Runtime.InteropServices;

namespace CommonLib.Enums
{
    [ComVisible(true)]
    [Guid("37D8595D-E7CA-4398-A6C6-81BC0B59EB07")]
    public enum MotionCmd
    {
        move_rel = 7,
        move_abs = 6,
        home = 5,
        reset = 4,
        eStop = 3,
        stop = 2,
        enable = 1
    }
}
