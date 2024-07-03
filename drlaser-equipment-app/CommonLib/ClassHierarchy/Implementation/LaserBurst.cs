using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("DF0B7385-14B9-4903-9FEC-96829FEC892C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class LaserBurst : ILaserBurst
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public LaserBurst(string path, string key = null)
        {
            BasePath = path + nameof(LaserBurst) + key + ".";
        }

        #region Children
        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Apply => BasePath + nameof(Apply);
        public string Content => BasePath + nameof(Content);
        public string Powerlock => BasePath + nameof(Powerlock);

        public string LaserBurstDTO => BasePath + nameof(LaserBurstDTO);
        #endregion
    }
}
