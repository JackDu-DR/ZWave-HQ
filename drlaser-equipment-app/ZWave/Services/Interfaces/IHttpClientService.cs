namespace ZWave.Services
{
    public interface IHttpClientService
    {
        /// <summary>
        /// <para>
        /// Get data async and returns data of
        /// <typeparamref name="T"/> type.
        /// </para>
        /// </summary>
        /// <exception cref="HttpException"></exception>
        Task<T> GetDataAsync<T>(string endpoint);

        /// <summary>
        /// Post data to endpoint
        /// </summary>
        Task<HttpResponseMessage> PostDataAsync(string endpoint, object data);

        /// <summary>
        /// Update value for a specific header of the HttpClientMessage
        /// </summary>
        public void UpdateHeader(string key, string value);

        public void RemoveHeader(string key);

        event EventHandler<EventArgs> UnAuthorizedResponsed;
    }
}
