using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("94B708E2-40DC-45F3-A940-194BCED2D051")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class LaserBasic : ILaserBasic
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public LaserBasic(string path, string key = null)
        {
            BasePath = path + nameof(LaserBasic) + key + ".";
        }

        #region Children
        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Apply => BasePath + nameof(Apply);
        public string Connect => BasePath + nameof(Connect);
        public string StandBy => BasePath + nameof(StandBy);
        public string Content => BasePath + nameof(Content);
        public string Output => BasePath + nameof(Output);
        public string LaserBasicDTO => BasePath + nameof(LaserBasicDTO);
        #endregion
    }
}
