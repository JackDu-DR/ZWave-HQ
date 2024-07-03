using CommunityToolkit.Maui.Views;

namespace ZWave.Views;

public partial class NeedShutdownErrorPopup : Popup
{
    public NeedShutdownErrorPopup(string title, string message)
    {
        InitializeComponent();
        PopupTitle.Text = title;
        PopupContent.Text = message;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}