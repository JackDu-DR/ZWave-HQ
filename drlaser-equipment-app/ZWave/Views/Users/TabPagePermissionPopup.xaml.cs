using CommunityToolkit.Maui.Views;

namespace ZWave.Views.Users;

public partial class TabPagePermissionPopup : Popup
{
    public TabPagePermissionPopup()
	{
		InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
		Close();
    }
}