using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using ZWave.ViewModels.Systems.Motions;
using CommunityToolkit.Mvvm.Input;
using ZWave.Helpers;
using Newtonsoft.Json;
using CommonLib.DTO.Implementation;
using ZWave.Models;

namespace ZWave.ViewModels.Motions
{
    public partial class MotionControlPageViewModel : BaseSystemMotionViewModel
    {
        private static readonly SKColor s_blue = new(25, 118, 210);
        private static readonly SKColor s_red = new(229, 57, 53);
        private static readonly SKColor s_yellow = new(198, 167, 0);

        private static readonly int POINT_PER_SERIES = 25;

        private string Topic => Master.System.Motion.MotionControl.BasePath + "#";
        private string FetchProfilesId => Master.System.Motion.MotionControl.Profiles;
        private string FetchLogsId => Master.System.Motion.MotionControl.Logs;
        private string FetchMotionKinematics => Master.System.Motion.MotionControl.Kinematics;
        private string KinematicsRoutingKey => Master.System.Motion.MotionControl.KinematicsRoute;
        private string RequestCalculateId => Master.System.Motion.MotionControl.Calculate;
        private string RequestStartId => Master.System.Motion.MotionControl.Start;
        private string RequestStopId => Master.System.Motion.MotionControl.Stop;
        private string TestStateRoutingKey => Master.System.Motion.MotionControl.TestState;

        private DateTimeAxis _customAxis;
        private List<List<DateTimePoint>> _listSeries;
        private List<Series> _newSeries = new List<Series>();
        private bool _isReading = true;
        private readonly Random _random = new();

        public object Sync { get; } = new object();

        [ObservableProperty]
        ObservableCollection<ISeries> _series;

        [ObservableProperty]
        ICartesianAxis[] _xAxes;

        [ObservableProperty]
        ICartesianAxis[] _yAxes;

        #region Control Profile Properties
        [ObservableProperty]
        List<MotionProfileModel> _profileModels;

        [ObservableProperty]
        MotionProfileModel _selectedProfile;
        partial void OnSelectedProfileChanged(MotionProfileModel value)
        {
            if (value != null)
            {
                Velocity = value.Velocity;
                Acceleration = value.Acceleration;
                Jerk = value.Jerk;
            }
        }

        [ObservableProperty]
        double _actualVelocity;

        [ObservableProperty]
        double _velocity;

        [ObservableProperty]
        double _actualAcceleration;

        [ObservableProperty]
        double _acceleration;

        [ObservableProperty]
        double _actualJerk;

        [ObservableProperty]
        double _jerk;

        [ObservableProperty]
        double _pointOne;

        [ObservableProperty]
        double _pointOneDelay;

        [ObservableProperty]
        double _pointOneEstTime;

        [ObservableProperty]
        double _pointTwo;

        [ObservableProperty]
        double _pointTwoDelay;

        [ObservableProperty]
        double _pointTwoEstTime;

        [ObservableProperty]
        double _noOfCycles;

        [ObservableProperty]
        double _totalEstCycleTime;

        [ObservableProperty]
        double _currentCycles;

        [ObservableProperty]
        double _totalCycles;

        [ObservableProperty]
        double _commandPosition;

        [ObservableProperty]
        double _encoderPosition;

        [ObservableProperty]
        double _missingSteps;
        #endregion

        [RelayCommand]
        void Loaded()
        {
            InitialProperty();
            FetchProfilesAsync();
            FetchMotionKinematicsAsync();
            AppMessageHandler.SubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived += OnGeneralMessageReceived;
            _isReading = true;
        }

        [RelayCommand]
        void UnLoaded()
        {
            AppMessageHandler.UnsubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived -= OnGeneralMessageReceived;
            _isReading = false;
        }

        [RelayCommand]
        void Calculate()
        {
            CalculateMotionTestAsync();
        }

