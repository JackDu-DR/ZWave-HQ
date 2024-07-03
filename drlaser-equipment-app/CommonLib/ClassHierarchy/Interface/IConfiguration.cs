using CommonLib.ClassHierarchy.Implementation;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("1268DA2F-48D4-43D4-8944-E379A4917A73")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IConfiguration
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        string Self { get; }
        string MotionAxisControl { get; }
        string DonorLiftingModule { get; }
        string ProcessTable { get; }
        string InspectionVision { get; }

    }
}
