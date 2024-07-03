using CommonLib.DTO.Implementation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZWave.Controls;
using ZWave.Enums;
using ZWave.Helpers;
using ZWave.Models;
using Newtonsoft.Json;
using ZWave.Helpers;
using System.ComponentModel;
using CommonLib.Enums;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Systems.Vision
{
    public partial class SystemVisionPageViewModel : BaseViewModel
    {
        private static readonly int DELAY_CODE_EXCUTION_TIME = 100; //Milliseconds;

        private string ImageDtoId => Master.System.Vision.ImageDTO;
        private string ROIDtoId => Master.System.Vision.ROIDTO;
        private string CircleROIResultId => Master.System.Vision.CircleROIResultId;
        private string RectangleROIResultId => Master.System.Vision.RectangleROIResultId;
        private string EllipseROIResultId => Master.System.Vision.EllipseROIResultId;
        private string SaveResultCommandId => Master.System.Vision.SaveResultCommandId;

        private string Topic => Master.System.Vision.BasePath + "#";
        private string ExposureRoutingKey => Master.System.Vision.Exposure;
        private string LightRoutingKey => Master.System.Vision.Light;

        private string ConnectId => Master.System.Vision.Connect;
        private string TriggerDataId => Master.System.Vision.Trigger;
        private string FetchCameraParamsId => Master.System.Vision.CameraParams;

        private string MotionMovedId => Master.System.Vision.MotionMovedId;

        private byte[] _imageToAppliedROI;
        private bool _generalQueueSubcribed;

        [ObservableProperty]
        private View _childView;

        [ObservableProperty]
        private bool _connectButtonEnabled;

        [ObservableProperty]
        private bool _liveButtonEnabled;

        [ObservableProperty]
        private bool _triggerButtonEnabled;

        [ObservableProperty]
        private bool _saveButtonEnabled;

        [ObservableProperty]
        private bool _isDrawingEnabled;

        [ObservableProperty]
        private bool _isCameraOnLive = false;

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

        public SystemVisionPageViewModel()
        {
            ConnectButtonEnabled = true;
            LiveButtonEnabled = false;
            TriggerButtonEnabled = false;
            SaveButtonEnabled = false;
            CameraModel = new CameraModel();

            _shapeType = ShapeType.Circle;
            _motionMode = MotionMode.Teaching;
            _imageToAppliedROI = [];

            IsApplyButtonEnabled = false;
            IsDiscardButtonEnabled = false;
        }

        [RelayCommand]
        void Loaded()
        {
            LogService.LogUser(LogAction.UserActivity, "System Page Navigate - Vision tab page");
            CriticalAction.PropertyChanged += OnCriticalActionChanged;
        }

        [RelayCommand]
        void Unloaded()
        {
            AppMessageHandler.UnsubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived -= OnGeneralMessageReceived;

            AppMessageHandler.UnsubcribeStream();
            AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;

            _generalQueueSubcribed = false;

            ResetPage();

            CriticalAction.PropertyChanged -= OnCriticalActionChanged;
        }

        private void ResetPage()
        {
            if (!ConnectButtonEnabled)
            {
                ClearROI();
                ShapeType = ShapeType.Circle;
                ClearShapes = true;
                IsTeaching = false;
                IsCameraOnLive = false;
                LiveButtonEnabled = true;
                IsApplyButtonEnabled = false;
                IsDiscardButtonEnabled = false;
                TriggerButtonEnabled = false;
                SaveButtonEnabled = false;
            }
            CameraModel.ImageBytes = default;
            CameraModel.Exposure = 0;
            CameraModel.Light = false;
            Ratio = 1;
            FullImageWidth = DimensionHelper.MotionVideoWidth;
            FullImageHeight = DimensionHelper.MotionVideoHeight;
            ScrollX = 0;
            ScrollY = 0;
            ChildView = null;
            MousePosition = new Point(0, 0);
        }

        [RelayCommand]
        private void Connect()
        {
            LogService.LogUser(LogAction.UserActivity, "System Vision Page Clicked - Connect button");
            AppMessageHandler.PublishToMachine(ConnectId, null);
            ConnectButtonEnabled = false;
            LiveButtonEnabled = true;
        }

        [RelayCommand]
        private void Live()
        {
            LogService.LogUser(LogAction.UserActivity, "System Vision Page Clicked - Live button");
            AppMessageHandler.SubcribeStream();
            AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;

            IsCameraOnLive = true;
            FetchCameraParams();
            if (!_generalQueueSubcribed)
            {
                AppMessageHandler.SubcribeGeneral(Topic);
                AppMessageHandler.OnGeneralMessageReceived += OnGeneralMessageReceived;
                _generalQueueSubcribed = true;
            }
            IsDrawingEnabled = true;
            SaveButtonEnabled = false;
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

        private void OnGeneralMessageReceived(object sender, byte[] bytesArray, string routingKey)
        {
            if (IsCameraOnLive)
            {
                if (routingKey == ExposureRoutingKey)
                {
                    SetupExposure(bytesArray);
                }
                else if (routingKey == LightRoutingKey)
                {
                    SetupLight(bytesArray);
                }
            }
            if (routingKey == CircleROIResultId)
            {
                SetupCircleROIResult(bytesArray);
                UpdateTeachingROI();
            }
            else if (routingKey == RectangleROIResultId)
            {
                SetupRectangleROIResult(bytesArray);
                UpdateTeachingROI();
            }
            else if (routingKey == EllipseROIResultId)
            {
                SetupEllipseROIResult(bytesArray);
                UpdateTeachingROI();
            }
        }

        private async void UpdateTeachingROI()
        {
            // Optimize performance for UI thread 
            await DelayUIUpdate();
            TeachingROI = null;
        }

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

            FullImageWidth = imageStreamDTO.FullImageWidth;
            FullImageHeight = imageStreamDTO.FullImageHeight;

            Ratio = imageStreamDTO.Ratio;

            ScrollX = imageStreamDTO.ScrollX;
            ScrollY = imageStreamDTO.ScrollY;
            ImageTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(imageStreamDTO.Timestamp).DateTime.ToLocalTime();

            LogService.LogUser(LogAction.Receive, $"Data to stream - TimeStamp: {ImageTimestamp.ToString("yyyyMMdd HH:mm:ss.fff")}");
            SetupLiveCamera();

            _isLoading = false;
        }

        [RelayCommand]
        private void Trigger()
        {
            LogService.LogUser(LogAction.UserActivity, "System Vision Page Clicked - Trigger button");
            AppMessageHandler.UnsubcribeStream();
            AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;

            IsCameraOnLive = false;
            SaveButtonEnabled = true;
            FetchTriggerData();
        }

        [RelayCommand]
        private void ZoomIn()
        {
            if (FullImageWidth / Ratio > DimensionHelper.MotionVideoWidth ||
                FullImageHeight / Ratio > DimensionHelper.MotionVideoHeight)
                Ratio++;

            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Clicked - Zoom in button - ratio {Ratio}");
            AppMessageHandler.PublishToMachine(ImageDtoId, CreateImageDto());
        }

        [RelayCommand]
        private void ZoomOut()
        {
            Ratio = Ratio == 1 ? 1 : Ratio - 1;

            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Clicked - Zoom out button - ratio {Ratio}");
            AppMessageHandler.PublishToMachine(ImageDtoId, CreateImageDto());
        }

        public void Scroll(double scrollX, double scrollY)
        {
            ScrollX = scrollX;
            ScrollY = scrollY;

            AppMessageHandler.PublishToMachine(ImageDtoId, CreateImageDto());
        }

        [RelayCommand]
        private void PointChanged(Point point)
        {
            MousePosition = point;
        }

        [RelayCommand]
        private void DiscardROI()
        {
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
            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Clicked - Save Result button");
            AppMessageHandler.PublishToMachine(SaveResultCommandId, null);
        }

        [RelayCommand]
        private void ApplyROI()
        {
            CriticalAction.UpdateCriticalAction(CriticalType.Any, true, ConfigurationService.CriticalActionTimeout);

            LogService.LogUser(LogAction.UserActivity, $"System Vision Page Clicked - Apply button");
            IsApplyButtonEnabled = false;
            IsDiscardButtonEnabled = false;
            IsTeaching = true;
            IsDrawingEnabled = false;

            if (IsCameraOnLive)
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
                            RowCenter = circle.CenterX,
                            ColumnCenter = circle.CenterY,
                            Radius = circle.Radius,
                            //ImageBytes = imageBytesROI
                        };
                        AppMessageHandler.PublishToMachine(ROIDtoId, sendingROIDto);
                        break;
                    }
                case ShapeType.Rectangle:
                    {
                        var rectangle = (Rectangle)TeachingROI;
                        var sendingROIDto = new RectangleROIDTO
                        {
                            Row1 = rectangle.Width < 0 ? rectangle.StartX + rectangle.Width : rectangle.StartX,
                            Column1 = rectangle.Height < 0 ? rectangle.StartY + rectangle.Height : rectangle.StartY,
                            Row2 = rectangle.Width < 0 ? rectangle.StartX : rectangle.StartX + rectangle.Width,
                            Column2 = rectangle.Height < 0 ? rectangle.StartY : rectangle.StartY + rectangle.Height,
                            RowCenter = rectangle.StartX + rectangle.Width / 2,
                            ColumnCenter = rectangle.StartY + rectangle.Height / 2,
                            Phi = rectangle.Phi,
                            //ImageBytes = imageBytesROI
                        };
                        AppMessageHandler.PublishToMachine(ROIDtoId, sendingROIDto);
                        break;
                    }
                case ShapeType.Ellipse:
                    {
                        var ellipse = (Ellipse)TeachingROI;
                        var sendingROIDto = new EllipseROIDTO
                        {
                            RowCenter = ellipse.CenterX,
                            ColumnCenter = ellipse.CenterY,
                            Row1 = ellipse.CenterX - ellipse.RadiusX,
                            Column1 = ellipse.CenterY - ellipse.RadiusY,
                            Radius1 = ellipse.RadiusX,
                            Radius2 = ellipse.RadiusY,
                            Phi = ellipse.Phi,
                            //ImageBytes = imageBytesROI
                        };
                        AppMessageHandler.PublishToMachine(ROIDtoId, sendingROIDto);
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
                ((LiveCamera)ChildView).CameraSource = CameraModel.ImageBytes;
            }
        }

        private void SetupExposure(byte[] e)
        {
            var jsonString = StringHelper.ByteArrayToString(e);
            var exposure = JsonConvert.DeserializeObject<double>(jsonString);
            CameraModel.SetExposure(exposure);
        }

        private void SetupLight(byte[] e)
        {
            var jsonString = StringHelper.ByteArrayToString(e);
            var light = JsonConvert.DeserializeObject<bool>(jsonString);
            CameraModel.SetLight(light);
        }

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

        private async void FetchTriggerData()
        {
            var response = await AppMessageHandler.Request(TriggerDataId, null);
            _imageToAppliedROI = response;
            var stream = new MemoryStream(response);
            ChildView = new ImageFromStream(stream);
        }

        private async void FetchCameraParams()
        {
            var response = await AppMessageHandler.Request(FetchCameraParamsId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);

            var cameraDto = JsonConvert.DeserializeObject<CameraParamsDTO>(message.Data.ToString());

            CameraModel.SetExposure(cameraDto.Exposure);
            CameraModel.SetLight(cameraDto.Light);
        }

        partial void OnIsCameraOnLiveChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                CameraModel.CameraId = Guid.NewGuid().ToString();
                TriggerButtonEnabled = true;
                LiveButtonEnabled = false;
            }
            else
            {
                TriggerButtonEnabled = false;
                LiveButtonEnabled = true;
                CameraModel = new CameraModel();
            }
        }
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
    }
}
