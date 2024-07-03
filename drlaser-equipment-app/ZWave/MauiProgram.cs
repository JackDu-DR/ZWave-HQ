using CommonLib.ClassHierarchy.Implementation;
using CommonLib.ClassHierarchy.Interface;
using CommonLib.MessageHandler.Implementation;
using CommonLib.MessageHandler.Interface;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Reflection;
using ZWave.Services;
using ZWave.Services.Interfaces;
using ZWave.ViewModels;
using ZWave.ViewModels.Systems;
using ZWave.ViewModels.Systems.CausewaySystem;
using ZWave.ViewModels.Systems.Vision;
using ZWave.ViewModels.Users;
using ZWave.Views;
using ZWave.Views.Jobs;
using ZWave.Views.Systems;
using ZWave.Views.Systems.CausewaySystem;
using ZWave.Views.Systems.Motions;
using ZWave.Views.Systems.ProcessSystem;


#if WINDOWS
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Handlers;
#endif

namespace ZWave
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var assembly = Assembly.GetExecutingAssembly();
            var config = new ConfigurationBuilder()
                .AddJsonFile("configurations.json", true)
                .Build();

            builder.Configuration.AddConfiguration(config);

            var appSettings = new AppConfigurations(config);
            builder.Services.AddSingleton(typeof(AppConfigurations), appSettings);

            InitSerilog();
            builder.Logging.AddSerilog();

            builder.Services.AddSingleton<HttpClient>();

            builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
            builder.Services.AddSingleton<ILogService, LogService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IAlarmGuidelineService, AlarmGuidelineService>();
            builder.Services.AddSingleton<IPermissionService, PermissionService>();

            //CommonLib
            builder.Services.AddSingleton(typeof(IAppMessageHandler), new AppMessageHandler());
            builder.Services.AddSingleton<IMaster, Master>();
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddSingleton<MasterPage>();

            builder.Services.AddSingleton<JobsPage>();
            builder.Services.AddSingleton<JobMainView>();
            builder.Services.AddSingleton<JobMainView2>();

            builder.Services.AddSingleton<SystemPage>();
            builder.Services.AddSingleton<SystemMotionPage>();
            builder.Services.AddSingleton<SystemVisionPageViewModel>();
            builder.Services.AddSingleton<MotionAxisControlPage>();
            builder.Services.AddSingleton<MotionSettingPage>();
            builder.Services.AddSingleton<MotionControlPage>();
            builder.Services.AddSingleton<TuneSettingsGainsControlPage>();
            builder.Services.AddSingleton<TuneSettingsFilterControlPage>();
            builder.Services.AddSingleton<SystemLaserPage>();
            builder.Services.AddSingleton<SystemVisionPage>();
            builder.Services.AddSingleton<LaserBasicPage>();
            builder.Services.AddSingleton<LaserBasicPageViewModel>();
            builder.Services.AddSingleton<LaserExternalControlPage>();

            builder.Services.AddSingleton<LaserBurstControlPage>();
            builder.Services.AddSingleton<LaserBasicPageViewModel>();

            builder.Services.AddSingleton<RecipiesPage>();
            builder.Services.AddSingleton<SetupPage>();
            builder.Services.AddSingleton<DatalogPage>();
            builder.Services.AddSingleton<DatalogPageViewModel>();
            builder.Services.AddSingleton<AlarmsPage>();
            builder.Services.AddSingleton<AlarmsPageViewModel>();
            builder.Services.AddSingleton<UsersPage>();


            builder.Services.AddTransient<UserInfoPopupViewModel>();
            builder.Services.AddTransient<ChangePasswordViewModel>();
            builder.Services.AddTransient<TabPagePermissionViewModel>();

            builder.Services.AddSingleton<DonorLiftingModulePageViewModel>();
            builder.Services.AddSingleton<DonorLiftingModulePage>();
            builder.Services.AddSingleton<SubstrateLiftingModulePage>();

            builder.Services.AddSingleton<ProcessTablePage>();
            builder.Services.AddSingleton<InspectionVisionPage>();

#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        window.ExtendsContentIntoTitleBar = false;
                        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
                        var _appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
                        if (_appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
                        {
                            presenter.SetBorderAndTitleBar(false, false);
                            presenter.Maximize();
                        }
                    });
                });
            });

            // Placeholder for picker on Window issue work-around, open issues: https://github.com/dotnet/maui/issues/6845 
            PickerHandler.Mapper.AppendToMapping(nameof(IPicker.Title), (handler, view) =>
            {
                if (handler.PlatformView is not null && view is Picker pick && !String.IsNullOrWhiteSpace(pick.Title))
                {
                    handler.PlatformView.HeaderTemplate = new Microsoft.UI.Xaml.DataTemplate();
                    handler.PlatformView.PlaceholderText = pick.Title;
                    pick.Title = null;
                 }
            });
#endif

#if ANDROID
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (h, v) =>
            {
                h.PlatformView.SetPadding(5, 0, 5, 0);
                h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            });
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void InitSerilog()
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
#if ANDROID
            appPath = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DataDirectory.AbsolutePath).Path;
#endif
            Log.Logger = new LoggerConfiguration()
                 .Enrich.With(new ThreadIdEnricher())
                 .MinimumLevel.Debug().WriteTo.Console()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                 .Enrich.FromLogContext()
                 .WriteTo.File($"{appPath}\\Logs\\Log-.txt",
                                rollingInterval: RollingInterval.Day,
                                restrictedToMinimumLevel: LogEventLevel.Information,
                                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({ThreadId}) {LogCategory} - {Message}{NewLine}{Exception}")
                 .CreateLogger();
        }
    }
}
