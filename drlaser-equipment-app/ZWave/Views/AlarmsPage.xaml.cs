using ZWave.Helpers;
using ZWave.Models;
using ZWave.ViewModels;

namespace ZWave.Views;

public partial class AlarmsPage : ContentView
{
    public AlarmsPage(AlarmsPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
#if WINDOWS
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (handler, v) =>
        {
              handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
        });
#endif
    }
}