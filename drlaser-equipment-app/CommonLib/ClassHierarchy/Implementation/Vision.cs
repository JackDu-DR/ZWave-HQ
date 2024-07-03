using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("6C19B496-2E0F-45E8-9F9B-C42FB1C40F2C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Vision : IVision
    {
        public string BasePath { get; set; }

        public Vision(string path, string key = null)
        {
            BasePath = path + nameof(Vision) + key + ".";
        }

        #region Children
        public Camera Camera => new Camera(BasePath);
        #endregion

        #region Ids

        public string CameraParams => BasePath + nameof(CameraParams);

        public string Connect => BasePath + nameof(Connect);

        public string Trigger => BasePath + nameof(Trigger);
        public string Exposure => BasePath + nameof(Exposure);
        public string Light => BasePath + nameof(Light);

        public string ImageDTO => BasePath + nameof(ImageDTO);

        public string ROIDTO => BasePath + nameof(ROIDTO);
        public string CircleROIResultId => BasePath + nameof(CircleROIResultId);
        public string RectangleROIResultId => BasePath + nameof(RectangleROIResultId);
        public string EllipseROIResultId => BasePath + nameof(EllipseROIResultId);
        public string SaveResultCommandId => BasePath + nameof(SaveResultCommandId);

        public string MotionMovedId => BasePath + nameof(MotionMovedId);
        #endregion
    }
}
