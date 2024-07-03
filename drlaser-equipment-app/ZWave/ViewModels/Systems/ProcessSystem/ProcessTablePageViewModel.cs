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
using System.Collections.Concurrent;
using System.Diagnostics;
using CommonLib.DTO.Interface;

namespace ZWave.ViewModels.Systems.ProcessSystem
{
    public partial class ProcessTablePageViewModel : BaseViewModel
    {
        protected string Topic => Master.System.ProcessSystem.ProcessTable.BasePath + "#";
        private string FetchDataId => Master.System.ProcessSystem.ProcessTable.Fetch;
        private string ApplyContentId => Master.System.ProcessSystem.ProcessTable.Content;
        private string MoveId => Master.System.ProcessSystem.ProcessTable.Move;
        private string ActionId => Master.System.ProcessSystem.ProcessTable.Action;
        private string CameraChangeId => Master.System.ProcessSystem.ProcessTable.CameraChange;
        private string SubstrateChuckVacuumOutputId => Master.System.ProcessSystem.ProcessTable.SubstrateChuckVacuumOutput;

        //private string VisionRoutingKey => AppMessageHandler.StreamVisionRoutingKey;

        [ObservableProperty]
        ProcessTableModel _processTableModel;

        [ObservableProperty]
        private IList<ProcessTableConfigurationModel> _processTableConfigurationModel;

        [ObservableProperty]
        private View _childView;

        [ObservableProperty]
        private CameraModel _cameraModel;

        //private Thread _thread;
        //private bool _dipose;
        int _x = 0;
        private readonly ConcurrentDictionary<int, byte[]> _visionCallback = new ConcurrentDictionary<int, byte[]>();

        [ObservableProperty]
        private bool _loadCompleted = false;

        public ProcessTablePageViewModel()
        {
            ProcessTableModel = new ProcessTableModel();
            ProcessTableModel.RequestUpdate += (sender, args) => ChuckVacuumChangeCommand();
            CameraModel = new CameraModel();

            LoadConfigurationData();

            //_dipose = false;
            //_thread = new Thread(ThreadLoop);
            //_thread.Start();
        }

        private void ChuckVacuumChangeCommand()
        {
            if (ProcessTableModel.IsSubstrateChuckVacuumEnabled)
                ProcessTableModel.ProTableUIElement = ProTableUIElement.Pro_Table_Vacuum_Chuck_On;
            else
                ProcessTableModel.ProTableUIElement = ProTableUIElement.Pro_Table_Vacuum_Chuck_Off;

            var processTableDTO = new ProcessTableDTO()
            {
                ProTableUIElement = ProcessTableModel.ProTableUIElement,
                IsSubstrateChuckVacuumEnabled = ProcessTableModel.IsSubstrateChuckVacuumEnabled
            };
            AppMessageHandler.PublishToMachine(SubstrateChuckVacuumOutputId, processTableDTO);
        }

        //private void ThreadLoop()
        //{
        //    while (!_dipose)
        //    {
        //        byte[] bytesArray;
        //        bool result = _visionCallback.TryRemove(_x, out bytesArray);
        //        if (result)
        //        {
        //            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ProsTable Image - deque : " + _x.ToString());
        //            CameraModel.ImageBytes = bytesArray;
        //            SetupLiveCamera();
        //            _x++;

        //        }
        //        Thread.Sleep(90);

        //    }

        //}

        [RelayCommand]
        void Loaded()
        {

            //FetchData();
            AppMessageHandler.SubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived += OnMessageHandlerGeneralMessageReceived;

            //_imageRecCount = 0;
            //_n = 0;
            //_x = 0;
            //_goOfflive = false;
            //AppMessageHandler.SubcribeStream(VisionRoutingKey);
            //AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;

            AppMessageHandler.SubcribeStream();
            AppMessageHandler.OnStreamMessageReceived += OnStreamMessageReceived;
        }
        //bool _goOfflive = false;

        private async void LoadConfigurationData()
        {
            await ConfigurationService.GetConfig_ProcessTable();
            ProcessTableConfigurationModel = ConfigurationService.ProcessTableConfigurationModels.ToList();
            LoadCompleted = true;
        }

        [RelayCommand]
        void Unloaded()
        {
            AppMessageHandler.UnsubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived -= OnMessageHandlerGeneralMessageReceived;

            //_goOfflive = true;
            //AppMessageHandler.UnsubcribeStream(VisionRoutingKey);
            //AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;

            AppMessageHandler.UnsubcribeStream();
            AppMessageHandler.OnStreamMessageReceived -= OnStreamMessageReceived;

            //_generalQueueSubcribed = false;
        }

