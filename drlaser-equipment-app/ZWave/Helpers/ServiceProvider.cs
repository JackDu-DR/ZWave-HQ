namespace ZWave.Helpers
{
    public static class ServiceProvider
    {
        public static IServiceProvider Current =>
#if WINDOWS
            MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;
#else
            null
#endif

        public static TService GetService<TService>()
        {
            return Current.GetService<TService>();
        }

        public static object GetService(Type type)
        {
            return Current.GetService(type);
        }
    }
}
