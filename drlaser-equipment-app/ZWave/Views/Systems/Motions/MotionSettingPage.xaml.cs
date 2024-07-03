namespace ZWave.Views.Systems.Motions;

public partial class MotionSettingPage : ContentView
{
    public MotionSettingPage()
	{
        InitializeComponent();
#if ANDROID
        // Make scroll view bar always visible so user know about it
        Microsoft.Maui.Handlers.ScrollViewHandler.Mapper.AppendToMapping("ObviousScrollViewCustomization",
        (handler, view) => {
            handler.PlatformView.ScrollbarFadingEnabled = false;
        });
#endif
    }
}