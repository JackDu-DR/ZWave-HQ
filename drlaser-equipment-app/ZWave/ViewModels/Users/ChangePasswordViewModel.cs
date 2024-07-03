using CommunityToolkit.Mvvm.ComponentModel;
using ZWave.Helpers;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Users
{
    public partial class ChangePasswordViewModel : UserBaseViewModel
    {
        private Guid? _userId;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _newPassword;

        [ObservableProperty]
        private string _reNewPassword;

        public async void SetupView(Guid userId)
        {
            var user = await UserService.GetUserById(userId);
            if (user != null)
            {
                _userId = userId;
                Username = user.Username;
            }
        }

        public async Task<bool> ChangePassword()
        {
            if (_userId == null) 
                return false;
            try
            {
                LogService.LogUser(LogAction.UserActivity, $"User Page Clicked - Confirm change password button of user with Id {_userId}");
                await UserService.ChangePassword((Guid)_userId, Md5Helper.HashMd5(NewPassword));
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
