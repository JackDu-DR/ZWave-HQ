using Microsoft.Extensions.Configuration;

namespace ZWave
{
    public class RabbitMQConfiguration
    {
        public string Url { get; set; }
    }

    public class ManagementServer
    {
        public string Url { get; set; }
    }

    public class AppConfigurations
    {
        private string DEFAULT_RABBITMQ_URL = "localhost";
        private string DEFAULT_MANAGEMENT_SERVER_URL = "localhost";

        public RabbitMQConfiguration RabbitMQConfiguration { get; set; }

        public ManagementServer ManagementServer { get; set; }

        public AppConfigurations() { }

        public AppConfigurations(IConfiguration configuration)
        {
            RabbitMQConfiguration = configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>() ?? new RabbitMQConfiguration() { Url = DEFAULT_RABBITMQ_URL };
            ManagementServer = configuration.GetSection("ManagementServer").Get<ManagementServer>() ?? new ManagementServer() { Url = DEFAULT_MANAGEMENT_SERVER_URL };

#if ANDROID
            var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DataDirectory.AbsolutePath);
            var filePath = $"{docsDirectory.AbsoluteFile.Path}/configuration.json";

            if (File.Exists(filePath))
            {
                var configurationText = File.ReadAllText(filePath);
                var appSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AppConfigurations>(configurationText);

                RabbitMQConfiguration = appSettings.RabbitMQConfiguration;
                ManagementServer = appSettings.ManagementServer;
            }
            else
            {
                var appSettingsString = Newtonsoft.Json.JsonConvert.SerializeObject(new AppConfigurations() { RabbitMQConfiguration = RabbitMQConfiguration, ManagementServer = ManagementServer });
                File.WriteAllText(filePath, appSettingsString);
            }
#endif
        }
    }
}
