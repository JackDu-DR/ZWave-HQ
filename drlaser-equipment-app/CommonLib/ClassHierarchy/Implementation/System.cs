using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("8B2CCF45-5C2D-4910-B52C-2C99A2D8759F")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class System : ISystem
    {
        public string BasePath { get; set; }
        public System(string path, string key = null)
        {
            BasePath = path + nameof(System) + key + ".";
        }

        #region Children
        public Laser Laser => new Laser(BasePath);
        public CausewaySystem CausewaySystem => new CausewaySystem(BasePath);
        public ProcessSystem ProcessSystem => new ProcessSystem(BasePath);
        public Motion Motion => new Motion(BasePath);
        public Vision Vision => new Vision(BasePath);
        #endregion

        #region Ids

        #endregion
    }
}
