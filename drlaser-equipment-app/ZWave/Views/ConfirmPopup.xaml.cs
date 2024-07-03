using CommunityToolkit.Maui.Views;

namespace ZWave.Views;

public partial class ConfirmPopup : Popup
{
    private Action _confirmAction;

    public ConfirmPopup(Action confirmAction)
    {
        InitializeComponent();
        _confirmAction = confirmAction;
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void OnConfirmClicked(object sender, EventArgs e)
    {
        _confirmAction?.Invoke();
    }
}