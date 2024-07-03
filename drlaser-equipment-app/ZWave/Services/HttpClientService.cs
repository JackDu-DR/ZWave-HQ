using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace ZWave.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static readonly AppConfigurations AppSettings = Helpers.ServiceProvider.GetService<AppConfigurations>();

        private static readonly string _baseUri = AppSettings.ManagementServer.Url;
        //private static readonly string _baseUri = "http://10.10.10.48:45455/";

        private HttpClient _client;

        public event EventHandler<EventArgs> UnAuthorizedResponsed;

        private HttpClient _httpClient
        {
            get
            {
                if (_client == null)
                {
                    _client = Helpers.ServiceProvider.GetService<HttpClient>();
                    _client.BaseAddress = new Uri(_baseUri);
                    _client.Timeout = TimeSpan.FromMinutes(2);
                }

                return _client;
            }
        }

        public async Task<T> GetDataAsync<T>(string endpoint)
        {
            try
            {
                var httpRequestMessage = CreateHttpMessage(HttpMethod.Get, endpoint, null);

                var response = await _httpClient.SendAsync(httpRequestMessage);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    UnAuthorizedResponsed?.Invoke(this, null);
                }
            }
            catch (Exception e)
            {
                Log.Information("{@action} - {@message}", "GetDataAsync", e.Message);
            }

            return default;
        }

        public async Task<HttpResponseMessage> PostDataAsync(string endpoint, object data)
        {
            try
            {
                var dataContent = new StringContent(JsonConvert.SerializeObject(data), encoding: Encoding.UTF8, "application/json");
                var httpRequestMessage = CreateHttpMessage(HttpMethod.Post, endpoint, dataContent);

                var response = await _httpClient.SendAsync(httpRequestMessage);
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (HttpRequestException e)
            {
                if (e.HttpRequestError == HttpRequestError.ConnectionError)
                {
                    Log.Information("{@action} - {@message}", "PostDataResponse", e.Message);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable);
                }
                else if (e.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    UnAuthorizedResponsed?.Invoke(this, null);

                    Log.Information("{@action} - {@message}", "PostDataResponse", e.Message);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception e)
            {
                Log.Information("{@action} - {@message}", "PostDataResponse", e.Message);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        private HttpRequestMessage CreateHttpMessage(HttpMethod method, string endpoint, HttpContent data)
        {
            var httpRequestMessage = new HttpRequestMessage(method, endpoint);
            httpRequestMessage.Headers.Add("Accept", "application/json");

            if (data != null)
            {
                httpRequestMessage.Content = data;
            }

            return httpRequestMessage;
        }

        public void RemoveHeader(string key)
        {
            _httpClient.DefaultRequestHeaders.Remove(key);
        }

        public void UpdateHeader(string key, string value)
        {
            RemoveHeader(key);

            _httpClient.DefaultRequestHeaders.Add(key, value);
        }
    }
}
