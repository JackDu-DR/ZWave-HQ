using CommonLib.ClassHierarchy.Implementation;
using System.Runtime.InteropServices;


namespace CommonLib.ClassHierarchy.Interface
{
    [Guid("0B984CE0-1129-45FC-BDA7-1A6FF4D87869")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ICausewaySystem
    {
        [ComVisible(false)]
        string BasePath { get; set; }

        DonorLiftingModule DonorLiftingModule { get; }

        string Info { get; }

        string Fetch { get; }
    }
}
