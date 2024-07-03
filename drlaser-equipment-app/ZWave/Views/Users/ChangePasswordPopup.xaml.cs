using CommunityToolkit.Maui.Views;
using ZWave.ViewModels.Users;

namespace ZWave.Views.Users;

public partial class ChangePasswordPopup : Popup
{
    private readonly ChangePasswordViewModel _changePasswordViewModel;
    public ChangePasswordPopup(ChangePasswordViewModel vm, Guid userId)
	{
        _changePasswordViewModel = vm;
        _changePasswordViewModel.SetupView(userId);
        InitializeComponent();
        BindingContext = _changePasswordViewModel;
	}

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }

    private void ShowHidePassword(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        ShowHidePasswordIcon.Source = PasswordEntry.IsPassword ? "eye_closed_black.png" : "eye_opened_black.png";
    }

    private void ShowHideRePassword(object sender, EventArgs e)
    {
        RePasswordEntry.IsPassword = !RePasswordEntry.IsPassword;
        ShowHideRePasswordIcon.Source = RePasswordEntry.IsPassword ? "eye_closed_black.png" : "eye_opened_black.png";
    }

    private async void ConfirmButtonClicked(object sender, EventArgs e)
    {
        await _changePasswordViewModel.ChangePassword();
        ClosePopup(sender, e);
    }
}