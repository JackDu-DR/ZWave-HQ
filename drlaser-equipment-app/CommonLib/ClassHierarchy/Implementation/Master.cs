using CommonLib.ClassHierarchy.Interface;
using CommonLib.DTO.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("3E890B5D-71A4-4C3B-A56B-42CBDDA3CD7F")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Master : IMaster
    {
        [ComVisible(false)]
        public string BasePath => nameof(Master) + ".";
        public string Message => BasePath + nameof(Message);
        #region Children
        public System System => new System(BasePath);
        public Configuration Configuration => new Configuration(BasePath);
        public Alarm Alarm => new Alarm(BasePath);
        public CriticalControl CriticalControl => new CriticalControl(BasePath);
        #endregion

        #region Ids
        public string Content => BasePath + nameof(Content);
        #endregion
    }
}
