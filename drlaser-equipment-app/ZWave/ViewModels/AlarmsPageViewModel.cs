using CommonLib.DTO.Implementation;
using CommonLib.DTO.Interface;
using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ZWave.Helpers;
using ZWave.Models;
using ZWave.Services.Interfaces;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels
{
    public enum ButtonAlarmSeverity
    {
        All = 0,
        High = 1,
        Medium = 2,
        Low = 3,
    }
    public partial class AlarmsPageViewModel : BaseViewModel
    {
        private readonly IAlarmGuidelineService AlarmService = Helpers.ServiceProvider.GetService<IAlarmGuidelineService>();
        private string AlarmTopic => Master.Alarm.BasePath + "#";
        private string FetchId => Master.Alarm.Fetch;
        private string AcknowlegdedAlarmRoutingKey => Master.Alarm.AcknowlegedAlarm;
        private string RetriedAlarmRoutingKey => Master.Alarm.RetriedAlarm;
        private string CancelledAlarmRoutingKey => Master.Alarm.CancelledAlarm;
        private string NewAlarmRoutingKey => Master.Alarm.New;
        private string AcknowledgedId => Master.Alarm.Ack;
        private string RetryId => Master.Alarm.Retry;
        private string CancelId => Master.Alarm.Cancel;

        private IEnumerable<AlarmGuidelineModel> _alarmGuideModels;
        private List<AlarmModel> _alarmsModels;

        [ObservableProperty]
        private IEnumerable<AlarmModel> _alarmsViewModels;

        private ButtonAlarmSeverity _selectedSeverity;
        public ButtonAlarmSeverity SelectedServerity
        {
            get => _selectedSeverity;
            set
            {
                if (SetProperty(ref  _selectedSeverity, value))
                {
                    LogService.LogUser(LogAction.UserActivity, $"Clicked - {value} Severity Button");
                    AlarmsViewModels = UpdatedAlarmList(_selectedSeverity, _alarmsModels);
                }
            }
        }

        [ObservableProperty]
        private AlarmModel _selectedAlarm;

        [RelayCommand]
        void SeverityClicked(string severityNumberStr)
        {
            SelectedServerity = (ButtonAlarmSeverity)Enum.Parse(typeof(ButtonAlarmSeverity), severityNumberStr);
        }

        [RelayCommand]
        void Acknowledge(string id)
        {
            LogService.LogUser(LogAction.UserActivity, $"Clicked - Acknowledge Button");
            LogService.LogUser(LogAction.Publish, $"Publish - Acknowledge Alarm Id {id}");
            AppMessageHandler.PublishToMachine(AcknowledgedId, id);
        }

        [RelayCommand]
        void Retry(string id)
        {
            LogService.LogUser(LogAction.UserActivity, "Clicked - Retry Button");
            LogService.LogUser(LogAction.Publish, $"Publish - Retry Alarm Id {id}");
            AppMessageHandler.PublishToMachine(RetryId, id);
        }

        [RelayCommand]
        void Cancel(string id)
        {
            LogService.LogUser(LogAction.UserActivity, $"Clicked - Cancel Button");
            LogService.LogUser(LogAction.Publish, $"Publish - Cancel Alarm Id {id}");
            AppMessageHandler.PublishToMachine(CancelId, id);
        }

        [RelayCommand]
        void Refresh()
        {
            FetchAlarms();
        }

        [RelayCommand]
        void Loaded()
        {
            FetchAlarms();
            AppMessageHandler.SubcribeGeneral(AlarmTopic);
            AppMessageHandler.OnGeneralMessageReceived += OnGeneralMessageReceived;
        }

        [RelayCommand]
        void Unloaded()
        {
            InitData();
            AppMessageHandler.UnsubcribeGeneral(AlarmTopic);
            AppMessageHandler.OnGeneralMessageReceived -= OnGeneralMessageReceived;
        }

        public AlarmsPageViewModel()
        {
            InitData();
        }

        private void InitData()
        {
            _alarmsModels = [];
            _alarmGuideModels = [];
            AlarmsViewModels = [];
            SelectedAlarm = new AlarmModel();
            SelectedServerity = ButtonAlarmSeverity.All;
        }

        private async void FetchAlarms()
        {
            _alarmGuideModels = await AlarmService.GetAlarmGuides();
            var response = await AppMessageHandler.Request(FetchId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);
            var alarmDtos = JsonConvert.DeserializeObject<IEnumerable<AlarmDTO>>(message.Data.ToString());
            _alarmsModels ??= [];
            LogService.LogUser(LogAction.Fetching, $"Fetch - {alarmDtos.Count()} Alarms");

            foreach (var alarmDto in alarmDtos)
            {
                _alarmsModels.Add(CreateNewAlarm(alarmDto, _alarmGuideModels));
            }
            AlarmsViewModels = UpdatedAlarmList(_selectedSeverity, _alarmsModels);
        }

        private void OnGeneralMessageReceived(object sender, byte[] bytesArray, string routingKey)
        {
            if (routingKey == NewAlarmRoutingKey)
            {
                var strResponse = StringHelper.ByteArrayToString(bytesArray);
                var alarmDto = JsonConvert.DeserializeObject<AlarmDTO>(strResponse);
                LogService.LogUser(LogAction.Receive, $"Receive - New alarm {JsonConvert.SerializeObject(alarmDto)}");
                var newAlarm = CreateNewAlarm(alarmDto, _alarmGuideModels);
                _alarmsModels.Add(newAlarm);
                if (_alarmsModels.Count > 100)
                {
                    _alarmsModels = _alarmsModels.OrderByDescending(a => a.TimeSpan).Take(100).ToList();
                }
                AlarmsViewModels = UpdatedAlarmList(_selectedSeverity, _alarmsModels);
            }
            if (routingKey == AcknowlegdedAlarmRoutingKey || routingKey == CancelledAlarmRoutingKey)
            {
                var strResponse = StringHelper.ByteArrayToString(bytesArray);
                var alarmId = JsonConvert.DeserializeObject<string>(JsonConvert.DeserializeObject<string>(strResponse));
                var alarm = _alarmsModels.FirstOrDefault(a => a.Id == alarmId);
                var action = routingKey == AcknowlegdedAlarmRoutingKey ? "Acknowledge" : "Cancel";
                if (alarm != null) 
                {
                    LogService.LogUser(LogAction.Receive, $"Receive - {action} alarm - {JsonConvert.SerializeObject(alarm)}");
                    var selectedAlarmIndex = AlarmsViewModels.ToList().FindIndex(a => SelectedAlarm != null && a.Id == SelectedAlarm.Id);
                    _alarmsModels.Remove(alarm);
                    AlarmsViewModels = UpdatedAlarmList(_selectedSeverity, _alarmsModels);
                    if (selectedAlarmIndex != -1)
                    {
                        SelectedAlarm = AlarmsViewModels.ElementAtOrDefault(selectedAlarmIndex);
                    }
                }
                else
                {
                    LogService.LogUser(LogAction.Receive, $"Receive - {action} unknown alarm Id {alarmId} from Machine");
                }
            }
            if (routingKey == RetriedAlarmRoutingKey)
            {
                var strResponse = StringHelper.ByteArrayToString(bytesArray);
                var retryAlarmResult = JsonConvert.DeserializeObject<AlarmRetryResultDTO>(strResponse);
                var alarmId = JsonConvert.DeserializeObject<string>(retryAlarmResult.AlarmId);
                var alarm = _alarmsModels.FirstOrDefault(a => a.Id == alarmId);
                if (alarm != null)
                {
                    if (retryAlarmResult.IsSuccess)
                    {
                        LogService.LogUser(LogAction.Receive, $"Receive - Retry success alarm - {JsonConvert.SerializeObject(alarm)}");

                        var selectedAlarmIndex = AlarmsViewModels.ToList().FindIndex(a => SelectedAlarm != null && a.Id == SelectedAlarm.Id);
                        _alarmsModels.Remove(alarm);
                        AlarmsViewModels = UpdatedAlarmList(_selectedSeverity, _alarmsModels);
                        if (selectedAlarmIndex != -1)
                        {
                            SelectedAlarm = AlarmsViewModels.ElementAtOrDefault(selectedAlarmIndex);
                        }
                    }
                    else
                    {
                        LogService.LogUser(LogAction.Receive, $"Receive - Retry failed alarm - {JsonConvert.SerializeObject(alarm)}");
                    }
                }
                else
                {
                    LogService.LogUser(LogAction.Receive, $"Receive - Retry unknown alarm Id {alarmId} from Machine");
                }
            }
        }

        private AlarmModel CreateNewAlarm(AlarmDTO alarmDto, IEnumerable<AlarmGuidelineModel> alarmGuideModels)
        {
            var alarmGuide = alarmGuideModels.FirstOrDefault(a => a.AlarmCode == alarmDto.ErrorCode);
            if (alarmGuide == null)
            {
                alarmGuide = new AlarmGuidelineModel(alarmDto.ErrorCode, string.Empty, string.Empty);
            }

            return new AlarmModel(
                        alarmDto.ErrorId,
                        alarmDto.TimeSpan,
                        alarmDto.Severity,
                        alarmDto.WaitForResponse,
                        alarmDto.AckEnabled,
                        alarmDto.RetryEnabled,
                        alarmDto.CancelEnabled,
                        alarmDto.ErrorCode,
                        alarmGuide.Description,
                        alarmGuide.RecoveryGuideline
                    );
        }

        private IEnumerable<AlarmModel> UpdatedAlarmList(ButtonAlarmSeverity selectedAlarmSeverity, IEnumerable<AlarmModel> alarmModels)
        {
            if (selectedAlarmSeverity == ButtonAlarmSeverity.All)
            {
                return alarmModels.ToList().OrderByDescending(a => a.TimeSpan);
            }
            else
            {
                return alarmModels.Where(a => a.Severity == GetAlarmSeverity(selectedAlarmSeverity)).OrderByDescending(a => a.TimeSpan);
            }
        }

        private AlarmSeverity GetAlarmSeverity(ButtonAlarmSeverity alarmSeverity)
        {
            switch (alarmSeverity)
            {
                case ButtonAlarmSeverity.High:
                    return AlarmSeverity.High;
                case ButtonAlarmSeverity.Medium:
                    return AlarmSeverity.Medium;
                case ButtonAlarmSeverity.Low:
                    return AlarmSeverity.Low;
                default:
                    throw new Exception("Not available severity");
            }
        }
    }
}
