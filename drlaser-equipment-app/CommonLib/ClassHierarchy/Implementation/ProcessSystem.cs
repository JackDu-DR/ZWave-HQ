using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("70DD3ECF-AE65-4B71-B012-07BD1AB9428F")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class ProcessSystem : IProcessSystem
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public ProcessSystem(string path, string key = null)
        {
            BasePath = path + nameof(ProcessSystem) + key + ".";
        }

        #region Children

        public ProcessTable ProcessTable => new ProcessTable(BasePath);
        public InspectionVision InspectionVision => new InspectionVision(BasePath);

        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Info => BasePath + nameof(Info);
        #endregion
    }
}
