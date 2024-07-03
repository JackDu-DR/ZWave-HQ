using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using Newtonsoft.Json;
using ZWave.Enums;

namespace ZWave.Models
{
    public class InspectionVisionModel : BaseModel
    {
        private bool _isCameraSelected;
        public bool IsCameraSelected
        {
            get => _isCameraSelected;
            set => SetProperty(ref _isCameraSelected, value);
        }
        private bool _isCameraConnected;
        public bool IsCameraConnected
        {
            get => _isCameraConnected;
            set => SetProperty(ref _isCameraConnected, value);
        }
        private bool _isCameraLive;
        public bool IsCameraLive
        {
            get => _isCameraLive;
            set => SetProperty(ref _isCameraLive, value);
        }

        private bool _isTriggerBtnEnable;
        public bool IsTriggerBtnEnable
        {
            get => _isTriggerBtnEnable;
            set => SetProperty(ref _isTriggerBtnEnable, value);
        }

        private bool _connected;
        public bool Connected
        {
            get => _connected;
            set => SetProperty(ref _connected, value);
        }
        private bool _acquisition;
        public bool Acquisition
        {
            get => _acquisition;
            set => SetProperty(ref _acquisition, value);
        }
        private bool _grabImage;
        public bool GrabImage
        {
            get => _grabImage;
            set => SetProperty(ref _grabImage, value);
        }

        // Camera Tab Start
        private bool _gammaEnable;
        public bool GammaEnable
        {
            get => _gammaEnable;
            set => SetProperty(ref _gammaEnable, value);
        }

        private double _exposureTime;
        public double ExposureTime
        {
            get => _exposureTime;
            set => SetProperty(ref _exposureTime, value);
        }

        private double _gain;
        public double Gain
        {
            get => _gain;
            set => SetProperty(ref _gain, value);
        }

        private double _gamma;
        public double Gamma
        {
            get => _gamma;
            set => SetProperty(ref _gamma, value);
        }

        private double _blackLevel;
        public double BlackLevel
        {
            get => _blackLevel;
            set => SetProperty(ref _blackLevel, value);
        }

        private double _sharpness;
        public double Sharpness
        {
            get => _sharpness;
            set => SetProperty(ref _sharpness, value);
        }
        // Camera Tab End

        private int _zoomValue;
        public int ZoomValue
        {
            get => _zoomValue;
            set => SetProperty(ref _zoomValue, value);
        }

        private int _scrollVertical;
        public int ScrollVertical
        {
            get => _scrollVertical;
            set => SetProperty(ref _scrollVertical, value);
        }

        private int _scrollHorizontal;
        public int ScrollHorizontal
        {
            get => _scrollHorizontal;
            set => SetProperty(ref _scrollHorizontal, value);
        }

        // Teach NCC Tab Start
        private DrawShapeCategory _drawShapeType;
        public DrawShapeCategory DrawShapeType
        {
            get => _drawShapeType;
            set => SetProperty(ref _drawShapeType, value);
        }

        private bool _numLevelAutoNcc;
        public bool NumLevelAutoNcc
        {
            get => _numLevelAutoNcc;
            set => SetProperty(ref _numLevelAutoNcc, value);
        }

        private bool _angStepAuto;
        public bool AngStepAuto
        {
            get => _angStepAuto;
            set => SetProperty(ref _angStepAuto, value);
        }

        private double _numLevel;
        public double NumLevel
        {
            get => _numLevel;
            set => SetProperty(ref _numLevel, value);
        }

        private double _angleStart;
        public double AngleStart
        {
            get => _angleStart;
            set => SetProperty(ref _angleStart, value);
        }

        private double _angleExtent;
        public double AngleExtent
        {
            get => _angleExtent;
            set => SetProperty(ref _angleExtent, value);
        }

        private double _angleStep;
        public double AngleStep
        {
            get => _angleStep;
            set => SetProperty(ref _angleStep, value);
        }
        // Teach NCC Tab End

        // Calibration Tab Start
        private List<string> _calibrationModelIdSelection;
        public List<string> CalibrationModelIdSelection
        {
            get => _calibrationModelIdSelection;
            set => SetProperty(ref _calibrationModelIdSelection, value);
        }