        [RelayCommand]
        void Start()
        {
            AppMessageHandler.PublishToMachine(RequestStartId, GetMotionTestDTO());
        }

        private MotionTestDTO GetMotionTestDTO()
        {
            return new MotionTestDTO
            {
                Name = SystemMotionData.SelectedAxis.AxisName ?? string.Empty,
                Velocity = Velocity,
                Acceleration = Acceleration,
                Jerk = Jerk,
                PointOne = PointOne,
                PointOneDelay = PointOneDelay,
                PointTwo = PointTwo,
                PointTwoDelay = PointTwoDelay,
                NoOfCycles = NoOfCycles,
            };
        }

        [RelayCommand]
        void Stop()
        {
            AppMessageHandler.PublishToMachine(RequestStopId, null);
        }

        [RelayCommand]
        void Logs()
        {
            // Must confirm more about logs of motion before process more.
            AppMessageHandler.Request(FetchLogsId, null);
        }

        public MotionControlPageViewModel()
        {
            InitialChart();
            ReadData();
        }

        private void InitialProperty()
        {
            _listSeries = new();
            Series = new ObservableCollection<ISeries>();
            SelectedProfile = new MotionProfileModel();
            ProfileModels = new List<MotionProfileModel>();
        }

        private void InitialChart()
        {
            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                Name = "Time Tick",
                NameTextSize = 14,
                NamePaint = new SolidColorPaint(s_blue),
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 0, 0, 20),
                TextSize = 12,
                LabelsPaint = new SolidColorPaint(s_blue),
                TicksPaint = new SolidColorPaint(s_blue),
                DrawTicksPath = true,

                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = new Axis[] { _customAxis };
            YAxes =
            [
                new Axis
                {
                    Name = "Y1",
                    NameTextSize = 14,
                    NamePaint = new SolidColorPaint(s_red),
                    NamePadding = new LiveChartsCore.Drawing.Padding(0, 20),
                    Padding =  new LiveChartsCore.Drawing.Padding(0, 0, 20, 0),
                    TextSize = 12,
                    LabelsPaint = new SolidColorPaint(s_red),
                    TicksPaint = new SolidColorPaint(s_red),
                    DrawTicksPath = true,
                },
                new Axis
                {
                    Name = "Y2",
                    NameTextSize = 14,
                    NamePadding = new LiveChartsCore.Drawing.Padding(0, 20),
                    Padding =  new LiveChartsCore.Drawing.Padding(20, 0, 0, 0),
                    NamePaint = new SolidColorPaint(s_yellow),
                    TextSize = 12,
                    LabelsPaint = new SolidColorPaint(s_yellow),
                    TicksPaint = new SolidColorPaint(s_yellow),
                    DrawTicksPath = true,
                    ShowSeparatorLines = false,
                    Position = LiveChartsCore.Measure.AxisPosition.End,
                }
            ];
        }

        private async void ReadData()
        {
            while (_isReading)
            {
                await Task.Delay(100);
                _newSeries = new List<Series>
                {
                    new Series() { Id = 0, YValue = _random.Next() },
                    new Series() { Id = 1, YValue = _random.Next() }
                };
                UpdateChart();
            }
        }

        private void UpdateChart()
        {
            lock (Sync)
            {
                for (int i = 0; i < _newSeries.Count; i++)
                {
                    if (_listSeries.Count < i + 1)
                    {
                        _listSeries.Add(new List<DateTimePoint>());

                        Series.Add(new LineSeries<DateTimePoint>
                        {
                            Values = _listSeries[i],
                            Fill = null,
                            GeometrySize = 10,
                            ScalesYAt = 0
                        });
                    }

                    _listSeries[i].Add(new DateTimePoint(DateTime.Now, _newSeries[i].YValue));
                    if (_listSeries[i].Count > POINT_PER_SERIES)
                    {
                        _listSeries[i].RemoveAt(0);
                    }
                }

                if (_newSeries.Count < _listSeries.Count)
                {
                    Series.RemoveAt(_listSeries.Count - 1);
                    _listSeries.RemoveAt(_listSeries.Count - 1);
                }

                _customAxis.CustomSeparators = GetSeparators();
            }
        }

