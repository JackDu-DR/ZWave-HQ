using ZWave.Shared.Enums;
using ZWave.Shared.Models;

namespace ZWave.Services
{
    public interface ILogService
    {
        /// <summary>
        /// Get all logs
        /// </summary>
        Task<IEnumerable<LogDto>> GetLogs();

        /// <summary>
        /// Get logs from date to date
        /// </summary>
        Task<IEnumerable<LogDto>> GetLogs(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Register login user to log activities of this user
        /// </summary>
        /// <param name="userAuthDto"></param>
        void RegisterUserForLogActivities(UserAuthDto userAuthDto);

        /// <summary>
        /// Write log into database
        /// </summary>
        void LogUser(LogAction action, string message);
        void LogUserLocal(LogAction action, string message);
    }
}
