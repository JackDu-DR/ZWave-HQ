using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("3BA95D9D-1189-4925-90BF-47BACF9CC454")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class InspectionVisionDTO : BaseDTO, IInspectionVisionDTO
    {
        public bool Connected { get; set; }
        public bool Acquisition { get; set; }
        public bool GrabImage { get; set; }
        public bool GammaEnable { get; set; }
        public double ExposureTime { get; set; }
        public double Gain { get; set; }
        public double Gamma { get; set; }
        public double BlackLevel {  get; set; }
        public double Sharpness { get; set; }
        public int ZoomValue { get; set; }
        public int ScrollVertical { get; set; }
        public int ScrollHorizontal { get; set; }
        public ProSystemUpLookInspecVisionUIElement ProSystemUpLookInspecVisionUIElement { get; set; }
        public CameraSelect CameraSelect { get; set; }
        public InspectionVisionPage InspectionVisionPage { get; set; }

        // NCC Start
        public bool NumLevelAutoNcc { get; set; }
        public bool AngStepAuto { get; set; }
        public double NumLevel { get; set; }
        public double AngleStart { get; set; }
        public double AngleExtent { get; set; }
        public double AngleStep { get; set; }
        public ModelSelect ModelSelect { get; set; }
        public MetricSelect MetricSelect { get; set; }
        // NCC End

        // Calibration
        public string CalibrationModelId { get; set; }
        public string CalibrationModelIdSelection { get; set; }
        public double StepSize { get; set; }
        public double StepCount { get; set; }
        public double TransactionX { get; set; }
        public double TransactionY { get; set; }
        public double Rotation { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double PixelX { get; set; }
        public double PixelY { get; set; }
        public double FovX { get; set; }
        public double FovY { get; set; }
        public ProSystemVCalibartionUIElement ProSystemVCalibartionUIElement { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<InspectionVisionDTO>(json);

            Connected = newObject.Connected;
            Acquisition = newObject.Acquisition;
            GrabImage = newObject.GrabImage;
            GammaEnable = newObject.GammaEnable;
            ExposureTime = newObject.ExposureTime;
            Gain = newObject.Gain;
            Gamma = newObject.Gamma;
            BlackLevel = newObject.BlackLevel;
            Sharpness = newObject.Sharpness;
            ZoomValue = newObject.ZoomValue;
            ScrollVertical = newObject.ScrollVertical;
            ScrollHorizontal = newObject.ScrollHorizontal;
            ProSystemUpLookInspecVisionUIElement = newObject.ProSystemUpLookInspecVisionUIElement;
            CameraSelect = newObject.CameraSelect;
            InspectionVisionPage = newObject.InspectionVisionPage;
            NumLevelAutoNcc = newObject.NumLevelAutoNcc;
            AngStepAuto = newObject.AngStepAuto;
            NumLevel = newObject.NumLevel;
            AngleStart = newObject.AngleStart;
            AngleExtent = newObject.AngleExtent;
            AngleStep = newObject.AngleStep;
            ModelSelect = newObject.ModelSelect;
            MetricSelect = newObject.MetricSelect;
            CalibrationModelId = newObject.CalibrationModelId;
            CalibrationModelIdSelection = newObject.CalibrationModelIdSelection;
            StepSize = newObject.StepSize;
            StepCount = newObject.StepCount;
            TransactionX = newObject.TransactionX;
            TransactionY = newObject.TransactionY;
            Rotation = newObject.Rotation;
            ScaleX = newObject.ScaleX;
            ScaleY = newObject.ScaleY;
            PixelX = newObject.PixelX;
            PixelY = newObject.PixelY;
            FovX = newObject.FovX;
            FovY = newObject.FovY;
            ProSystemVCalibartionUIElement = newObject.ProSystemVCalibartionUIElement;
        }
    }
}
