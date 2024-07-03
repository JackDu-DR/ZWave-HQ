using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("126D4578-7D90-473E-822B-3A1D9686C5FC")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class ProcessTable : IProcessTable
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public ProcessTable(string path, string key = null)
        {
            BasePath = path + nameof(ProcessTable) + key + ".";
        }

        #region Children
        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Content => BasePath + nameof(Content);
        public string Move => BasePath + nameof(Move);
        public string Action => BasePath + nameof(Action);
        public string CameraChange => BasePath + nameof(CameraChange);
        public string SubstrateChuckVacuumOutput => BasePath + nameof(SubstrateChuckVacuumOutput);
        public string ProcessTableDTO => BasePath + nameof(ProcessTableDTO);
        #endregion
    }
}

