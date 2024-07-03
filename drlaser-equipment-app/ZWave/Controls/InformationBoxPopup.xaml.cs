using CommunityToolkit.Maui.Views;

namespace ZWave.Controls;

public partial class InformationBoxPopup : Popup
{
    public InformationBoxPopup(string title, string message)
	{
		InitializeComponent();
        PopupTitle.Text = title;
        PopupContent.Text = message;
    }

    public event EventHandler OnOKButtonClicked;
    private void Button_Clicked(object sender, EventArgs e)
    {
        OnOKButtonClicked?.Invoke(this, e);
    }
}