        private string _calibrationModelId;
        public string CalibrationModelId
        {
            get => _calibrationModelId;
            set => SetProperty(ref _calibrationModelId, value);
        }
        private double _stepSize;
        public double StepSize
        {
            get => _stepSize;
            set => SetProperty(ref _stepSize, value);
        }
        private double _stepCount;
        public double StepCount
        {
            get => _stepCount;
            set => SetProperty(ref _stepCount, value);
        }
        private double _transactionX;
        public double TransactionX
        {
            get => _transactionX;
            set => SetProperty(ref _transactionX, value);
        }
        private double _transactionY;
        public double TransactionY
        {
            get => _transactionY;
            set => SetProperty(ref _transactionY, value);
        }
        private double _rotation;
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _scaleX;
        public double ScaleX
        {
            get => _scaleX;
            set => SetProperty(ref _scaleX, value);
        }
        private double _scaleY;
        public double ScaleY
        {
            get => _scaleY;
            set => SetProperty(ref _scaleY, value);
        }
        private double _fovX;
        public double FovX
        {
            get => _fovX;
            set => SetProperty(ref _fovX, value);
        }
        private double _fovY;
        public double FovY
        {
            get => _fovY;
            set => SetProperty(ref _fovY, value);
        }
        // Calibration Tab End

        private ProSystemUpLookInspecVisionUIElement _proSystemUpLookInspecVisionUIElement;
        public ProSystemUpLookInspecVisionUIElement ProSystemUpLookInspecVisionUIElement
        {
            get => _proSystemUpLookInspecVisionUIElement;
            set => SetProperty(ref _proSystemUpLookInspecVisionUIElement, value);
        }

        private CameraSelect _cameraSelect;
        public CameraSelect CameraSelect
        {
            get => _cameraSelect;
            set
            {
                if (value != null)
                    IsCameraSelected = true;
                SetProperty(ref _cameraSelect, value);
            }

        }

        private InspectionVisionPage _inspectionVisionPage;
        public InspectionVisionPage InspectionVisionPage
        {
            get => _inspectionVisionPage;
            set => SetProperty(ref _inspectionVisionPage, value);
        }

        // Teach NCC Tab Start
        private ModelSelect _modelSelection;
        public ModelSelect ModelSelection
        {
            get => _modelSelection;
            set => SetProperty(ref _modelSelection, value);
        }
        private MetricSelect _metricSelection;
        public MetricSelect MetricSelection
        {
            get => _metricSelection;
            set => SetProperty(ref _metricSelection, value);
        }
        private string _shapeSelection;
        public string ShapeSelection
        {
            get => _shapeSelection;
            set => SetProperty(ref _shapeSelection, value);
        }
        // Teach Ncc Tab End
        private ProSystemVCalibartionUIElement _proSystemVCalibartionUIElement;
        public ProSystemVCalibartionUIElement ProSystemVCalibartionUIElement
        {
            get => _proSystemVCalibartionUIElement;
            set => SetProperty(ref _proSystemVCalibartionUIElement, value);
        }

        public IEnumerable<InspectionVisionConfigurationModel> InspectionVisionConfigurationData { get; set; }

        private InspectionVisionConfigurationModel _selectedCamera;
        public InspectionVisionConfigurationModel SelectedCamera
        {
            get => _selectedCamera;
            set => SetProperty(ref _selectedCamera, value);
        }

        public InspectionVisionModel()
        {
            InspectionVisionConfigurationData = new List<InspectionVisionConfigurationModel>();
            SelectedCamera = new InspectionVisionConfigurationModel();
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var inspectionVisionDTO = JsonConvert.DeserializeObject<InspectionVisionDTO>(baseDTO.ToString());

            Connected = inspectionVisionDTO.Connected;
            Acquisition = inspectionVisionDTO.Acquisition;
            GrabImage = inspectionVisionDTO.GrabImage;
            GammaEnable = inspectionVisionDTO.GammaEnable;
            ExposureTime = inspectionVisionDTO.ExposureTime;
            Gain = inspectionVisionDTO.Gain;
            Gamma = inspectionVisionDTO.Gamma;
            BlackLevel = inspectionVisionDTO.BlackLevel;
            Sharpness = inspectionVisionDTO.Sharpness;
            ZoomValue = inspectionVisionDTO.ZoomValue;
            ScrollVertical = inspectionVisionDTO.ScrollVertical;
            ScrollHorizontal = inspectionVisionDTO.ScrollHorizontal;
        }

    }
}
