using CommonLib.ClassHierarchy.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.ClassHierarchy.Implementation
{
    [Guid("785E1556-1E00-4C43-B7F9-27508F84643A")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class InspectionVision : IInspectionVision
    {
        [ComVisible(false)]
        public string BasePath { get; set; }

        public InspectionVision(string path, string key = null)
        {
            BasePath = path + nameof(InspectionVision) + key + ".";
        }

        #region Children
        #endregion

        #region Ids
        public string Fetch => BasePath + nameof(Fetch);
        public string Content => BasePath + nameof(Content);
        public string Connect => BasePath + nameof(Connect);
        public string Live => BasePath + nameof(Live);
        public string Trigger => BasePath + nameof(Trigger);
        public string Save => BasePath + nameof(Save);
        public string ExposureTime => BasePath + nameof(ExposureTime);
        public string Gain => BasePath + nameof(Gain);
        public string Gamma => BasePath + nameof(Gamma);
        public string GammaEnable => BasePath + nameof(GammaEnable);
        public string BlackLevel => BasePath + nameof(BlackLevel);
        public string Sharpness => BasePath + nameof(Sharpness);
        public string ZoomValue => BasePath + nameof(ZoomValue);
        public string ScrollVertical => BasePath + nameof(ScrollVertical);
        public string ScrollHorizontal => BasePath + nameof(ScrollHorizontal);
        public string InspectionVisionDTO => BasePath + nameof(InspectionVisionDTO);

        public string ImageDTO => BasePath + nameof(ImageDTO);
        public string ROIDTO => BasePath + nameof(ROIDTO);
        public string RegionDTO => BasePath + nameof(RegionDTO);
        public string CircleROIResultId => BasePath + nameof(CircleROIResultId);
        public string RectangleROIResultId => BasePath + nameof(RectangleROIResultId);
        public string EllipseROIResultId => BasePath + nameof(EllipseROIResultId);
        public string SaveResultCommandId => BasePath + nameof(SaveResultCommandId);
        public string MotionMovedId => BasePath + nameof(MotionMovedId);

        // NCC Start
        public string Teach => BasePath + nameof(Teach);
        public string NumLevelAutoNcc => BasePath + nameof(NumLevelAutoNcc);
        public string AngStepAuto => BasePath + nameof(AngStepAuto);
        public string NumLevel => BasePath + nameof(NumLevel);
        public string AngleStart => BasePath + nameof(AngleStart);
        public string AngleExtent => BasePath + nameof(AngleExtent);
        public string AngleStep => BasePath + nameof(AngleStep);
        // NCC End

        // Calibration Start
        public string CalibrationAction => BasePath + nameof(CalibrationAction);
        public string CalibrationResult => BasePath + nameof(CalibrationResult);
        public string CalibrationModelId => BasePath + nameof(CalibrationModelId);
        public string CalibrationModelIdSelection => BasePath + nameof(CalibrationModelIdSelection);
        public string StepSize => BasePath + nameof(StepSize);
        public string StepCount => BasePath + nameof(StepCount);
        public string TransactionX => BasePath + nameof(AngleStart);
        public string TransactionY => BasePath + nameof(TransactionY);
        public string Rotation => BasePath + nameof(Rotation);
        public string ScaleX => BasePath + nameof(ScaleX);
        public string ScaleY => BasePath + nameof(ScaleY);
        public string PixelX => BasePath + nameof(PixelX);
        public string PixelY => BasePath + nameof(PixelY);
        public string FovX => BasePath + nameof(FovX);
        public string FovY => BasePath + nameof(FovY);
        // Calibration End
        #endregion
    }
}

