using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("C783BCF4-5333-40E7-9BA4-235D3FEC2E96")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionControl : IMotionControl
    {
        public string BasePath { get; set; }
        public MotionControl(string path, string key = null)
        {
            BasePath = path + nameof(MotionControl) + key + ".";
        }
        public string Profiles => BasePath + nameof(Profiles);
        public string Logs => BasePath + nameof(Logs);
        public string Calculate => BasePath + nameof(Calculate);
        public string Start => BasePath + nameof(Start);
        public string Stop => BasePath + nameof(Stop);
        public string Kinematics => BasePath + nameof(Kinematics);
        public string KinematicsRoute => BasePath + nameof(KinematicsRoute);
        public string TestState => BasePath + nameof(TestState);
    }
}
