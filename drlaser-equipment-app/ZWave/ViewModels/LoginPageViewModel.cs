using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZWave.Helpers;

namespace ZWave.ViewModels
{
    public partial class LoginPageViewModel : BaseShellPageViewModel
    {
        [ObservableProperty]
        private string _errorText;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        protected override void Appearing()
        {
            base.Appearing();
        }

        [RelayCommand]
        protected override void Disappearing()
        {
            base.Disappearing();
        }

        [RelayCommand]
        async Task Login()
        {
            //var loginResult = await AuthService.Login(Username, Md5Helper.HashMd5(Password));
            //switch (loginResult)
            //{
            //    case Services.LoginStatus.Success:
            //        Username = string.Empty;
            //        Password = string.Empty;

            //        PageNavigationHelper.GoTo(Enums.Page.Master, new Dictionary<string, object> { { PageNavigationHelper.FromPageParamName, Enums.Page.Login } });
            //        break;
            //    case Services.LoginStatus.Unauthorized:
            //        ErrorText = Localization["UsernameOrPasswordIncorrectMessage"].ToString();
            //        break;
            //    case Services.LoginStatus.UserDisabled:
            //        ErrorText = Localization["AccountDisabledMessage"].ToString();
            //        break;
            //    case Services.LoginStatus.Failed:
            //    default:
            //        ErrorText = Localization["ErrorOccuredMessage"].ToString();
            //        break;
            //}
            PageNavigationHelper.GoTo(Enums.Page.Master, new Dictionary<string, object> { { PageNavigationHelper.FromPageParamName, Enums.Page.Login } });
        }
    }
}
