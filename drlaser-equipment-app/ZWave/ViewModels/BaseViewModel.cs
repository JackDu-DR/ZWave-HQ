using CommonLib.ClassHierarchy.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommonLib.MessageHandler.Interface;
using ZWave.Services;

namespace ZWave.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        public LocalizationResourceManager Localization => LocalizationResourceManager.Instance;
        public CriticalActionManager CriticalAction => CriticalActionManager.Instance;
        protected readonly IMaster Master = Helpers.ServiceProvider.GetService<IMaster>();
        protected readonly IConfigurationService ConfigurationService = Helpers.ServiceProvider.GetService<IConfigurationService>();
        protected readonly IAuthService AuthService = Helpers.ServiceProvider.GetService<IAuthService>();

        protected readonly ILogService LogService = Helpers.ServiceProvider.GetService<ILogService>();

        protected readonly IAppMessageHandler AppMessageHandler = Helpers.ServiceProvider.GetService<IAppMessageHandler>();
        
        protected readonly AppConfigurations AppSettings = Helpers.ServiceProvider.GetService<AppConfigurations>();
    }
}
