namespace ZWave;

public class MauiExceptions
{
#if WINDOWS
    private Exception _lastFirstChanceException;
#endif

    public event UnhandledExceptionEventHandler UnhandledException;

    public MauiExceptions()
    {
        // Events fired by the TaskScheduler. That is calls like Task.Run(...)     
        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            UnhandledException?.Invoke(sender, new UnhandledExceptionEventArgs(args.Exception, args.Observed));
        };

#if WINDOWS
        AppDomain.CurrentDomain.FirstChanceException += (_, args) =>
        {
            _lastFirstChanceException = args.Exception;
        };

        Microsoft.UI.Xaml.Application.Current.UnhandledException += (sender, args) =>
        {
            var exception = args.Exception;
            if (exception.StackTrace is null)
            {
                exception = _lastFirstChanceException;
            }
            // args.Handled failed mean that this exception will cause the app to stop
            if (!args.Handled)
            {
                UnhandledException?.Invoke(sender, new UnhandledExceptionEventArgs(exception, true));
            }
        };
#else
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            if (args.IsTerminating) 
            {
                UnhandledException?.Invoke(sender, args);
            }
        };
#endif
    }
}