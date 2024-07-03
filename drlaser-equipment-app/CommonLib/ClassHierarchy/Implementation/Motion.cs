using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("DDA897ED-D702-4263-AC2B-361465C11E81")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Motion : IMotion
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public Motion(string path, string key = null)
        {
            BasePath = path + nameof(Motion) + key + ".";
        }

        #region Children
        public MotionControl MotionControl => new MotionControl(BasePath);

        public MotionAxisControl MotionAxisControl => new MotionAxisControl(BasePath);


        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Info => BasePath + nameof(Info);
        #endregion
    }
}
