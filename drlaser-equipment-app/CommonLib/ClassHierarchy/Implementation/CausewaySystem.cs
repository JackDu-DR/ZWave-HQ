using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("C210FD04-4537-4BE9-8975-267C9009CD2D")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CausewaySystem : ICausewaySystem
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public CausewaySystem(string path, string key = null)
        {
            BasePath = path + nameof(CausewaySystem) + key + ".";
        }

        #region Children

        public DonorLiftingModule DonorLiftingModule => new DonorLiftingModule(BasePath);


        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Info => BasePath + nameof(Info);
        #endregion
    }
}
