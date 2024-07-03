using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("560AB0AA-0F32-41BC-98F2-ECA07547BDD9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Laser : ILaser
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public Laser(string path, string key = null)
        {
            BasePath = path + nameof(Laser) + key + ".";
        }

        #region Children

        public LaserBurst LaserBurst => new LaserBurst(BasePath);

        public LaserBasic LaserBasic => new LaserBasic(BasePath);

        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Info => BasePath + nameof(Info);
        #endregion
    }
}
