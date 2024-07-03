using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;
using ZWave.Controls;
using ZWave.Helpers;
using ZWave.Models;
using ZWave.Shared.Enums;
using Message = CommonLib.DTO.Implementation.Message;

namespace ZWave.ViewModels.Systems.CausewaySystem
{
    public partial class DonorLiftingModulePageViewModel : BaseViewModel
    {
        protected string Topic => Master.System.CausewaySystem.DonorLiftingModule.BasePath + "#";
        private string FetchDataId => Master.System.CausewaySystem.DonorLiftingModule.Fetch;
        private string ApplyContentId => Master.System.CausewaySystem.DonorLiftingModule.Content;
        private string MoveId => Master.System.CausewaySystem.DonorLiftingModule.Move;
        private string ActionId => Master.System.CausewaySystem.DonorLiftingModule.Action;
        private string CameraChangeId => Master.System.CausewaySystem.DonorLiftingModule.CameraChange;
        private string DonorChuckVacuumOutputId => Master.System.CausewaySystem.DonorLiftingModule.DonorChuckVacuumOutput;
        //private string VisionRoutingKey => AppMessageHandler.StreamVisionRoutingKey;

        [ObservableProperty]
        DonorLiftingModuleModel _donorLiftingModuleModel;

        [ObservableProperty]
        private IList<DonorLiftingModuleConfigurationModel> _donorLiftingModuleConfigurationModel;

        [ObservableProperty]
        private View _childView;

        [ObservableProperty]
        private CameraModel _cameraModel;

        [ObservableProperty]
        private bool _loadCompleted = false;

        public DonorLiftingModulePageViewModel()
        {
            DonorLiftingModuleModel = new DonorLiftingModuleModel();
            DonorLiftingModuleModel.RequestUpdate += (sender, args) => ChuckVacuumChangeCommand();

            CameraModel = new CameraModel();

            LoadConfigurationData();
        }

        private void ChuckVacuumChangeCommand()
        {
            var donorLiftingModuleDTO = new DonorLiftingModuleDTO()
            {
                IsDonorChuckVacuumEnabled = DonorLiftingModuleModel.IsDonorChuckVacuumEnabled,
            };
            AppMessageHandler.PublishToMachine(DonorChuckVacuumOutputId, donorLiftingModuleDTO);
        }

        private async void LoadConfigurationData()
        {
            await ConfigurationService.GetConfig_DonorLifting();
            DonorLiftingModuleConfigurationModel = ConfigurationService.DonorLiftingModuleConfigurationModels.ToList();
            LoadCompleted = true;
        }


        [RelayCommand]
        void Loaded()
        {
            //FetchData();
            AppMessageHandler.SubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived += OnMessageHandlerGeneralMessageReceived;

            //AppMessageHandler.SubcribeStream(VisionRoutingKey);
            //AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;

            AppMessageHandler.SubcribeStream();
            AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;
        }

        [RelayCommand]
        void Unloaded()
        {
            AppMessageHandler.UnsubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived -= OnMessageHandlerGeneralMessageReceived;

            //AppMessageHandler.UnsubcribeStream(VisionRoutingKey);
            //AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;

            AppMessageHandler.UnsubcribeStream();
            AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;
        }

        //private void OnStreamMessageReceived(object sender, byte[] bytesArray, string routingKey)
        //{
        //    //if (routingKey == VisionRoutingKey)
        //    //{
        //        CameraModel.ImageBytes = bytesArray;
        //        SetupLiveCamera();
        //    //}
        //}