        private static string Formatter(DateTime date)
        {
            var secsAgo = (DateTime.Now - date).TotalSeconds;

            return secsAgo < 1
                ? "now"
                : $"{secsAgo:N0}s ago";
        }

        private double[] GetSeparators()
        {
            var now = DateTime.Now;

            return new double[]
            {
                now.AddSeconds(-3).Ticks,
                now.AddSeconds(-2).Ticks,
                now.AddSeconds(-1).Ticks,
                now.Ticks
            };
        }


        private void OnGeneralMessageReceived(object sender, byte[] bytesArray, string routingKey)
        {
            if (routingKey == KinematicsRoutingKey)
            {
                var motionKinematicsDTO = GetDataFromGeneralQueue<MotionKinematicsDTO>(bytesArray);
                UpdateMotionKinematics(motionKinematicsDTO);
            }
            if (routingKey == TestStateRoutingKey)
            {
                var motionTestStateDTO = GetDataFromGeneralQueue<MotionTestStateDTO>(bytesArray);
                UpdateMotionTestState(motionTestStateDTO);
            }
        }

        private async void FetchProfilesAsync()
        {
            var data = await AppMessageHandler.Request(FetchProfilesId, null);
            var profiles = GetDataFromRequestQueue<List<MotionProfileDTO>>(data);
            var newProfiles = new List<MotionProfileModel>();

            foreach (var profile in profiles)
            {
                var newProfile = new MotionProfileModel();
                newProfile.LoadDataFromDTOJson(profile);
                newProfiles.Add(newProfile);
            }

            ProfileModels = newProfiles;
        }

        private async void FetchMotionKinematicsAsync()
        {
            var data = await AppMessageHandler.Request(FetchMotionKinematics, null);
            var motionKinematicsDTO = GetDataFromRequestQueue<MotionKinematicsDTO>(data);
            UpdateMotionKinematics(motionKinematicsDTO);
        }

        private void UpdateMotionKinematics(MotionKinematicsDTO motionKinematicsDTO)
        {
            ActualVelocity = motionKinematicsDTO.Velocity;
            ActualAcceleration = motionKinematicsDTO.Acceleration;
            ActualJerk = motionKinematicsDTO.Jerk;
        }

        private void UpdateMotionTestState(MotionTestStateDTO motionTestStateDTO)
        {
            CurrentCycles = motionTestStateDTO.CurrentCycles;
            TotalCycles = motionTestStateDTO.TotalCycles;
            CommandPosition = motionTestStateDTO.CommandPosition;
            EncoderPosition = motionTestStateDTO.EncoderPosition;
            MissingSteps = motionTestStateDTO.MissingSteps;
        }

        private async void CalculateMotionTestAsync()
        {
            var data = await AppMessageHandler.Request(RequestCalculateId, GetMotionTestDTO());
            var motionTestCalculateDTO = GetDataFromRequestQueue<MotionCalculateDTO>(data);

            PointOneEstTime = motionTestCalculateDTO.PointTwoEstTime;
            PointTwoEstTime = motionTestCalculateDTO.PointTwoEstTime;
            TotalEstCycleTime = motionTestCalculateDTO.TotalEstCycleTime;
        }

        private T GetDataFromRequestQueue<T>(byte[] data)
        {
            var jsonString = StringHelper.ByteArrayToString(data);
            var message = JsonConvert.DeserializeObject<Message>(jsonString);
            return JsonConvert.DeserializeObject<T>((string)message.Data);
        }

        private T GetDataFromGeneralQueue<T>(byte[] data)
        {
            var jsonString = StringHelper.ByteArrayToString(data);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }

    public class Series
    {
        public double Id { get; set; }
        public double YValue { get; set; }
    }
}
