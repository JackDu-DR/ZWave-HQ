namespace ZWave.Models
{
    public class CameraModel : BaseModel
    {
        private System.Timers.Timer typingTimer;
        private string ExposureId => Master.System.Vision.Camera.Exposure;
        private string LightId => Master.System.Vision.Camera.Light;

        private string _cameraId;
        public string CameraId
        {
            get => _cameraId;
            set => SetProperty(ref _cameraId, value);
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get => _imageBytes; 
            set => SetProperty(ref _imageBytes, value);
        }

        private double _exposure = 0;
        public double Exposure
        {
            get => _exposure;
            set
            {
                if (SetProperty(ref _exposure, value))
                {
                    StartTimer();
                };
            }
        }

        public void SetExposure(double value)
        {
            _exposure = value;
            OnPropertyChanged(nameof(Exposure));
        }

        private bool _light;
        public bool Light
        {
            get => _light;
            set
            {
                if (SetProperty(ref _light, value) && !string.IsNullOrEmpty(CameraId))
                {
                    AppMessageHandler.PublishToMachine(LightId, value);
                }
            }
        }

        public void SetLight(bool value)
        {
            _light = value;
            OnPropertyChanged(nameof(Light));
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
        }

        private void StartTimer()
        {
            if (typingTimer == null)
            {
                typingTimer = new System.Timers.Timer(500); // 500 milliseconds delay
                typingTimer.AutoReset = false;
                typingTimer.Elapsed += (sender, e) =>
                {
                    typingTimer.Stop();
                    Task.Run(() =>
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            if (CameraId != null)
                            {
                                AppMessageHandler.PublishToMachine(ExposureId, _exposure);
                            }
                        });
                    });
                };
            }
            else
            {
                typingTimer.Stop();
            }

            typingTimer.Start();
        }
    }
}
