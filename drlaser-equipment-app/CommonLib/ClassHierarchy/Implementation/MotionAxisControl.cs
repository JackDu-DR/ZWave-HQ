using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("E7D89A6F-7EBF-4BB4-9823-C54B3D027693")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionAxisControl : IMotionAxisControl
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public MotionAxisControl(string path, string key = null)
        {
            BasePath = path + nameof(MotionAxisControl) + key + ".";
        }

        #region Children
        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Content => BasePath + nameof(Content);
        public string Action => BasePath + nameof(Action);
        public string ChangeAxis => BasePath + nameof(ChangeAxis);
        public string MotionAxisControlDTO => BasePath + nameof(MotionAxisControlDTO);
        public string MotionParamUpdates => BasePath + nameof(MotionParamUpdates);
        public string ExecutingBtnStatus => BasePath + nameof(ExecutingBtnStatus);
        public string EnableBtnStatus => BasePath + nameof(EnableBtnStatus);
        #endregion
    }
}
