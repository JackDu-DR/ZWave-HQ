using System.Net;
using ZWave.Shared.Models;

namespace ZWave.Services
{
    public enum LoginStatus
    {
        Success,
        UserDisabled,
        Unauthorized,
        Failed
    }

    public class AuthService : BaseService, IAuthService
    {
        private static readonly string ENDPOINT = "api/Auth";
        private static readonly string AUTHORIZATION_HEADER = "Authorization";
        private static readonly string BEARER = "Bearer";
        public static readonly string ACCESS_TOKEN = "Access_Token";

        public event EventHandler<EventArgs> LogoutRequested;

        public AuthService() : base()
        {
            HttpClientService.UnAuthorizedResponsed += OnUnAuthorizedResponsed;
        }

        private void OnUnAuthorizedResponsed(object sender, EventArgs e)
        {
            LogoutRequested?.Invoke(this, null);
        }

        public async Task<LoginStatus> Login(string username, string passwordHash)
        {
            var machineId = ConfigurationService.MachineId;

            var responseMessage = await HttpClientService.PostDataAsync($"{ENDPOINT}/Login?username={username}&passwordHash={passwordHash}&machineId={machineId}", null);
            if (responseMessage != null)
            {
                switch (responseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var token = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseMessage.Content.ReadAsStringAsync().Result);
                        InitLoggedInData(token);
                        return LoginStatus.Success;
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.NotFound:
                        return LoginStatus.Unauthorized;
                    case HttpStatusCode.Forbidden:
                        return LoginStatus.UserDisabled;
                    default:
                        return LoginStatus.Failed;
                }
            }

            return LoginStatus.Failed;
        }

        public async Task<UserAuthDto> GetUserInfo()
        {
            var userInfoDto = await HttpClientService.GetDataAsync<UserAuthDto>($"{ENDPOINT}/UserInfo");
            if (userInfoDto == default(UserAuthDto))
            {
                userInfoDto = new UserAuthDto();
            }
            
            return userInfoDto;
        }

        public async void InitLoggedInData(string token)
        {
            HttpClientService.UpdateHeader(AUTHORIZATION_HEADER, BEARER + " " + token);

            await SecureStorage.Default.SetAsync(ACCESS_TOKEN, token);
        }

        public void ClearLoggedInData()
        {
            HttpClientService.RemoveHeader(AUTHORIZATION_HEADER);

            SecureStorage.Default.Remove(ACCESS_TOKEN);
        }
    }
}
