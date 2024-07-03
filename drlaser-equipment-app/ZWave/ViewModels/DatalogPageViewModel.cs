using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZWave.Helpers;
using ZWave.Models;

namespace ZWave.ViewModels
{
    public partial class DatalogPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        IEnumerable<LogItemModel> _logItems;

        [ObservableProperty]
        DateTime _dateFrom = DateTime.Today;

        [ObservableProperty]
        DateTime _dateTo = DateTime.Today;

        [ObservableProperty]
        bool _isRefreshing;

        [ObservableProperty]
        bool _isLoading;

        [RelayCommand]
        async Task Export()
        {
            LogService.LogUser(Shared.Enums.LogAction.UserActivity, $"Datalog Page Export - Data from date {DateFrom.ToString("yyyy-MM-dd hh:mm:ss")} to date {DateTo.ToString("yyyy-MM-dd hh:mm:ss")}");
            IsLoading = true;
            var logs = await LogService.GetLogs(DateFrom, DateTo);
            var logStrings = logs.Select(log => $"{log.CreatedTime.ToString("yyyy-MM-dd hh:mm:ss.fffffffK")} - {(log.IsMachineLog ? "Machine" : log.UserName)} - {log.Action} - {log.Message}");
            await FileHelper.SaveStringsToFile(logStrings);
            IsLoading = false;
        }

        [RelayCommand]
        async Task Refresh()
        {
            LogService.LogUser(Shared.Enums.LogAction.UserActivity, "Datalog Page Refresh - Data list");
            await FetchData();
            IsRefreshing = false;
        }

        public DatalogPageViewModel()
        {
            FetchData().ConfigureAwait(false);
        }

        private async Task FetchData()
        {
            var logs = await LogService.GetLogs();
            LogItems = logs.Select((log, index) => new LogItemModel
            {
                Index = index,
                CreatedTime = log.CreatedTime.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                IsMachineLog = log.IsMachineLog,
                Message = log.Message,
                UserName = log.UserName,
                Action = log.Action.ToString()
            });
        }
    }
}
