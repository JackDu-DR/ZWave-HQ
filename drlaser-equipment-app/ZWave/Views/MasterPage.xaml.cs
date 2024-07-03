using ZWave.Services;

namespace ZWave.Views;

public partial class MasterPage : ContentPage
{
    private readonly ILogService LogService = Helpers.ServiceProvider.GetService<ILogService>();

    public MasterPage()
    {
        InitializeComponent();

#if ANDROID
        var standardScreenWidthInPixel = 1920;
        var standardScreenHeightInPixel = 1080;

        var width = (float)DeviceDisplay.Current.MainDisplayInfo.Width;
        var height = (float)DeviceDisplay.Current.MainDisplayInfo.Height;
        var density = (float)DeviceDisplay.Current.MainDisplayInfo.Density;

        MasterGrid.HeightRequest = standardScreenHeightInPixel / density;
        MasterGrid.WidthRequest = standardScreenWidthInPixel / density;
#endif
    }

    private void ShutdownButton_Clicked(object sender, EventArgs e)
    {
        LogService.LogUser(Shared.Enums.LogAction.UserActivity, "Clicked - Shutdown button");
   
        ShutdownDropLayout.IsVisible = !ShutdownDropLayout.IsVisible;
    }
    private void ShutdownSystemButton_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}