using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using ZWave.Controls;
using ZWave.Enums;
using ZWave.Helpers;
using ZWave.Models;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Systems.ProcessSystem
{
    public partial class InspectionVisionPageViewModel : BaseViewModel
    {
        private static readonly int DELAY_CODE_EXCUTION_TIME = 100; //Milliseconds;
        private string FetchDataId => Master.System.ProcessSystem.InspectionVision.Fetch;
        private string ImageDtoId => Master.System.ProcessSystem.InspectionVision.ImageDTO;
        private string ROIDtoId => Master.System.ProcessSystem.InspectionVision.ROIDTO;
        private string RegionDtoId => Master.System.ProcessSystem.InspectionVision.RegionDTO;
        private string CircleROIResultId => Master.System.ProcessSystem.InspectionVision.CircleROIResultId;
        private string RectangleROIResultId => Master.System.ProcessSystem.InspectionVision.RectangleROIResultId;
        private string EllipseROIResultId => Master.System.ProcessSystem.InspectionVision.EllipseROIResultId;
        private string SaveResultCommandId => Master.System.ProcessSystem.InspectionVision.SaveResultCommandId;

        //private string VisionRoutingKey => AppMessageHandler.StreamVisionRoutingKey;
        //private string VisionRoutingKey2 => AppMessageHandler.StreamVisionRoutingKey2;

        //private readonly ConcurrentDictionary<int, byte[]> _visionCallback = new ConcurrentDictionary<int, byte[]>();
        //private readonly ConcurrentDictionary<int, ImageStreamDTO> _visionCallback = new ConcurrentDictionary<int, ImageStreamDTO>();

        protected string Topic => Master.System.ProcessSystem.InspectionVision.BasePath + "#";
        private string FetchCameraParamsId => Master.System.ProcessSystem.InspectionVision.Fetch;

        private string MotionMovedId => Master.System.ProcessSystem.InspectionVision.MotionMovedId;
        private string ApplyContentId => Master.System.ProcessSystem.InspectionVision.Content;
        private string ConnectId => Master.System.ProcessSystem.InspectionVision.Connect;
        private string LiveId => Master.System.ProcessSystem.InspectionVision.Live;
        private string TriggerId => Master.System.ProcessSystem.InspectionVision.Trigger;
        private string SaveId => Master.System.ProcessSystem.InspectionVision.Save;
        private string ExposureTimeId => Master.System.ProcessSystem.InspectionVision.ExposureTime;
        private string GainId => Master.System.ProcessSystem.InspectionVision.Gain;
        private string GammaId => Master.System.ProcessSystem.InspectionVision.Gamma;
        private string GammaEnableId => Master.System.ProcessSystem.InspectionVision.GammaEnable;
        private string BlackLevelId => Master.System.ProcessSystem.InspectionVision.BlackLevel;
        private string SharpnessId => Master.System.ProcessSystem.InspectionVision.Sharpness;
        private string ZoomValueId => Master.System.ProcessSystem.InspectionVision.ZoomValue;
        private string ScrollVerticalId => Master.System.ProcessSystem.InspectionVision.ScrollVertical;
        private string ScrollHorizontalId => Master.System.ProcessSystem.InspectionVision.ScrollHorizontal;

        private string TeachId => Master.System.ProcessSystem.InspectionVision.Teach;
        private string CalibrationActionId => Master.System.ProcessSystem.InspectionVision.CalibrationAction;
        private string CalibrationResultId => Master.System.ProcessSystem.InspectionVision.CalibrationResult;
        private string CalibrationModelIdSelectionId => Master.System.ProcessSystem.InspectionVision.CalibrationModelIdSelection;

        private object _lockThrd = new object();

        [ObservableProperty]
        InspectionVisionModel _inspectionVisionModel;
        [ObservableProperty]
        private IList<InspectionVisionConfigurationModel> _inspectionVisionConfigurationModel;

        [ObservableProperty]
        private InspectionVisionConfigurationModel _selectedCamera;

        [ObservableProperty]
        List<string> _shapeSource;
        [ObservableProperty]
        List<string> _modelSelectionSource;
        [ObservableProperty]
        List<string> _metricSelectionSource;

        [ObservableProperty]
        private bool _isCameraPageBtnEnabled = true;
        [ObservableProperty]
        private bool _isTeachPageBtnEnabled = false;
        [ObservableProperty]
        private bool _isCalibrationPageBtnEnabled = false;

        [ObservableProperty]
        private string _selectedShape;
        [ObservableProperty]
        private string _selectedTeachModel;
        [ObservableProperty]
        private string _selectedMetric;

        [ObservableProperty]
        private bool _isDisplayTeachPageNccModel = false;
        [ObservableProperty]
        private bool _isDisplayTeachPageShapeBasedModel = false;


        private byte[] _imageToAppliedROI;
        private bool _generalQueueSubcribed;

        [ObservableProperty]
        private View _childView;

        [ObservableProperty]
        private bool _isDrawingEnabled;

        [ObservableProperty]
        private int _ratio = 1;

        [ObservableProperty]
        private double _scrollX;

        [ObservableProperty]
        private double _scrollY;

        [ObservableProperty]
        private double _fullImageWidth;

        [ObservableProperty]
        private double _fullImageHeight;

        [ObservableProperty]
        private DateTime _imageTimestamp;

        [ObservableProperty]
        private CameraModel _cameraModel;

        [ObservableProperty]
        private bool _hasROIResults;

        [ObservableProperty]
        private bool _clearShapes;

        [ObservableProperty]
        private MotionMode _motionMode;

        [RelayCommand]
        void Rotating(float phi)
        {
            OnPhiChanged(phi);
        }

        private void OnPhiChanged(float value)
        {
            if (TeachingROI != null)
            {
                new Task(async () =>
                {
                    await Task.Delay(100);
                }).Start();

                if (TeachingROI is Rectangle rectangle)
                {
                    var newRect = new Rectangle
                    {
                        StartX = rectangle.StartX,
                        StartY = rectangle.StartY,
                        Width = rectangle.Width,
                        Height = rectangle.Height,
                        Phi = NormalizePhi(value),
                    };
                    TeachingROI = newRect;
                }
                else if (TeachingROI is Ellipse ellipse)
                {
                    var newEllipse = new Ellipse
                    {
                        CenterX = ellipse.CenterX,
                        CenterY = ellipse.CenterY,
                        RadiusX = ellipse.RadiusX,
                        RadiusY = ellipse.RadiusY,
                        Phi = NormalizePhi(value)
                    };
                    TeachingROI = newEllipse;
                }
            }
        }

        [ObservableProperty]
        private ShapeType _shapeType = ShapeType.Circle;
        partial void OnShapeTypeChanged(ShapeType value)
        {
            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Selected - {Enum.GetName(typeof(ShapeType), value)} Shape");
            DiscardROI();
        }

        [ObservableProperty]
        private IShapeROI _teachingROI;
        partial void OnTeachingROIChanged(IShapeROI value)
        {
            IsApplyButtonEnabled = value != null;
            IsDiscardButtonEnabled = value != null;
        }

        private IEnumerable<IShapeROI> _resultROIs;
        public IEnumerable<IShapeROI> ResultROIs
        {
            get => _resultROIs;
            set
            {
                SetProperty(ref _resultROIs, value);

                new Task(async () =>
                {
                    await DelayUIUpdate();
                    HasROIResults = ResultROIs != null;
                }).Start();
            }
        }

        [ObservableProperty]
        private Point _mousePosition;

        [ObservableProperty]
        private bool _isTeaching;

        [ObservableProperty]
        private bool _isApplyButtonEnabled;

        [ObservableProperty]
        private bool _isDiscardButtonEnabled;

        //private Thread _thread;
        //private bool _dipose;

        [ObservableProperty]
        private bool _loadCompleted = false;

        [ObservableProperty]
        private bool _isCameraChanging;

        public InspectionVisionPageViewModel()
        {
            InspectionVisionModel = new InspectionVisionModel();
            CameraModel = new CameraModel();

            LoadConfigurationData();

            _shapeType = ShapeType.Circle;
            _motionMode = MotionMode.Teaching;
            _imageToAppliedROI = [];

            IsApplyButtonEnabled = false;
            IsDiscardButtonEnabled = false;

            //_dipose = false;
            //_thread = new Thread(ThreadLoop);
            //_thread.Start();

            PropertyChanged += OnPropertyChanged;

            var shapeTypeList = new List<string>() { };
            var inspectionVisionShapeTypeValues = Enum.GetValues(typeof(ShapeType));

            foreach (var value in inspectionVisionShapeTypeValues)
            {
                shapeTypeList.Add(value.ToString());
            }
            _shapeSource = shapeTypeList;
            InspectionVisionModel.ShapeSelection = shapeTypeList[0];
            _selectedShape = shapeTypeList[0].ToString();
            _shapeType = (ShapeType)Enum.Parse(typeof(ShapeType), shapeTypeList[0]);

            var modelSelectionList = new List<string>() { };
            var inspectionVisionModelSelectionValues = Enum.GetValues(typeof(ModelSelect));

            foreach (var value in inspectionVisionModelSelectionValues)
            {
                modelSelectionList.Add(value.ToString());
            }
            _modelSelectionSource = modelSelectionList;
            InspectionVisionModel.ModelSelection = (ModelSelect)Enum.Parse(typeof(ModelSelect), modelSelectionList[0].ToString());
            _selectedTeachModel = modelSelectionList[0].ToString();
            if (InspectionVisionModel.ModelSelection == ModelSelect.NCCModel)
            {
                IsDisplayTeachPageNccModel = true;
                IsDisplayTeachPageShapeBasedModel = false;
            }
            else if (InspectionVisionModel.ModelSelection == ModelSelect.ShapeBasedModel)
            {
                IsDisplayTeachPageNccModel = false;
                IsDisplayTeachPageShapeBasedModel = true;
            }

            var metricSelectionList = new List<string>() { };
            var inspectionVisionMetricSelectionValues = Enum.GetValues(typeof(MetricSelect));

            foreach (var value in inspectionVisionMetricSelectionValues)
            {
                metricSelectionList.Add(value.ToString());
            }
            _metricSelectionSource = metricSelectionList;
            InspectionVisionModel.MetricSelection = (MetricSelect)Enum.Parse(typeof(MetricSelect), metricSelectionList[0].ToString());
            _selectedMetric = metricSelectionList[0].ToString();
        }

        private async void LoadConfigurationData()
        {
            await ConfigurationService.GetConfig_InspectionVision();
            InspectionVisionConfigurationModel = ConfigurationService.InspectionVisionConfigurationModels.ToList();
            LoadCompleted = true;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedCamera))
            {
                InspectionVisionModel.SelectedCamera = SelectedCamera;
            }
            if (e.PropertyName == nameof(SelectedShape))
            {
                InspectionVisionModel.ShapeSelection = SelectedShape;
            }
            if (e.PropertyName == nameof(SelectedTeachModel))
            {
                InspectionVisionModel.ModelSelection = (ModelSelect)Enum.Parse(typeof(ModelSelect), SelectedTeachModel);
                if (InspectionVisionModel.ModelSelection == ModelSelect.NCCModel)
                {
                    IsDisplayTeachPageNccModel = true;
                    IsDisplayTeachPageShapeBasedModel = false;
                }
                else if (InspectionVisionModel.ModelSelection == ModelSelect.ShapeBasedModel)
                {
                    IsDisplayTeachPageNccModel = false;
                    IsDisplayTeachPageShapeBasedModel = true;
                }
            }
        }

        //int _x = 0;
        //private void ThreadLoop()
        //{
        //    while (!_dipose)
        //    {
        //        if (_visionCallback.Count() > 0)
        //        {
        //            //byte[] bytesArray;
        //            ImageStreamDTO _ImageStreamDTO;
        //            bool result = _visionCallback.TryRemove(_x, out _ImageStreamDTO);
        //            if (result)
        //            {
        //                _x++;
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "_visionCallback - Image deque : " + (_x-1).ToString());
        //                //bDisplay = true;
        //                CameraModel.ImageBytes = _ImageStreamDTO.StreamData;
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - ThreadLoop - ImageID: " + _ImageStreamDTO.ImageID);
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - ThreadLoop - timeStamp: " + _ImageStreamDTO.Timestamp);
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - ThreadLoop - SetupLiveCamera: Start  " + _ImageStreamDTO.Timestamp);
        //                SetupLiveCamera();
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - ThreadLoop - SetupLiveCamera: Done  " + _ImageStreamDTO.Timestamp);

        //            }
        //            Thread.Sleep(1);
        //        }
        //        else
        //            Thread.Sleep(2);
        //    }

        //}

        int eventSub = 0;
        [RelayCommand]
        void Loaded()
        {
            //_n = 0;
            //_x = 0;
            //LogService.LogUser(LogAction.UserActivity, "System Page Navigate - Vision tab page");
            //CriticalAction.PropertyChanged += OnCriticalActionChanged;

            AppMessageHandler.SubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived += OnMessageHandlerGeneralMessageReceived;

            AppMessageHandler.SubcribeStream();
            AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;

            //AppMessageHandler.SubcribeStream();
            //AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;

            //_generalQueueSubcribed = true;

            //AppMessageHandler.SubcribeStream(VisionRoutingKey);
            //AppMessageHandler.SubcribeStream(VisionRoutingKey2);
            //AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;
            //eventSub++;
            //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "OnMessageHandlerGeneralMessageReceived + " + eventSub.ToString());
        }

        [RelayCommand]
        void Unloaded()
        {
            AppMessageHandler.UnsubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived -= OnMessageHandlerGeneralMessageReceived;

            AppMessageHandler.UnsubcribeStream();
            AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;

            _generalQueueSubcribed = false;

            ResetPage();

            //CriticalAction.PropertyChanged -= OnCriticalActionChanged;

            //eventSub--;
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "OnMessageHandlerGeneralMessageReceived - " + eventSub.ToString());

            //AppMessageHandler.UnsubcribeStream(VisionRoutingKey);
            //AppMessageHandler.UnsubcribeStream(VisionRoutingKey2);
            //AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;
        }

        private void ResetPage()
        {
            ClearROI();
            ShapeType = ShapeType.Circle;
            ClearShapes = true;
            IsTeaching = false;

            CameraModel.ImageBytes = default;
            Ratio = 1;
            FullImageWidth = DimensionHelper.MotionVideoWidth;
            FullImageHeight = DimensionHelper.MotionVideoHeight;
            ScrollX = 0;
            ScrollY = 0;
            ChildView = null;
            MousePosition = new Point(0, 0);
        }

        [RelayCommand]
        void Connect()
        {
            // 0 -> false
            // -1 -> true
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Connect ");
            InspectionVisionModel.Connected = true;
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_CONNECT;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                Connected = InspectionVisionModel.Connected,
            };

            AppMessageHandler.PublishToMachine(ConnectId, inspectionVisionDTO);
        }

        [RelayCommand]
        void Disconnect()
        {
            // 0 -> false
            // -1 -> true
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "disconnect ");
            InspectionVisionModel.Connected = false;
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_CONNECT;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                Connected = InspectionVisionModel.Connected,
            };

            AppMessageHandler.PublishToMachine(ConnectId, inspectionVisionDTO);
        }
        bool setOnce = false;
        [RelayCommand]
        void Live()
        {
            try
            {
                //_goOfflive = false;
                LogService.LogUser(LogAction.UserActivity, "System Vision Page Clicked - Live button");

                InspectionVisionModel.Acquisition = true;
                //_imageRecCount = 0;
                //_n = 0;
                //_x = 0;
                InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_ACQUISITION;
                InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

                var inspectionVisionDTO = new InspectionVisionDTO()
                {
                    ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                    CameraSelect = InspectionVisionModel.CameraSelect,
                    InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                    Acquisition = InspectionVisionModel.Acquisition
                };

                AppMessageHandler.PublishToMachine(LiveId, inspectionVisionDTO);
            }
            catch (Exception ex)
            {
                LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Live Exception" + ex.ToString());
            }
            //if(!setOnce)
            //{

            //    setOnce = true;
            //}
        }

        //bool _goOfflive = false;
        [RelayCommand]
        void OffLive()
        {
            try
            {
                AppMessageHandler.UnsubcribeStream();
                AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;

                IsDrawingEnabled = true;

                //_goOfflive = true;
                LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Offlive ");
                InspectionVisionModel.Acquisition = false;
                InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_ACQUISITION;
                InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

                var inspectionVisionDTO = new InspectionVisionDTO()
                {
                    ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                    CameraSelect = InspectionVisionModel.CameraSelect,
                    InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                    Acquisition = InspectionVisionModel.Acquisition
                };

                AppMessageHandler.PublishToMachine(LiveId, inspectionVisionDTO);
            }
            catch (Exception ex)
            {
                LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "OffLive Exception" + ex.ToString());
            }
        }
        [RelayCommand]
        void Trigger()
        {
            //InspectionVisionModel.GrabImage = true;
            //InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = "8"; // INSPEC_VISION_GRAB
            //InspectionVisionModel.InspectionVisionPage = "1"; // CameraPage

            //var inspectionVisionDTO = new InspectionVisionDTO()
            //{
            //    ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
            //    CameraSelect = InspectionVisionModel.CameraSelect,
            //    InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
            //    GrabImage = InspectionVisionModel.GrabImage
            //};

            //AppMessageHandler.PublishToMachine(TriggerId, inspectionVisionDTO);

            //if (!((LiveCamera)ChildView).IsActive)
            //    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Is not Active");
            //else
            //    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Is Active");

            //CameraModel camDflt = new CameraModel();

            //if (((LiveCamera)ChildView).IsLoading)
            //{
            //    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Image loading - Skip due to loading ");
            //}
            //else
            //{
            //    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "No skiping");
            //    CameraModel.ImageBytes = camDflt.ImageBytes;
            //    SetupLiveCamera();

            //    Thread.Sleep(2000);
            CameraModel.ImageBytes = null;
            SetupLiveCamera();
            //}
            //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "CameraModel.ImageBytes.Count ");
            //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "CameraModel.ImageBytes.Count " + CameraModel.ImageBytes.GetLongLength(1).ToString());
            //for (int n = 0; n < CameraModel.ImageBytes.GetLongLength(1); n++)
            //{
            //    CameraModel.ImageBytes[0] = 1;
            //}

            //SetupLiveCamera();
        }

        [RelayCommand]
        private void MotionChanged(DragTransition transition)
        {
            CriticalAction.UpdateCriticalAction(CriticalType.MotionMoving, true, ConfigurationService.CriticalActionTimeout);
            AppMessageHandler.PublishToMachine(MotionMovedId, new MotionMovedDTO
            {
                TransitionX = transition.TransitionX,
                TransitionY = transition.TransitionY,
            });
        }

        [RelayCommand]
        void ZoomIn()
        {
            if (Ratio < InspectionVisionModel.SelectedCamera.ZoomValue_Max)
            {
                InspectionVisionModel.ZoomValue = Ratio + 1;
                InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_ZOOM_IN;
                InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

                var inspectionVisionDTO = new InspectionVisionDTO()
                {
                    ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                    CameraSelect = InspectionVisionModel.CameraSelect,
                    InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                    ZoomValue = InspectionVisionModel.ZoomValue
                };

                AppMessageHandler.PublishToMachine(ZoomValueId, inspectionVisionDTO);
            }
        }

        [RelayCommand]
        void ZoomOut()
        {
            if (InspectionVisionModel.SelectedCamera.ZoomValue_Min < Ratio)
            {
                InspectionVisionModel.ZoomValue = Ratio == 1 ? 1 : Ratio - 1;
                InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_ZOOM_OUT;
                InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

                var inspectionVisionDTO = new InspectionVisionDTO()
                {
                    ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                    CameraSelect = InspectionVisionModel.CameraSelect,
                    InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                    ZoomValue = InspectionVisionModel.ZoomValue
                };

                AppMessageHandler.PublishToMachine(ZoomValueId, inspectionVisionDTO);
            }
        }
        [RelayCommand]
        void ZoomReset()
        {
            if (Ratio != 1)
            {
                InspectionVisionModel.ZoomValue = 1;
                InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_ZOOM_OUT;
                InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

                var inspectionVisionDTO = new InspectionVisionDTO()
                {
                    ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                    CameraSelect = InspectionVisionModel.CameraSelect,
                    InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                    ZoomValue = InspectionVisionModel.ZoomValue
                };

                AppMessageHandler.PublishToMachine(ZoomValueId, inspectionVisionDTO);
            }
        }

        [RelayCommand]
        void Save()
        {
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_SAVE;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
            };

            AppMessageHandler.PublishToMachine(SaveId, inspectionVisionDTO);
        }

        [RelayCommand]
        void ExposureValueChanged()
        {
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Expo Change ");
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_EXPOSURE;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                ExposureTime = InspectionVisionModel.ExposureTime
            };

            AppMessageHandler.PublishToMachine(ExposureTimeId, inspectionVisionDTO);
        }

        [RelayCommand]
        void GainValueChanged()
        {
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Gain Change ");
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_GAIN;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                Gain = InspectionVisionModel.Gain
            };

            AppMessageHandler.PublishToMachine(GainId, inspectionVisionDTO);
        }
        [RelayCommand]
        void GammaValueChanged()
        {
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Gamma Change ");
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_GAMMA;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                Gamma = InspectionVisionModel.Gamma
            };

            AppMessageHandler.PublishToMachine(GammaId, inspectionVisionDTO);
        }
        [RelayCommand]
        void GammaEnableValueChanged()
        {
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Gamma Enable Change ");
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_GAMMAENABLE;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                GammaEnable = InspectionVisionModel.GammaEnable
            };

            AppMessageHandler.PublishToMachine(GammaEnableId, inspectionVisionDTO);
        }
        [RelayCommand]
        void SharpnessValueChanged()
        {
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Sharpness Change ");
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_SHARPNESS;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                Sharpness = Convert.ToInt32(InspectionVisionModel.Sharpness)
            };

            AppMessageHandler.PublishToMachine(SharpnessId, inspectionVisionDTO);
        }
        [RelayCommand]
        void BlackLevelValueChanged()
        {
            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Black Level Change ");
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_BLACKLEVEL;
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                BlackLevel = InspectionVisionModel.BlackLevel
            };

            AppMessageHandler.PublishToMachine(BlackLevelId, inspectionVisionDTO);
        }
        [RelayCommand]
        void Teach()
        {
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.TeachPage;
            InspectionVisionModel.ModelSelection = (ModelSelect)Enum.Parse(typeof(ModelSelect), SelectedTeachModel);
            InspectionVisionModel.MetricSelection = (MetricSelect)Enum.Parse(typeof(MetricSelect), SelectedMetric);

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                ModelSelect = InspectionVisionModel.ModelSelection,
                MetricSelect = InspectionVisionModel.MetricSelection,
                NumLevelAutoNcc = InspectionVisionModel.NumLevelAutoNcc,
                AngStepAuto = InspectionVisionModel.AngStepAuto,
                NumLevel = InspectionVisionModel.NumLevel,
                AngleStart = InspectionVisionModel.AngleStart,
                AngleExtent = InspectionVisionModel.AngleExtent,
                AngleStep = InspectionVisionModel.AngleStep,
            };

            AppMessageHandler.PublishToMachine(TeachId, inspectionVisionDTO);
        }
        [RelayCommand]
        void StepCalib()
        {
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CalibrationPage;
            InspectionVisionModel.ProSystemVCalibartionUIElement = ProSystemVCalibartionUIElement.INSPEC_VISION_STEP_CALIBRATION;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                ProSystemVCalibartionUIElement = InspectionVisionModel.ProSystemVCalibartionUIElement,
                CalibrationModelId = InspectionVisionModel.CalibrationModelId,
                StepSize = InspectionVisionModel.StepSize,
                StepCount = InspectionVisionModel.StepCount,
            };

            AppMessageHandler.PublishToMachine(CalibrationActionId, inspectionVisionDTO);
        }
        [RelayCommand]
        void AutoCalib()
        {
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CalibrationPage;
            InspectionVisionModel.ProSystemVCalibartionUIElement = ProSystemVCalibartionUIElement.INSPEC_VISION_AUTO_CALIBRATION;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                ProSystemVCalibartionUIElement = InspectionVisionModel.ProSystemVCalibartionUIElement,
                CalibrationModelId = InspectionVisionModel.CalibrationModelId,
                StepSize = InspectionVisionModel.StepSize,
                StepCount = InspectionVisionModel.StepCount,
            };

            AppMessageHandler.PublishToMachine(CalibrationActionId, inspectionVisionDTO);
        }
        [RelayCommand]
        void StopCalib()
        {
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CalibrationPage;
            InspectionVisionModel.ProSystemVCalibartionUIElement = ProSystemVCalibartionUIElement.INSPEC_VISION_STOP_CALIBRATION;

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                ProSystemVCalibartionUIElement = InspectionVisionModel.ProSystemVCalibartionUIElement,
                CalibrationModelId = InspectionVisionModel.CalibrationModelId,
                StepSize = InspectionVisionModel.StepSize,
                StepCount = InspectionVisionModel.StepCount,
            };

            AppMessageHandler.PublishToMachine(CalibrationActionId, inspectionVisionDTO);
        }

        private void OnMessageHandlerGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == CalibrationModelIdSelectionId)
            {
                var jsonString = StringHelper.ByteArrayToString(byteArray);
                var inspectionVisionDTOs = JsonConvert.DeserializeObject<List<InspectionVisionDTO>>(jsonString);

                var tempModelIdList = new List<string>();

                for (int i = 0; i < inspectionVisionDTOs.Count; i++)
                {
                    tempModelIdList.Add(inspectionVisionDTOs[i].CalibrationModelIdSelection);
                }
                InspectionVisionModel.CalibrationModelId = "";
                InspectionVisionModel.CalibrationModelIdSelection = tempModelIdList;
            }
            if (routingKey == CircleROIResultId)
            {
                SetupCircleROIResult(byteArray);
                UpdateTeachingROI();
            }
            else if (routingKey == RectangleROIResultId)
            {
                SetupRectangleROIResult(byteArray);
                UpdateTeachingROI();
            }
            else if (routingKey == EllipseROIResultId)
            {
                SetupEllipseROIResult(byteArray);
                UpdateTeachingROI();
            }
            else
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var inspectionVisionDTO = JsonConvert.DeserializeObject<InspectionVisionDTO>(stringData);
                if (routingKey == ApplyContentId)
                {
                    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Content ");
                    InspectionVisionModel.GammaEnable = inspectionVisionDTO.GammaEnable;
                    InspectionVisionModel.ExposureTime = inspectionVisionDTO.ExposureTime;
                    InspectionVisionModel.Gain = inspectionVisionDTO.Gain;
                    InspectionVisionModel.Gamma = inspectionVisionDTO.Gamma;
                    InspectionVisionModel.BlackLevel = inspectionVisionDTO.BlackLevel;
                    InspectionVisionModel.Sharpness = inspectionVisionDTO.Sharpness;
                    //InspectionVisionModel.ZoomValue = inspectionVisionDTO.ZoomValue;
                    //InspectionVisionModel.ScrollVertical = inspectionVisionDTO.ScrollVertical;
                    //InspectionVisionModel.ScrollHorizontal = inspectionVisionDTO.ScrollHorizontal;
                }
                else if (routingKey == ConnectId)
                {
                    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Conneted ");
                    InspectionVisionModel.Connected = inspectionVisionDTO.Connected;

                    if (inspectionVisionDTO.Connected)
                    {
                        InspectionVisionModel.IsCameraConnected = true;
                    }
                    else
                    {
                        InspectionVisionModel.IsCameraConnected = false;
                        InspectionVisionModel.IsCameraLive = false;
                        InspectionVisionModel.IsTriggerBtnEnable = false;

                        //InspectionVisionModel.GammaEnable = false;
                        //InspectionVisionModel.ExposureTime = 0.00;
                        //InspectionVisionModel.Gain = 0.00;
                        //InspectionVisionModel.Gamma = 0.00;
                        //InspectionVisionModel.BlackLevel = 0.00;
                        //InspectionVisionModel.Sharpness = 0.00;
                        //InspectionVisionModel.ZoomValue = 0;
                        //InspectionVisionModel.ScrollVertical = 0;
                        //InspectionVisionModel.ScrollHorizontal = 0;
                    }
                }
                else if (routingKey == LiveId)
                {
                    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "LiveId ");
                    InspectionVisionModel.Acquisition = inspectionVisionDTO.Acquisition;

                    if (inspectionVisionDTO.Acquisition)
                    {
                        InspectionVisionModel.IsCameraLive = true;
                        InspectionVisionModel.IsTriggerBtnEnable = true;
                    }
                    else
                    {
                        InspectionVisionModel.IsCameraLive = false;
                        InspectionVisionModel.IsTriggerBtnEnable = false;

                        //InspectionVisionModel.GammaEnable = false;
                        //InspectionVisionModel.ExposureTime = 0.00;
                        //InspectionVisionModel.Gain = 0.00;
                        //InspectionVisionModel.Gamma = 0.00;
                        //InspectionVisionModel.BlackLevel = 0.00;
                        //InspectionVisionModel.Sharpness = 0.00;
                        //InspectionVisionModel.ZoomValue = 0;
                        //InspectionVisionModel.ScrollVertical = 0;
                        //InspectionVisionModel.ScrollHorizontal = 0;
                    }
                }
                else if (routingKey == TriggerId)
                {
                    InspectionVisionModel.GrabImage = inspectionVisionDTO.GrabImage;
                }
                else if (routingKey == ExposureTimeId)
                {
                    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ExposureID ");
                    InspectionVisionModel.ExposureTime = inspectionVisionDTO.ExposureTime;
                }
                else if (routingKey == GainId)
                {
                    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "GainID ");
                    InspectionVisionModel.Gain = inspectionVisionDTO.Gain;
                }
                else if (routingKey == GammaId)
                {
                    InspectionVisionModel.Gamma = inspectionVisionDTO.Gamma;
                }
                else if (routingKey == GammaEnableId)
                {
                    InspectionVisionModel.GammaEnable = inspectionVisionDTO.GammaEnable;
                }
                else if (routingKey == BlackLevelId)
                {
                    InspectionVisionModel.BlackLevel = inspectionVisionDTO.BlackLevel;
                }
                else if (routingKey == SharpnessId)
                {
                    InspectionVisionModel.Sharpness = inspectionVisionDTO.Sharpness;
                }
                else if (routingKey == ZoomValueId)
                {
                    InspectionVisionModel.ZoomValue = inspectionVisionDTO.ZoomValue;
                }
                else if (routingKey == ScrollVerticalId)
                {
                    InspectionVisionModel.ScrollVertical = inspectionVisionDTO.ScrollVertical;
                }
                else if (routingKey == ScrollHorizontalId)
                {
                    InspectionVisionModel.ScrollHorizontal = inspectionVisionDTO.ScrollHorizontal;
                }
                else if (routingKey == CircleROIResultId)
                {
                    SetupCircleROIResult(byteArray);
                    UpdateTeachingROI();
                }
                else if (routingKey == RectangleROIResultId)
                {
                    SetupRectangleROIResult(byteArray);
                    UpdateTeachingROI();
                }
                else if (routingKey == EllipseROIResultId)
                {
                    SetupEllipseROIResult(byteArray);
                    UpdateTeachingROI();
                }
                else if (routingKey == CalibrationResultId)
                {
                    InspectionVisionModel.TransactionX = inspectionVisionDTO.TransactionX;
                    InspectionVisionModel.TransactionY = inspectionVisionDTO.TransactionY;
                    InspectionVisionModel.Rotation = inspectionVisionDTO.Rotation;
                    InspectionVisionModel.ScaleX = inspectionVisionDTO.ScaleX;
                    InspectionVisionModel.ScaleY = inspectionVisionDTO.ScaleY;
                    InspectionVisionModel.FovX = inspectionVisionDTO.FovX;
                    InspectionVisionModel.FovY = inspectionVisionDTO.FovY;
                }
            }
        }

        private async void UpdateTeachingROI()
        {
            // Optimize performance for UI thread 
            await DelayUIUpdate();
            TeachingROI = null;
        }

        //int _imageRecCount = 0;
        //int _n = 0;
        //private void OnStreamMessageReceived(object sender, byte[] bytesArray, string routingKey)
        //{
        //    //if (routingKey == VisionRoutingKey)
        //    //{
        //    //    _imageRecCount++;
        //    //    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Image - event count : " + _imageRecCount.ToString());
        //    //}
        //    //if (_goOfflive)
        //    //    return;

        //    //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Stream Received ");
        //    if (routingKey == VisionRoutingKey)
        //    {
        //        //_imageRecCount++;
        //        //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Image - event count : " + _imageRecCount.ToString());

        //        lock (_lockThrd)
        //        {
        //            if ((_visionCallback.Count() == 0) || (_visionCallback.Count() == 1) || (_visionCallback.Count() == 2))
        //            {
        //                bool result = _visionCallback.TryAdd(_n, bytesArray);
        //                if (result)
        //                {
        //                    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Image - queue : " + _n.ToString());
        //                    _n++;
        //                }
        //                else
        //                    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Dict add fail : " + _imageRecCount.ToString());
        //            }
        //            else
        //            {
        //                LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Dict not add : " + _imageRecCount.ToString());
        //            }
        //        }

        //    }
        //}

        //private void OnStreamMessageReceived(object sender, byte[] bytesArray, string routingKey)
        //{
        //    if (routingKey == VisionRoutingKey)
        //    {
        //        CameraModel.ImageBytes = bytesArray;
        //        SetupLiveCamera();
        //    }
        //    else if (routingKey == VisionRoutingKey2)
        //    {
        //        LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived Start ");
        //        _imageRecCount++;
        //        LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived Image count : " + _imageRecCount.ToString());

        //        if (_visionCallback.Count() < 1)
        //        {
        //            var stringData = Encoding.UTF8.GetString(bytesArray);
        //            var imageStreamDTO = JsonConvert.DeserializeObject<ImageStreamDTO>(stringData);
        //            //CameraModel.ImageBytes = imageStreamDTO.StreamData;

        //            bool result = _visionCallback.TryAdd(_n++, imageStreamDTO);
        //            if (result)
        //            {
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "_visionCallback - Image queue : " + (_n - 1).ToString());
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - ImageID: " + imageStreamDTO.ImageID);
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - timeStamp: " + imageStreamDTO.Timestamp);
        //            }
        //            else
        //                LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - Add to queue fail " + (_n - 1).ToString());
        //        }
        //        else
        //        {
        //            LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - Add to queue skip " + _imageRecCount.ToString());
        //        }
        //        LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived Done ");

        //        //var stringData = Encoding.UTF8.GetString(bytesArray);
        //        //var imageStreamDTO = JsonConvert.DeserializeObject<ImageStreamDTO>(stringData);
        //        //CameraModel.ImageBytes = imageStreamDTO.StreamData;

        //        //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - ImageID: " + imageStreamDTO.ImageID);
        //        //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - timeStamp: " + imageStreamDTO.timeStamp);

        //        //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - SetupLiveCamera: Start  " + imageStreamDTO.timeStamp);
        //        //SetupLiveCamera();
        //        //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Latency check - OnStreamMessageReceived - SetupLiveCamera: Done  " + imageStreamDTO.timeStamp);


        //    }
        //}

        private static bool _isLoading = false;
        private void OnStreamMessageReceived(object _, byte[] bytesArray)
        {
            if (_isLoading)
            {
                Console.WriteLine("SKIP");
                LogService.LogUser(LogAction.Receive, $"SKIP Data to stream - TimeStamp: {ImageTimestamp.ToString("yyyyMMdd HH:mm:ss.fff")}");
                return;
            }

            Console.WriteLine("RECEIVED");

            _isLoading = true;

            var jsonString = StringHelper.ByteArrayToString(bytesArray);
            var imageStreamDTO = JsonConvert.DeserializeObject<ImageStreamDTO>(jsonString);

            CameraModel.ImageBytes = imageStreamDTO.ImageBytes;

            //FullImageWidth = imageStreamDTO.FullImageWidth;
            //FullImageHeight = imageStreamDTO.FullImageHeight;

            Ratio = imageStreamDTO.Ratio;

            ScrollX = imageStreamDTO.ScrollX;
            ScrollY = imageStreamDTO.ScrollY;
            ImageTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(imageStreamDTO.Timestamp).DateTime.ToLocalTime();

            LogService.LogUser(LogAction.Receive, $"Data to stream - TimeStamp: {ImageTimestamp.ToString("yyyyMMdd HH:mm:ss.fff")}");
            SetupLiveCamera();

            _isLoading = false;
        }

        public void Scroll(double scrollX, double scrollY)
        {
            //ScrollX = scrollX;
            //ScrollY = scrollY;
            //AppMessageHandler.PublishToMachine(ImageDtoId, CreateImageDto());
            if (IsCameraChanging)
                return;

            ScrollValueChanged(scrollX, scrollY);
        }

        public void ScrollValueChanged(double scrollX, double scrollY)
        {
            InspectionVisionModel.ScrollHorizontal = (int)scrollX;
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_SCROLL_HORIZONTAL; 
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage; 

            var inspectionVisionDTO = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                ScrollHorizontal = InspectionVisionModel.ScrollHorizontal
            };
            AppMessageHandler.PublishToMachine(ScrollHorizontalId, inspectionVisionDTO);

            InspectionVisionModel.ScrollVertical = (int)scrollY;
            InspectionVisionModel.ProSystemUpLookInspecVisionUIElement = ProSystemUpLookInspecVisionUIElement.INSPEC_VISION_SCROLL_VERTICAL; 
            InspectionVisionModel.InspectionVisionPage = InspectionVisionPage.CameraPage; 

            var inspectionVisionDTO2 = new InspectionVisionDTO()
            {
                ProSystemUpLookInspecVisionUIElement = InspectionVisionModel.ProSystemUpLookInspecVisionUIElement,
                CameraSelect = InspectionVisionModel.CameraSelect,
                InspectionVisionPage = InspectionVisionModel.InspectionVisionPage,
                ScrollVertical = InspectionVisionModel.ScrollVertical
            };

            AppMessageHandler.PublishToMachine(ScrollVerticalId, inspectionVisionDTO2);
        }

        [RelayCommand]
        private void PointChanged(Point point)
        {
            MousePosition = point.Round();
        }

        [RelayCommand]
        private void DiscardROI()
        {
            //TeachingROI = null;
            //IsDrawingEnabled = EnableDrawing();
            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Clicked - Discard ROI button");
            ClearROI();
        }

        // Do not make any http request called inside this function as it run when scrolling camera
        public void ClearROI()
        {
            TeachingROI = null;
            IsDrawingEnabled = true;
        }

        [RelayCommand]
        private void DiscardROIResult()
        {
            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Clicked - Discard ROI Result button");
            ResultROIs = null;

            OnPropertyChanged(nameof(ResultROIs));
            IsTeaching = false;
            IsDrawingEnabled = true;
        }

        [RelayCommand]
        private void SaveResult()
        {
            AppMessageHandler.PublishToMachine(SaveResultCommandId, null);
        }

        [RelayCommand]
        private void ApplyROI()
        {
            //CriticalAction.UpdateCriticalAction(CriticalType.Any, true, ConfigurationService.CriticalActionTimeout);

            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Clicked - Apply button");

            IsApplyButtonEnabled = false;
            IsDiscardButtonEnabled = false;
            IsTeaching = true;
            IsDrawingEnabled = true;

            if (InspectionVisionModel.IsCameraLive)
            {
                _imageToAppliedROI = CameraModel.ImageBytes;
            }
            var isImageNullOrEmpty = _imageToAppliedROI == null || _imageToAppliedROI.Length == 0;
            var imageBytesROI = !isImageNullOrEmpty ? ROIHelper.GetROIImageBytes(_imageToAppliedROI, TeachingROI) : [];
            switch (ShapeType)
            {
                case ShapeType.Circle:
                    {
                        var circle = (Circle)TeachingROI;
                        var sendingROIDto = new CircleROIDTO
                        {
                            ShapeName = "Circle",
                            RowCenter = circle.CenterX,
                            ColumnCenter = circle.CenterY,
                            Radius = circle.Radius,
                            ImageBytes = imageBytesROI
                        };
                        if (InspectionVisionModel.DrawShapeType == DrawShapeCategory.ROI)
                            AppMessageHandler.PublishToMachine(ROIDtoId, sendingROIDto);
                        else if (InspectionVisionModel.DrawShapeType == DrawShapeCategory.SearchRegion)
                            AppMessageHandler.PublishToMachine(RegionDtoId, sendingROIDto);
                        break;
                    }
                case ShapeType.Rectangle:
                    {
                        var rectangle = (Rectangle)TeachingROI;
                        var sendingROIDto = new RectangleROIDTO
                        {
                            ShapeName = "Rectangle",
                            Row1 = rectangle.Width < 0 ? rectangle.StartX + rectangle.Width : rectangle.StartX - ScrollX,
                            Column1 = rectangle.Height < 0 ? rectangle.StartY + rectangle.Height : rectangle.StartY - ScrollY,
                            Row2 = rectangle.Width < 0 ? rectangle.StartX : rectangle.StartX + rectangle.Width - ScrollX,
                            Column2 = rectangle.Height < 0 ? rectangle.StartY : rectangle.StartY + rectangle.Height - ScrollY,
                            RowCenter = rectangle.StartX + rectangle.Width / 2,
                            ColumnCenter = rectangle.StartY + rectangle.Height / 2,
                            Phi = rectangle.Phi,
                            ImageBytes = imageBytesROI
                        };
                        if (InspectionVisionModel.DrawShapeType == DrawShapeCategory.ROI)
                            AppMessageHandler.PublishToMachine(ROIDtoId, sendingROIDto);
                        else if (InspectionVisionModel.DrawShapeType == DrawShapeCategory.SearchRegion)
                            AppMessageHandler.PublishToMachine(RegionDtoId, sendingROIDto);
                        break;
                    }
                case ShapeType.Ellipse:
                    {
                        var ellipse = (Ellipse)TeachingROI;
                        var sendingROIDto = new EllipseROIDTO
                        {
                            ShapeName = "Ellipse",
                            RowCenter = ellipse.CenterX,
                            ColumnCenter = ellipse.CenterY,
                            Row1 = ellipse.CenterX - ellipse.RadiusX,
                            Column1 = ellipse.CenterY - ellipse.RadiusY,
                            Radius1 = ellipse.RadiusX,
                            Radius2 = ellipse.RadiusY,
                            Phi = ellipse.Phi,
                            ImageBytes = imageBytesROI
                        };
                        if (InspectionVisionModel.DrawShapeType == DrawShapeCategory.ROI)
                            AppMessageHandler.PublishToMachine(ROIDtoId, sendingROIDto);
                        else if (InspectionVisionModel.DrawShapeType == DrawShapeCategory.SearchRegion)
                            AppMessageHandler.PublishToMachine(RegionDtoId, sendingROIDto);
                        break;
                    }
                default: throw new ArgumentException($"{ShapeType} is not valid");
            }
        }

        [RelayCommand]
        private void OnDrawComplete(IShapeROI shape)
        {
            TeachingROI = shape;
        }

        private VisionImageDto CreateImageDto()
        {
            var imageDto = new VisionImageDto
            {
                FullImageWidth = FullImageWidth,
                FullImageHeight = FullImageHeight,
                ImageVisualWidth = DimensionHelper.MotionVideoWidth,
                ImageVisualHeight = DimensionHelper.MotionVideoHeight,
                ScrollX = ScrollX,
                ScrollY = ScrollY,
                Ratio = Ratio
            };

            return imageDto;
        }

        private void SetupLiveCamera()
        {
            if (ChildView?.GetType() != typeof(LiveCamera))
            {
                var liveCamera = new LiveCamera();
                liveCamera.IsActive = true;
                liveCamera.CameraSource = CameraModel.ImageBytes;
                ChildView = liveCamera;
            }
            else
            {
                if (!((LiveCamera)ChildView).IsActive)
                {
                    LogService.LogUserLocal(ZWave.Shared.Enums.LogAction.Connect, "Latency check - LiveCamera.IsActive not");
                }
                else
                    ((LiveCamera)ChildView).CameraSource = CameraModel.ImageBytes;
            }


            //if (ChildView?.GetType() != typeof(LiveCamera))
            //{
            //    var liveCamera = new LiveCamera();
            //    liveCamera.IsActive = true;
            //    liveCamera.CameraSource = CameraModel.ImageBytes;
            //    ChildView = liveCamera;
            //}
            //else
            //{
            //    ((LiveCamera)ChildView).CameraSource = null;
            //    if (((LiveCamera)ChildView).IsLoading)
            //    {
            //        LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Image loading - Skip due to loading ");
            //    }
            //    else
            //    {
            //        LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Image loading - Start ");
            //        ((LiveCamera)ChildView).CameraSource = CameraModel.ImageBytes;
            //        LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "Inspec Image loading - End ");
            //    }

            //}
        }
        //private void SetupROIResult(byte[] e)
        //{
        //    var jsonString = StringHelper.ByteArrayToString(e);
        //    var CircleROIDTOs = JsonConvert.DeserializeObject<List<CircleROIDTO>>(jsonString);

        //    ResultROIs = CircleROIDTOs.Select(circleROIDTO =>
        //    new ROI
        //    {
        //        ShapeType = ShapeType.Circle,
        //        Coordinator = new Coordinator
        //        {
        //            StartPoint = new Point()
        //            {
        //                X = circleROIDTO.RowCenter - circleROIDTO.Radius,
        //                Y = circleROIDTO.ColumnCenter - circleROIDTO.Radius,
        //            },
        //            EndPoint = new Point()
        //            {
        //                X = circleROIDTO.RowCenter + circleROIDTO.Radius,
        //                Y = circleROIDTO.ColumnCenter + circleROIDTO.Radius,
        //            }
        //        }
        //    }).ToList();

        //    OnPropertyChanged(nameof(ResultROIs));
        //}

        private void SetupCircleROIResult(byte[] e)
        {
            var jsonString = StringHelper.ByteArrayToString(e);
            var circleROIDTOs = JsonConvert.DeserializeObject<List<CircleROIDTO>>(jsonString);

            ResultROIs = circleROIDTOs.Select(circleROIDTO =>
                new Circle
                {
                    CenterX = (float)circleROIDTO.RowCenter,
                    CenterY = (float)circleROIDTO.ColumnCenter,
                    Radius = (float)circleROIDTO.Radius,
                    Score = (float)circleROIDTO.Score,
                });
        }

        private void SetupRectangleROIResult(byte[] e)
        {
            var jsonString = StringHelper.ByteArrayToString(e);
            var reactangleROIDTOs = JsonConvert.DeserializeObject<List<RectangleROIDTO>>(jsonString);

            ResultROIs = reactangleROIDTOs.Select(recROIDTO =>
                new Rectangle
                {
                    StartX = (float)recROIDTO.Row1,
                    StartY = (float)recROIDTO.Column1,
                    Width = (float)(recROIDTO.Column2 - recROIDTO.Column1),
                    Height = (float)(recROIDTO.Row2 - recROIDTO.Row1),
                    Score = (float)recROIDTO.Score,
                    Phi = NormalizePhi((float)recROIDTO.Phi, true),
                });
        }

        private void SetupEllipseROIResult(byte[] e)
        {
            var jsonString = StringHelper.ByteArrayToString(e);
            var ellipseROIDTOs = JsonConvert.DeserializeObject<List<EllipseROIDTO>>(jsonString);

            ResultROIs = ellipseROIDTOs.Select(ellipeROIDTO =>
                new Ellipse
                {
                    CenterX = (float)ellipeROIDTO.RowCenter,
                    CenterY = (float)ellipeROIDTO.ColumnCenter,
                    RadiusX = (float)ellipeROIDTO.Radius1,
                    RadiusY = (float)ellipeROIDTO.Radius2,
                    Score = (float)ellipeROIDTO.Score,
                    Phi = NormalizePhi((float)ellipeROIDTO.Phi, true),
                });
        }

        //        private bool EnableDrawing()
        //        {
        //#if ANDROID
        //            return ActionModeSelectorVisible && ActionMode == ActionMode.Draw;
        //#endif
        //            return true;
        //        }

        private async Task DelayUIUpdate()
        {
            await Task.Delay(DELAY_CODE_EXCUTION_TIME);
        }

        private void OnCriticalActionChanged(object sender, PropertyChangedEventArgs e)
        {
            var criticalActionManager = (CriticalActionManager)sender;

            if (!criticalActionManager[CriticalType.Any.ToString()])
            {
                IsDiscardButtonEnabled = TeachingROI != null;
                IsApplyButtonEnabled = TeachingROI != null;
            }
        }

        private float NormalizePhi(float phi, bool invert = false)
        {
            if (!invert)
            {
                if (phi > 0 && phi < 180)
                {
                    phi = -phi;
                }
                if (phi >= 180)
                {
                    phi = 360 - phi;
                }
            }
            else
            {
                if (phi < 0)
                {
                    phi = 360 + phi;
                }
                if (phi >= 180)
                {
                    phi = -phi;
                }
            }

            return phi;
        }

        //private async void FetchTriggerData()
        //{
        //    var response = await AppMessageHandler.Request(TriggerDataId, null);
        //    _imageToAppliedROI = response;
        //    var stream = new MemoryStream(response);
        //    ChildView = new ImageFromStream(stream);
        //}

        //private async void FetchCameraParams()
        //{
        //    var response = await AppMessageHandler.Request(FetchCameraParamsId, null);
        //    var strResponse = StringHelper.ByteArrayToString(response);
        //    var message = JsonConvert.DeserializeObject<Message>(strResponse);

        //    var cameraDto = JsonConvert.DeserializeObject<CameraParamsDTO>(message.Data.ToString());

        //    CameraModel.SetExposure(cameraDto.Exposure);
        //    CameraModel.SetLight(cameraDto.Light);
        //}
        //partial void OnIsCameraOnLiveChanged(bool oldValue, bool newValue)
        //{
        //    if (newValue)
        //    {
        //        CameraModel.CameraId = Guid.NewGuid().ToString();
        //        //TriggerButtonEnabled = true;
        //        //LiveButtonEnabled = false;
        //    }
        //    else
        //    {
        //        //TriggerButtonEnabled = false;
        //        //LiveButtonEnabled = true;
        //        CameraModel = new CameraModel();
        //    }
        //}

    }
}
