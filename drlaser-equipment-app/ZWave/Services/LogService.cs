using Serilog;
using ZWave.Helpers;
using ZWave.Shared.Enums;
using ZWave.Shared.Models;

namespace ZWave.Services
{
    public class LogService : BaseService, ILogService
    {
        private static readonly string LOG_ENDPOINT = "api/Log";
        private static readonly string SERI_LOG_TEMPLATE = "{@action} - {@message}";

        private static bool _isHttpServiceAvailable = true;
        private UserAuthDto _userAuthDto;
        private readonly int RETRY_POST_LOG_TIMESPAN = 5000;
        private System.Timers.Timer _timer;
        private List<LogDto> _logBuffer;
        public LogService()
        {
            _timer = new System.Timers.Timer(RETRY_POST_LOG_TIMESPAN);
            _timer.Elapsed += OnTimerElapsed;
            _logBuffer = [];
        }

        public Task<IEnumerable<LogDto>> GetLogs()
        {
            return HttpClientService.GetDataAsync<IEnumerable<LogDto>>($"{LOG_ENDPOINT}/All?count=1000");
        }

        public Task<IEnumerable<LogDto>> GetLogs(DateTime fromDate, DateTime toDate)
        {
            return HttpClientService.GetDataAsync<IEnumerable<LogDto>>($"{LOG_ENDPOINT}/All?fromDate={fromDate}&toDate={toDate}");
        }

        public void LogUserLocal(LogAction action, string message)
        {
            WriteLocalLog(action, message, false);
        }

        private void WriteLocalLog(LogAction action, string message, bool isMachineLog)
        {
            WriteSeriLog(action, message);
        }

		public void RegisterUserForLogActivities(UserAuthDto userAuthDto)
        {
            _userAuthDto = userAuthDto;
        }

        public void LogUser(LogAction action, string message)
        {
            switch (action)
            {
                case LogAction.UserActivity:
                    var user = _userAuthDto?.Fullname ?? "unknown";
                    message = $"User {user} - {message}";
                    break;
                default:
                    message = $"System - {message}";
                    break;
            }
            WriteLog(action, message);
        }

        private void WriteLog(LogAction action, string message, bool isMachineLog = false)
        {
            WriteSeriLog(action, message);
            PostLogAsync(action, message, isMachineLog);
        }

        private async void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var response = await HttpClientService.PostDataAsync($"{LOG_ENDPOINT}/CreateMany", _logBuffer);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _isHttpServiceAvailable = true;
                _timer.Stop();
                _logBuffer.Clear();
            }
        }

        private async void PostLogAsync(LogAction action, string message, bool isMachineLog)
        {
            var log = new LogDto()
            {
                Action = action,
                Message = message,
                IsMachineLog = isMachineLog,
                UserId = isMachineLog ? null : _userAuthDto?.Id,
            };

            if (!_isHttpServiceAvailable)
            {
                _logBuffer.Add(log);
                return;
            }

            var response = await HttpClientService.PostDataAsync($"{LOG_ENDPOINT}/Create", log);
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                _isHttpServiceAvailable = false;
                _logBuffer.Add(log);
                _timer.Start();
            }
        }

        private void WriteSeriLog(LogAction action, string message)
        {
            Log.Information(SERI_LOG_TEMPLATE, action, message);
        }
    }
}