        private static bool _isLoading = false;
        private void OnStreamMessageReceived(object _, byte[] bytesArray)
        {
            if (_isLoading)
            {
                //SKIP
                return;
            }
            //RECEIVED

            _isLoading = true;

            var jsonString = StringHelper.ByteArrayToString(bytesArray);
            var imageStreamDTO = JsonConvert.DeserializeObject<ImageStreamDTO>(jsonString);

            CameraModel.ImageBytes = imageStreamDTO.ImageBytes;

            //FullImageWidth = imageStreamDTO.FullImageWidth;
            //FullImageHeight = imageStreamDTO.FullImageHeight;

            //Ratio = imageStreamDTO.Ratio;

            //ScrollX = imageStreamDTO.ScrollX;
            //ScrollY = imageStreamDTO.ScrollY;
            //ImageTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(imageStreamDTO.Timestamp).DateTime.ToLocalTime();

            SetupLiveCamera();
            _isLoading = false;
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

        private async void FetchData()
        {
            LogService.LogUser(LogAction.Fetching, "Fetch Donor Lifting Data");

            var response = await AppMessageHandler.Request(FetchDataId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);

            DonorLiftingModuleModel.LoadDataFromDTOJson(message.Data);
            DonorLiftingModuleModel.SetIsDonorChuckVacuumOutputEnabled(DonorLiftingModuleModel.IsDonorChuckVacuumEnabled);

            LogService.LogUser(LogAction.Fetching, $"Receive Donor Lifting Data: {message.Data}");
        }

        [RelayCommand]
        void Move()
        {
            var donorLiftingModuleDTO = new DonorLiftingModuleDTO()
            {
                DonorLifterUIElement = DonorLiftingModuleModel.DonorLifterUIElement,
                MoveDirection = DonorLiftingModuleModel.MoveDirection,
                MotionCmd = DonorLiftingModuleModel.MotionCmd,
                MoveRel = DonorLiftingModuleModel.MoveRel,
            };

            AppMessageHandler.PublishToMachine(MoveId, donorLiftingModuleDTO);
        }

        [RelayCommand]
        void Home()
        {
            var donorLiftingModuleDTO = new DonorLiftingModuleDTO()
            {
                DonorLifterUIElement = DonorLiftingModuleModel.DonorLifterUIElement,
                MotionCmd = DonorLiftingModuleModel.MotionCmd,
            };

            AppMessageHandler.PublishToMachine(ActionId, donorLiftingModuleDTO);
        }

        [RelayCommand]
        void HomeAll()
        {
            var donorLiftingModuleDTO = new DonorLiftingModuleDTO()
            {
                DonorLifterUIElement = DonorLifterUIElement.Donor_Home_All,
            };

            AppMessageHandler.PublishToMachine(ActionId, donorLiftingModuleDTO);
        }

        [RelayCommand]
        void LoadingUnloading()
        {
            var donorLiftingModuleDTO = new DonorLiftingModuleDTO()
            {
                DonorLifterUIElement = DonorLiftingModuleModel.DonorLifterUIElement,
            };

            AppMessageHandler.PublishToMachine(ActionId, donorLiftingModuleDTO);
        }

        [RelayCommand]
        void CameraChange()
        {
            var donorLiftingModuleDTO = new DonorLiftingModuleDTO()
            {
                DonorLifterUIElement = DonorLiftingModuleModel.DonorLifterUIElement,
                CameraSelect = DonorLiftingModuleModel.CameraSelect,
            };

            AppMessageHandler.PublishToMachine(CameraChangeId, donorLiftingModuleDTO);
        }

        private void OnMessageHandlerGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == ApplyContentId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var donorLiftingModuleDTO = JsonConvert.DeserializeObject<DonorLiftingModuleDTO>(stringData);

                DonorLiftingModuleModel.XAxisPosition = donorLiftingModuleDTO.XAxisPosition;
                DonorLiftingModuleModel.YAxisPosition = donorLiftingModuleDTO.YAxisPosition;
                DonorLiftingModuleModel.ZAxisPosition = donorLiftingModuleDTO.ZAxisPosition;

                if (DonorLiftingModuleModel.IsDonorChuckVacuumEnabled != donorLiftingModuleDTO.IsDonorChuckVacuumEnabled)
                    DonorLiftingModuleModel.IsTempDonorChuckVacuumEnabled = donorLiftingModuleDTO.IsDonorChuckVacuumEnabled;
            }
            else if (routingKey == DonorChuckVacuumOutputId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                bool bVacc = bool.Parse(stringData);
                if (DonorLiftingModuleModel.IsDonorChuckVacuumEnabled != bVacc)
                    DonorLiftingModuleModel.IsTempDonorChuckVacuumEnabled = bVacc;
            }
        }
    }
}
