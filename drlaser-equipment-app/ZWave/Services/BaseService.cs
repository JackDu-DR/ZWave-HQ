namespace ZWave.Services
{
    public abstract class BaseService
    {
        protected readonly IHttpClientService HttpClientService = Helpers.ServiceProvider.GetService<IHttpClientService>();
        protected readonly IConfigurationService ConfigurationService = Helpers.ServiceProvider.GetService<IConfigurationService>();

    }
}
