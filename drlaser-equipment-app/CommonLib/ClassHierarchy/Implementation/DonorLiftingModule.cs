using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("33990010-44B4-41A9-AD9B-3516B0F856C8")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class DonorLiftingModule : IDonorLiftingModule
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public DonorLiftingModule(string path, string key = null)
        {
            BasePath = path + nameof(DonorLiftingModule) + key + ".";
        }

        #region Children
        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Content => BasePath + nameof(Content);
        public string Move => BasePath + nameof(Move);
        public string Action => BasePath + nameof(Action);
        public string CameraChange => BasePath + nameof(CameraChange);
        public string DonorChuckVacuumOutput => BasePath + nameof(DonorChuckVacuumOutput);
        public string DonorLiftingModuleDTO => BasePath + nameof(DonorLiftingModuleDTO);
        #endregion
    }
}
