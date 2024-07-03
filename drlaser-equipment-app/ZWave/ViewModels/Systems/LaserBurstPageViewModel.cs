using CommonLib.DTO.Implementation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;
using ZWave.Helpers;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Systems
{
    public partial class LaserBurstPageViewModel : BaseViewModel
    {
        private string Topic => Master.System.Laser.LaserBurst.BasePath + "#";
        private string ApplyContentRoutingKey => Master.System.Laser.LaserBurst.Content;
        private string PowerlockRoutingKey => Master.System.Laser.LaserBurst.Powerlock;

        private string ApplyId => Master.System.Laser.LaserBurst.Apply;
        private string FetchDataId => Master.System.Laser.LaserBurst.Fetch;

        private bool _isPowerlockEnabled;
        public bool IsPowerlockEnabled
        {
            get => _isPowerlockEnabled;
            set
            {
                if (SetProperty(ref _isPowerlockEnabled, value))
                {
                    AppMessageHandler.PublishToMachine(PowerlockRoutingKey, value);

                    LogService.LogUser(LogAction.Publish, $"Applying IsPowerlockEnabled {IsPowerlockEnabled}");
                }
            }
        }

        [ObservableProperty]
        public double _actualP;

        [ObservableProperty]
        public double _p;

        [ObservableProperty]
        public double _actualN;

        [ObservableProperty]
        public double _n;

        [ObservableProperty]
        public double _actualEnvelopeControlP;

        [ObservableProperty]
        public double _envelopeControlP;

        [ObservableProperty]
        public double _actualEnvelopeControlN;

        [ObservableProperty]
        public double _envelopeControlN;

        [RelayCommand]
        void Apply()
        {
            var laserApplyDTO = new LaserBurstApplyDTO()
            {
                P = P,
                N = N,
                EnvelopeControlP = EnvelopeControlP,
                EnvelopeControlN = EnvelopeControlN,
            };

            AppMessageHandler.PublishToMachine(ApplyId, laserApplyDTO);

            LogService.LogUser(LogAction.UserActivity, $"Laser Burst Page Clicked - Apply button");
            LogService.LogUser(LogAction.Publish, $"Applying LaserBurst {JsonConvert.SerializeObject(laserApplyDTO)}");
        }

        [RelayCommand]
        void Loaded()
        {
            LogService.LogUser(LogAction.UserActivity, "System Page Navigate - Laser Burst sub tab page");
            FetchData();
            AppMessageHandler.SubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived += OnMessageHandlerGeneralMessageReceived;
        }

        [RelayCommand]
        void Unloaded()
        {
            AppMessageHandler.UnsubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived -= OnMessageHandlerGeneralMessageReceived;
        }

        private async void FetchData()
        {
            LogService.LogUser(LogAction.Fetching, "Fetch LaserBurst Data");
            var response = await AppMessageHandler.Request(FetchDataId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);

            LoadDataFromDTOJson(message.Data);
            LogService.LogUser(LogAction.Fetching, $"Receive LaserBurst Data: {message.Data}");
        }

        private void OnMessageHandlerGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == ApplyContentRoutingKey)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var laserBurstApplyDTO = JsonConvert.DeserializeObject<LaserBurstApplyDTO>(stringData);

                ActualP = P = laserBurstApplyDTO.P;
                ActualN = N = laserBurstApplyDTO.N;
                ActualEnvelopeControlP = EnvelopeControlP = laserBurstApplyDTO.EnvelopeControlP;
                ActualEnvelopeControlN = EnvelopeControlN = laserBurstApplyDTO.EnvelopeControlN;
            }
            else if (routingKey == PowerlockRoutingKey)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                SetIsPowerlockEnabled(bool.Parse(stringData));
            }
        }

        public void SetIsPowerlockEnabled(bool isOutputEnabled)
        {
            _isPowerlockEnabled = isOutputEnabled;
            OnPropertyChanged(nameof(IsPowerlockEnabled));
        }

        public void LoadDataFromDTOJson(object baseDTO)
        {
            var laserBurstDTO = JsonConvert.DeserializeObject<LaserBurstDTO>(baseDTO.ToString());

            SetIsPowerlockEnabled(laserBurstDTO.IsPowerlockEnabled);
            ActualP = P = laserBurstDTO.P;
            ActualN = N = laserBurstDTO.N;
            ActualEnvelopeControlP = EnvelopeControlP = laserBurstDTO.EnvelopeControlN;
            ActualEnvelopeControlN = EnvelopeControlN = laserBurstDTO.EnvelopeControlN;
        }
    }
}
