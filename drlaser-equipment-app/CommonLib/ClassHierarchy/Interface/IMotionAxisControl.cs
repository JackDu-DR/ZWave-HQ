using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("A293BDEA-D4A1-4002-A0A6-D64C748BFB3A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IMotionAxisControl
    {
        [ComVisible(false)]
        string BasePath { get; set; }
        
        string Fetch { get; }
        string Content { get; }
        string ChangeAxis { get; }

        string Action { get; }

        string MotionAxisControlDTO { get; }
        string MotionParamUpdates { get; }
        string ExecutingBtnStatus { get; }
        string EnableBtnStatus { get; }
    }
}
