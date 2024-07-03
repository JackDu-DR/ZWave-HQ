using CommonLib.MessageHandler.Interface;
using System.Reflection;
using ZWave.Services;

namespace ZWave
{
    public partial class App : Application
    {
        private static readonly ILogService _logService = Helpers.ServiceProvider.GetService<ILogService>();

#if WINDOWS
        private static Mutex mutex = new Mutex(true, Assembly.GetEntryAssembly().GetName().Name);
#endif
        public App()
        {
#if WINDOWS
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                Current.Quit();
                Environment.Exit(0);
            }
#endif
            InitializeComponent();
            EstablishMachineConnection();
            var globalException = new MauiExceptions();
            globalException.UnhandledException += MauiExceptions_UnhandledException;
            MainPage = new AppShell();

            AddDimensionsResource("Dimensions.xaml");
            AddStylesResource();
        }

        private void MauiExceptions_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
            {
                var environment = "Windows";
#if ANDROID
                environment = "Android";
#endif
                var exception = (Exception)e.ExceptionObject;
                _logService.LogUser(Shared.Enums.LogAction.Exception, $"{environment} Thown - {exception.Message}");
            }
        }

        private void AddDimensionsResource(string resource)
        {
            var path = $"Resources/Styles/{resource}";
            var sourceUri = new Uri(path, UriKind.RelativeOrAbsolute);
            var source = new ResourceDictionary();
            source.SetAndLoadSource(sourceUri, path, GetType().GetTypeInfo().Assembly, null);

#if ANDROID
            var standardScreenWidthInPixel = 1920;
            var standardScreenHeightInPixel = 1080;

            var width = (float)DeviceDisplay.Current.MainDisplayInfo.Width;
            var height = (float)DeviceDisplay.Current.MainDisplayInfo.Height;
            var density = (float)DeviceDisplay.Current.MainDisplayInfo.Density;

            var horizontalPadding = 0.0;
            var verticalPadding = 0.0;

            var keys = source.Keys;
            foreach(var key in keys)
            {
                var value = source[key];
                if (value is double)
                {
                    source.Remove(key);
                    source.Add(key, ((double)value / density));
                }
                if (value is Thickness)
                {
                    source.Remove(key);
                    source.Add(key, new Thickness(((Thickness)value).Left / density, ((Thickness)value).Top / density, ((Thickness)value).Right / density, ((Thickness)value).Bottom / density));
                }
            }
#endif
            Resources.MergedDictionaries.Add(source);
        }

        private void AddStylesResource()
        {
            var path = $"Resources/Styles/Styles.xaml";
            var sourceUri = new Uri(path, UriKind.RelativeOrAbsolute);
            var source = new ResourceDictionary();
            source.SetAndLoadSource(sourceUri, path, GetType().GetTypeInfo().Assembly, null);
            Resources.MergedDictionaries.Add(source);
        }

        private static void EstablishMachineConnection()
        {
            var appMessageHandler = Helpers.ServiceProvider.GetService<IAppMessageHandler>();
            var configurationService = Helpers.ServiceProvider.GetService<IConfigurationService>();
            var appSettings = Helpers.ServiceProvider.GetService<AppConfigurations>();

            try
            {
                _logService.LogUser(Shared.Enums.LogAction.Connect, $"Connecting to {appSettings.RabbitMQConfiguration.Url}");
                appMessageHandler.EstablishConnection(appSettings.RabbitMQConfiguration.Url);
                _logService.LogUser(Shared.Enums.LogAction.Connect, "Connected");

               // configurationService.GetConfig().ConfigureAwait(false);
               //configurationService.GetConfig_MotionAxisControl().ConfigureAwait(false);
               //configurationService.GetConfig_DonorLifting().ConfigureAwait(false);
               //configurationService.GetConfig_ProcessTable().ConfigureAwait(false);
               //configurationService.GetConfig_InspectionVision().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logService.LogUser(Shared.Enums.LogAction.Connect, $"Connect failed {ex.Message}");
            }
        }
    }
}
