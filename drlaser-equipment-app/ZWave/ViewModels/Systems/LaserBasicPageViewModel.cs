using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;
using ZWave.Helpers;
using ZWave.Models;
using ZWave.Shared.Enums;
using Message = CommonLib.DTO.Implementation.Message;

namespace ZWave.ViewModels.Systems
{
    public partial class LaserBasicPageViewModel : BaseViewModel
    {
        private string Topic => Master.System.Laser.LaserBasic.BasePath + "#";
        private string OutputRoutingKey => Master.System.Laser.LaserBasic.Output;
        private string ApplyContentRoutingKey => Master.System.Laser.LaserBasic.Content;

        private string ApplyId => Master.System.Laser.LaserBasic.Apply;
        private string ConnectId => Master.System.Laser.LaserBasic.Connect;
        private string StandbyId => Master.System.Laser.LaserBasic.StandBy;
        private string FetchDataId => Master.System.Laser.LaserBasic.Fetch;

        [ObservableProperty]
        LaserBasicModel _laserBasicModel;

        [ObservableProperty]
        List<string> _presetItemsSource;

        public LaserBasicPageViewModel()
        {
            //https://sioux.atlassian.net/browse/DRL-212
            _presetItemsSource = new List<string>() { "0", "1", "2" };
            LaserBasicModel = new LaserBasicModel();
        }

        [RelayCommand]
        void Apply()
        {
            var laserApplyDTO = new LaserApplyDTO()
            {
                AttenuatorPercentage = LaserBasicModel.AttenuatorPercentage,
                PPDivider = LaserBasicModel.PPDivider,
                PulseDuration = LaserBasicModel.PulseDuration,
                PresetControl = (PresetControl)Enum.Parse(typeof(PresetControl), LaserBasicModel.PresetControl)
            };

            LogService.LogUser(LogAction.UserActivity, $"Laser Basic Page Clicked - Apply button");
            LogService.LogUser(LogAction.Publish, $"Applying LaserBasic {JsonConvert.SerializeObject(laserApplyDTO)}");

            AppMessageHandler.PublishToMachine(ApplyId, laserApplyDTO);
        }

        public void ConnectCommand()
        {
            AppMessageHandler.PublishToMachine(ConnectId, null);

            LogService.LogUser(LogAction.UserActivity, $"Laser Basic Page Clicked - Connect button");
            LogService.LogUser(LogAction.Publish, $"PublishToMachine LaserBasic Connect command");
        }

        public void StandbyCommand()
        {
            AppMessageHandler.PublishToMachine(StandbyId, null);

            LogService.LogUser(LogAction.UserActivity, $"Laser Basic Page Clicked - Standby button");
            LogService.LogUser(LogAction.Publish, $"PublishToMachine LaserBasic StandBy command");
        }

        [RelayCommand]
        void Loaded()
        {
            LogService.LogUser(LogAction.UserActivity, "System Page Navigate - Laser Basic sub tab page");
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
            LogService.LogUser(LogAction.Fetching, "Fetch LaserBasic Data");

            var response = await AppMessageHandler.Request(FetchDataId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);

            LaserBasicModel.LoadDataFromDTOJson(message.Data);

            LogService.LogUser(LogAction.Fetching, $"Receive LaserBasic Data: {message.Data}");
        }

        private void OnMessageHandlerGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == ApplyContentRoutingKey)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var laserApplyDTO = JsonConvert.DeserializeObject<LaserApplyDTO>(stringData);

                LaserBasicModel.ActualAttenuatorPercentage = LaserBasicModel.AttenuatorPercentage = laserApplyDTO.AttenuatorPercentage;
                LaserBasicModel.ActualPPDivider = LaserBasicModel.PPDivider = laserApplyDTO.PPDivider;
                LaserBasicModel.ActualPulseDuration = LaserBasicModel.PulseDuration = laserApplyDTO.PulseDuration;
                LaserBasicModel.ActualPresetControl = LaserBasicModel.PresetControl = ((int)laserApplyDTO.PresetControl).ToString();
            }
            else if (routingKey == OutputRoutingKey)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                LaserBasicModel.SetIsOutputEnabled(bool.Parse(stringData));
            }
        }
    }
}
