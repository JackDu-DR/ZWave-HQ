using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ZWave.ViewModels.Jobs
{
    public enum CameraOption
    {
        All = 0,
        Camera1 = 1,
        Camera2 = 2,
        Camera3 = 3,
    }

    public partial class JobMainViewModel : BaseViewModel
    {
        [ObservableProperty]
        LaserStatusRange _laserConnectionStatus;
        [ObservableProperty]
        string _laserConnectionValue;

        [ObservableProperty]
        LaserStatusRange _operationalStatus;
        [ObservableProperty]
        string _operationalValue;

        [ObservableProperty]
        LaserStatusRange _emissionStatus;
        [ObservableProperty]
        string _emissionValue;

        [ObservableProperty]
        LaserStatusRange _powerEnergyStatus;
        [ObservableProperty]
        string _powerEnergyValue;

        [ObservableProperty]
        LaserStatusRange _frequencyPulseDividerStatus;
        [ObservableProperty]
        string _frequencyPulseDividerValue;

        [ObservableProperty]
        LaserStatusRange _waveLengthStatus;
        [ObservableProperty]
        string _waveLengthValue;

        [ObservableProperty]
        List<string> _cameraOptions;

        [ObservableProperty]
        string _selectedCameraOption;
        partial void OnSelectedCameraOptionChanged(string value)
        {
            SubcribeCameraStream();
        }

        [ObservableProperty]
        byte[] _camera1Sources;

        [ObservableProperty]
        byte[] _camera2Sources;

        [ObservableProperty]
        byte[] _camera3Sources;

        [RelayCommand]
        void Connect()
        {
            LogService.LogUser(Shared.Enums.LogAction.UserActivity, $"Clicked - Connect button");
        }

        [RelayCommand]
        void Standby()
        {
            LogService.LogUser(Shared.Enums.LogAction.UserActivity, $"Clicked - Standby button");
        }

        [RelayCommand]
        async void Loaded()
        {
            await Task.Delay(5000);
            SubcribeCameraStream();
        }

        [RelayCommand]
        void Unloaded()
        {
            UnsubcribeCameraStream();
        }

        public JobMainViewModel()
        {
            _cameraOptions = new List<string>(Enum.GetNames(typeof(CameraOption)));
            _selectedCameraOption = _cameraOptions[0];

            //TODO: remove dummy data
            _laserConnectionStatus = LaserStatusRange.OutOfRange;
            _operationalStatus = LaserStatusRange.WithinRange;
            _emissionStatus = LaserStatusRange.Warning;
            _powerEnergyStatus = LaserStatusRange.Unknown;
            _frequencyPulseDividerStatus = LaserStatusRange.WithinRange;
            _waveLengthStatus = LaserStatusRange.WithinRange;

            _laserConnectionValue = "Disconnected";
            _operationalValue = "Operational";
            _emissionValue = "No emission";
            _powerEnergyValue = "80.0390W - 2000.6";
            _frequencyPulseDividerValue = "40.0kHz - 40.0/1";
            _waveLengthValue = "1030 nm";
        }

        private void SubcribeCameraStream()
        {
            UnsubcribeCameraStream();

            var cameraOption = Enum.Parse<CameraOption>(SelectedCameraOption);
            if (cameraOption == CameraOption.All || cameraOption == CameraOption.Camera1)
            {
                AppMessageHandler.SubcribeCamera1Stream();
                AppMessageHandler.OnSubcribeCamera1StreamMessageReceived += OnSubcribeCamera1StreamMessageReceived;
            }
            if (cameraOption == CameraOption.All || cameraOption == CameraOption.Camera2)
            {
                AppMessageHandler.SubcribeCamera2Stream();
                AppMessageHandler.OnSubcribeCamera2StreamMessageReceived += OnSubcribeCamera2StreamMessageReceived;
            }
            if (cameraOption == CameraOption.All || cameraOption == CameraOption.Camera3)
            {
                AppMessageHandler.SubcribeCamera3Stream();
                AppMessageHandler.OnSubcribeCamera3StreamMessageReceived += OnSubcribeCamera3StreamMessageReceived;
            }
        }
        private async void UnsubcribeCameraStream()
        {
            AppMessageHandler.OnSubcribeCamera1StreamMessageReceived -= OnSubcribeCamera1StreamMessageReceived;
            AppMessageHandler.OnSubcribeCamera2StreamMessageReceived -= OnSubcribeCamera2StreamMessageReceived;
            AppMessageHandler.OnSubcribeCamera3StreamMessageReceived -= OnSubcribeCamera3StreamMessageReceived;

            await Task.Run(() =>
            {
                AppMessageHandler.UnsubcribeCamera1Stream();
                AppMessageHandler.UnsubcribeCamera2Stream();
                AppMessageHandler.UnsubcribeCamera3Stream();
            });
        }

        private void OnSubcribeCamera1StreamMessageReceived(object _, byte[] bytesArray)
        {
            if (_is1Running)
            {
                return;
            }

            _is1Running = true;
            Camera1Sources = bytesArray;
            _is1Running = false;
        }

        private static bool _is1Running;
        private static bool _is2Running;
        private static bool _is3Running;
        private void OnSubcribeCamera2StreamMessageReceived(object _, byte[] bytesArray)
        {
            if (_is2Running)
            {
                return;
            }

            _is2Running = true;
            Camera2Sources = bytesArray;
            _is2Running = false;
        }

        private void OnSubcribeCamera3StreamMessageReceived(object _, byte[] bytesArray)
        {
            if (_is3Running)
            {
                return;
            }

            _is3Running = true;
            Camera3Sources = bytesArray;
            _is3Running = false;
        }
    }
}
