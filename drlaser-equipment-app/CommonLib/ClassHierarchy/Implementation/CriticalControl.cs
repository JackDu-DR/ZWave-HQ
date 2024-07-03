using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("10B5FE40-379E-4963-AB71-25C6CB603435")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CriticalControl : ICriticalControl
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public CriticalControl(string path, string key = null)
        {
            BasePath = path + nameof(CriticalControl) + key + ".";
        }

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Update => BasePath + nameof(Update);
        #endregion
    }
}