        //int _imageRecCount = 0;
        //int _n = 0;
        //private void OnStreamMessageReceived(object sender, byte[] bytesArray, string routingKey)
        //{
        //    if (routingKey == VisionRoutingKey)
        //    {
        //        _imageRecCount++;
        //        LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ProsTable Image - event count : " + _imageRecCount.ToString());
        //    }
        //    //if (_goOfflive)
        //    //    return;

        //    LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ProsTable Stream Received ");
        //    if (routingKey == VisionRoutingKey)
        //    {
        //        //_imageRecCount++;
        //        //LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ProsTable Image - event count : " + _imageRecCount.ToString());

        //        if ((_visionCallback.Count() == 0) || (_visionCallback.Count() == 1) || (_visionCallback.Count() == 2))
        //        {
        //            bool result = _visionCallback.TryAdd(_n, bytesArray);
        //            if (result)
        //            {
        //                LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ProsTable Image - queue : " + _n.ToString());
        //                _n++;
        //            }
        //            else
        //                LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ProsTable Dict add fail : " + _imageRecCount.ToString());
        //        }
        //        else
        //        {
        //            LogService.LogUser(ZWave.Shared.Enums.LogAction.Connect, "ProsTable Dict not add : " + _imageRecCount.ToString());
        //        }
        //    }
        //}

        //private void OnStreamMessageReceived(object sender, byte[] bytesArray, string routingKey)
        //{
        //    //if (routingKey == VisionRoutingKey)
        //    //{
        //    //    CameraModel.ImageBytes = bytesArray;
        //    //    SetupLiveCamera();
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
            LogService.LogUser(LogAction.Fetching, "Fetch Process Table Data");

            var response = await AppMessageHandler.Request(FetchDataId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);

            ProcessTableModel.LoadDataFromDTOJson(message.Data);

            LogService.LogUser(LogAction.Fetching, $"Receive Process Table Data: {message.Data}");
        }

        [RelayCommand]
        void Move()
        {
            var processTableDTO = new ProcessTableDTO()
            {
                ProTableUIElement = ProcessTableModel.ProTableUIElement,
                MoveDirection = ProcessTableModel.MoveDirection,
                MotionCmd = ProcessTableModel.MotionCmd,
                MoveRel = ProcessTableModel.MoveRel,
            };

            AppMessageHandler.PublishToMachine(MoveId, processTableDTO);
        }

        [RelayCommand]
        void Home()
        {
            var processTableDTO = new ProcessTableDTO()
            {
                ProTableUIElement = ProcessTableModel.ProTableUIElement,
                MotionCmd = ProcessTableModel.MotionCmd,
            };

            AppMessageHandler.PublishToMachine(ActionId, processTableDTO);
        }

        [RelayCommand]
        void HomeAll()
        {
            ProcessTableModel.ProTableUIElement = ProTableUIElement.Pro_Table_Home_All;
            var processTableDTO = new ProcessTableDTO()
            {
                ProTableUIElement = ProcessTableModel.ProTableUIElement,
            };

            AppMessageHandler.PublishToMachine(ActionId, processTableDTO);
        }

        [RelayCommand]
        void LoadingUnloading()
        {
            var processTableDTO = new ProcessTableDTO()
            {
                ProTableUIElement = ProcessTableModel.ProTableUIElement,
            };

            AppMessageHandler.PublishToMachine(ActionId, processTableDTO);
        }

        [RelayCommand]
        void CameraChange()
        {
            var processTableDTO = new ProcessTableDTO()
            {
                ProTableUIElement = ProcessTableModel.ProTableUIElement,
                CameraSelect = ProcessTableModel.CameraSelect,
            };

            AppMessageHandler.PublishToMachine(CameraChangeId, processTableDTO);
        }

        private void OnMessageHandlerGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == ApplyContentId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var processTableDTO = JsonConvert.DeserializeObject<ProcessTableDTO>(stringData);

                ProcessTableModel.XAxisPosition = processTableDTO.XAxisPosition;
                ProcessTableModel.YAxisPosition = processTableDTO.YAxisPosition;
                ProcessTableModel.TXAxisPosition = processTableDTO.TXAxisPosition;
                ProcessTableModel.TYAxisPosition = processTableDTO.TYAxisPosition;
                ProcessTableModel.ZAxisPosition = processTableDTO.ZAxisPosition;

                if (ProcessTableModel.IsSubstrateChuckVacuumEnabled != processTableDTO.IsSubstrateChuckVacuumEnabled)
                    ProcessTableModel.IsTempSubstrateChuckVacuumEnabled = processTableDTO.IsSubstrateChuckVacuumEnabled;;
            }
            else if (routingKey == SubstrateChuckVacuumOutputId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                bool bVacc = bool.Parse(stringData);
                if (ProcessTableModel.IsSubstrateChuckVacuumEnabled != bVacc)
                    ProcessTableModel.IsTempSubstrateChuckVacuumEnabled = bVacc;
            }
        }
    }
}
