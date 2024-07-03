using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;
using ZWave.Helpers;
using Message = CommonLib.DTO.Implementation.Message;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Systems
{
    public partial class SystemLaserPageViewModel : BaseViewModel
    {
        private readonly LaserBasicPageViewModel _laserBasicPageViewModel = Helpers.ServiceProvider.GetService<LaserBasicPageViewModel>();

        private string Topic => Master.System.Laser.Info + ".#";
        private string LaserInfoRoutingKey => Master.System.Laser.Info;

        private string FetchDataId => Master.System.Laser.Fetch;

        [ObservableProperty]
        bool _laserConnectionStatus;
        [ObservableProperty]
        string _laserConnectionValue;

        [ObservableProperty]
        LaserOperation _operationalStatus;
        [ObservableProperty]
        string _operationalValue;

        [ObservableProperty]
        bool _emissionStatus;
        [ObservableProperty]
        string _emissionValue;

        [ObservableProperty]
        LaserStatusRange _laserPowerRange;
        [ObservableProperty]
        string _powerEnergyValue;

        [ObservableProperty]
        LaserStatusRange _frequencyRange;
        [ObservableProperty]
        string _frequencyPulseDividerValue;

        [ObservableProperty]
        LaserStatusRange _waveLengthRange;
        [ObservableProperty]
        string _waveLengthValue;

        [RelayCommand]
        void Connect()
        {
            _laserBasicPageViewModel.ConnectCommand();
        }

        [RelayCommand]
        void Standby()
        {
            _laserBasicPageViewModel.StandbyCommand();
        }

        public SystemLaserPageViewModel()
        {
            Localization.PropertyChanged += OnLocalizationPropertyChanged;
        }

        private void OnLocalizationPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateStatusTexts();
        }

        private void UpdateStatusTexts()
        {
            LaserConnectionValue = LaserConnectionStatus ? Localization["Connected"].ToString() : Localization["Disconnected"].ToString();

            switch (OperationalStatus)
            {
                case LaserOperation.Operation:
                    OperationalValue = Localization["Operational"].ToString();
                    break;
                case LaserOperation.Standby:
                    OperationalValue = Localization["Standby"].ToString();
                    break;
                case LaserOperation.Housekeeping:
                    OperationalValue = Localization["HouseKeeping"].ToString();
                    break;
            }

            EmissionValue = EmissionStatus ? Localization["Emission"].ToString() : Localization["NoEmission"].ToString();
        }


        [RelayCommand]
        void Loaded()
        {
            LogService.LogUser(LogAction.UserActivity, "System Page Navigate - Laser tab page");
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

        private void UpdateStatusValues(LaserInfoDTO laserInfoDTO)
        {
            LaserConnectionStatus = laserInfoDTO.IsConnected;
            OperationalStatus = laserInfoDTO.Operation;
            EmissionStatus = laserInfoDTO.Emission;

            PowerEnergyValue = Math.Round(laserInfoDTO.LaserPower, 2) + "W - " + Math.Round(laserInfoDTO.Energy, 2) + "µJ";
            FrequencyPulseDividerValue = Math.Round(laserInfoDTO.Frequency, 2) + "kHz - " + Math.Round(laserInfoDTO.Frequency, 2) + "/" + Math.Round(laserInfoDTO.PulseDivider, 2);
            WaveLengthValue = laserInfoDTO.WaveLength + "nm";
        }

        private void UpdateStatusRange(LaserInfoDTO laserInfoDTO)
        {
            LaserPowerRange = laserInfoDTO.LaserPowerRange;
            FrequencyRange = laserInfoDTO.FrequencyRange;
            WaveLengthRange = laserInfoDTO.WaveLengthRange;
        }

        private async void FetchData()
        {
            var response = await AppMessageHandler.Request(FetchDataId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);

            var laserInfoDTO = JsonConvert.DeserializeObject<LaserInfoDTO>(message.Data.ToString());
            UpdateStatusValues(laserInfoDTO);
            UpdateStatusRange(laserInfoDTO);
            UpdateStatusTexts();
        }

        private void OnMessageHandlerGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == LaserInfoRoutingKey)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var laserInfoDTO = JsonConvert.DeserializeObject<LaserInfoDTO>(stringData);

                UpdateStatusValues(laserInfoDTO);
                UpdateStatusRange(laserInfoDTO);
                UpdateStatusTexts();
            }
        }
    }
}
