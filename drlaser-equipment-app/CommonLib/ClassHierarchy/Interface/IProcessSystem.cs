using CommonLib.ClassHierarchy.Implementation;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("90752DE1-A242-44ED-BAD7-CD52D8F6EC22")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IProcessSystem
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        ProcessTable ProcessTable { get; }
        InspectionVision InspectionVision { get; }
        string Info { get; }

        string Fetch { get; }
    }
}
