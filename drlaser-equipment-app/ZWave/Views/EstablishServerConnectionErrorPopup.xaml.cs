using CommunityToolkit.Maui.Views;

namespace ZWave.Views;

public partial class EstablishServerConnectionErrorPopup : Popup
{
	public EstablishServerConnectionErrorPopup(string title, string content)
	{
		InitializeComponent();
		PopupTitle.Text = title;
		PopupContent.Text = content;
	}

    private void ShutdownButton_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    public event EventHandler OnRetryButtonClicked;
    private void RetryButton_Clicked(object sender, EventArgs e)
    {
        OnRetryButtonClicked?.Invoke(this, e);
    }
}