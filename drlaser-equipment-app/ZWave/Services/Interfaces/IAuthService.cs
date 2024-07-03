using ZWave.Shared.Models;

namespace ZWave.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Login
        /// </summary>
        Task<LoginStatus> Login(string username, string passwordHash);

        /// <summary>
        /// Get userinfo based on the logging-in token
        /// </summary>
        Task<UserAuthDto> GetUserInfo();

        void InitLoggedInData(string token);

        void ClearLoggedInData();

        event EventHandler<EventArgs> LogoutRequested;
    }
}
