using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("3C95F053-0E54-4FCD-B42F-EE25A954A168")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Camera : ICamera
    {
        public string BasePath { get; set; }

        public Camera(string path, string key = null)
        {
            BasePath = path + nameof(Camera) + key + ".";
        }

        public string Exposure => BasePath + nameof(Exposure);

        public string Light => BasePath + nameof(Light);
    }
}
