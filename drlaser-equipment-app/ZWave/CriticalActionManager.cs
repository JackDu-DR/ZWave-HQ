using CommonLib.ClassHierarchy.Interface;
using CommonLib.DTO.Implementation;
using CommonLib.MessageHandler.Interface;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using CommonLib.Enums;
using ZWave.Helpers;
using Serilog;

namespace ZWave
{
    public class CriticalActionManager : INotifyPropertyChanged
    {
        protected readonly IMaster Master = Helpers.ServiceProvider.GetService<IMaster>();

        private Dictionary<CriticalType, bool> _criticalActionTable = Enum.GetValues(typeof(CriticalType)).Cast<CriticalType>()
            .ToDictionary(e => e, e => false);

        protected readonly IAppMessageHandler AppMessageHandler = Helpers.ServiceProvider.GetService<IAppMessageHandler>();

        private string CriticalUpdateId => Master.CriticalControl.Update;
        private string CriticalFetchId => Master.CriticalControl.Fetch;

        private CriticalActionManager()
        {
        }

        public static CriticalActionManager Instance { get; } = new();

        /// <summary>
        /// Indicate if any critical action currently running
        /// </summary>
        public bool IsRunning => _criticalActionTable.Any(c => c.Key != CriticalType.Any && c.Value);

        public bool this[string resourceKey]
            => _criticalActionTable[Enum.Parse<CriticalType>(resourceKey)];

        public event PropertyChangedEventHandler PropertyChanged;

        CancellationTokenSource _cancellationTokenSource;
        public async void UpdateCriticalAction(CriticalType criticalType, bool isProcessing, int timeout)
        {
            if (!isProcessing)
            {
                _cancellationTokenSource?.Cancel();
            }

            ChangeCriticalValue(criticalType, isProcessing);

            if (isProcessing)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                try
                {
                    await Task.Delay(timeout, _cancellationTokenSource.Token);

                    ChangeCriticalValue(criticalType, false);
                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("Critical Delay was canceled.");
                }
            }
        }

        public void SubscribeCriticalTopic()
        {
            AppMessageHandler.SubcribeGeneral(CriticalUpdateId);
            AppMessageHandler.OnGeneralMessageReceived += OnCriticalTopicMessageReceived;
        }

        public async void FetchData()
        {
            try
            {
                var response = await AppMessageHandler.Request(CriticalFetchId, null);
                var strResponse = StringHelper.ByteArrayToString(response);
                var message = JsonConvert.DeserializeObject<Message>(strResponse);
                var criticalActionDTO = JsonConvert.DeserializeObject<CriticalActionDTO>(message.Data.ToString());

                UpdateCriticalAction(criticalActionDTO.CriticalType, criticalActionDTO.IsProcessing, criticalActionDTO.Timeout);
            }
            catch (Exception e)
            {
                Log.Warning($"Could not get critical status from machine. Exeption: {e}");
            }
        }

        private void OnCriticalTopicMessageReceived(object sender, byte[] bytesArray, string routingKey)
        {
            if (routingKey == CriticalUpdateId)
            {
                var strResponse = StringHelper.ByteArrayToString(bytesArray);
                var criticalActionDTO = JsonConvert.DeserializeObject<CriticalActionDTO>(strResponse);

                UpdateCriticalAction(criticalActionDTO.CriticalType, criticalActionDTO.IsProcessing, criticalActionDTO.Timeout);
            }
        }

        private void ChangeCriticalValue(CriticalType criticalType, bool isProcessing)
        {
            _criticalActionTable[criticalType] = isProcessing;
            _criticalActionTable[CriticalType.Any] = _criticalActionTable.Any(c => c.Key != CriticalType.Any && c.Value);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
