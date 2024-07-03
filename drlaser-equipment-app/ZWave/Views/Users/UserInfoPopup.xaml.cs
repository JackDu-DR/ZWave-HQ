using CommunityToolkit.Maui.Views;
using ZWave.Helpers;
using ZWave.ViewModels.Users;
#if WINDOWS
using Microsoft.Maui.Handlers;
#endif

namespace ZWave.Views.Users;

public partial class UserInfoPopup : Popup
{
    private readonly UserInfoPopupViewModel _userInforPopupViewModel;

    public UserInfoPopup(UserInfoPopupViewModel vm)
    {
        InitializeComponent();
        _userInforPopupViewModel = vm;
        _userInforPopupViewModel.SetupViewForAddNewUser();
        BindingContext = _userInforPopupViewModel;
        UserInfoPopupContainer.HeightRequest = (double)ResourceHelper.GetDimension("UserInfoPopupAddNewHeith");
    }


    public UserInfoPopup(UserInfoPopupViewModel vm, Guid userid)
	{
        InitializeComponent();
        _userInforPopupViewModel = vm;
        _userInforPopupViewModel.SetupViewForEditUser(userid);
		BindingContext = _userInforPopupViewModel;
        UserInfoPopupContainer.HeightRequest = (double)ResourceHelper.GetDimension("UserInfoPopupEditHeith");
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
        ReEnterPasswordEntry.IsPassword = !ReEnterPasswordEntry.IsPassword;
        ShowHideRePasswordIcon.Source = ReEnterPasswordEntry.IsPassword ? "eye_closed_black.png" : "eye_opened_black.png";
    }

    public event EventHandler OnConfirmClose;
    private async void ConfirmButtonClicked(object sender, EventArgs e)
    {
        await _userInforPopupViewModel.ConfirmButtonClicked();
        OnConfirmClose?.Invoke(this, EventArgs.Empty);
        ClosePopup(sender, e);
    }
}