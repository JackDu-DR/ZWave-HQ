using ZWave.ViewModels.Systems;

namespace ZWave.Views.Systems;

public partial class LaserBasicPage : ContentView
{
    public LaserBasicPage(LaserBasicPageViewModel laserBasicPageViewModel)
    {
        InitializeComponent();
        BindingContext = laserBasicPageViewModel;
    }
}