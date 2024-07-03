using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("D97D5469-4360-4E6E-AC76-B8E062BB9F8A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IMotionControl
    {
        [ComVisible(false)]
        string BasePath { get; set; }
        string Profiles { get; }
        string Logs { get; }
        string Calculate {  get; }
        string Start { get; }
        string Stop { get; }
        string Kinematics { get; }
        string KinematicsRoute { get; }
        string TestState { get; }
    }
}
