using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("2749A840-968A-41E1-94D7-C43ECF6E21F0")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Configuration : IConfiguration
    {
        public string BasePath { get; set; }

        public Configuration(string path, string key = null)
        {
            BasePath = path + nameof(Configuration) + key + ".";
        }

        #region Children
        public string Self => BasePath + nameof(Self);
        public string MotionAxisControl => BasePath + nameof(MotionAxisControl); 
        public string DonorLiftingModule => BasePath + nameof(DonorLiftingModule); 
        public string ProcessTable => BasePath + nameof(ProcessTable);
        public string InspectionVision => BasePath + nameof(InspectionVision);
        #endregion

        #region Ids

        #endregion
    }
}
