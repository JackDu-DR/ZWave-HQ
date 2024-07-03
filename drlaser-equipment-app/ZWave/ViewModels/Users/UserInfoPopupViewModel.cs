using CommunityToolkit.Mvvm.ComponentModel;
using ZWave.Helpers;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Users
{
    public partial class UserInfoPopupViewModel : UserBaseViewModel
    {
        private Guid? _userId;
        private bool _isActive;

        public string PopupTitle { get; private set; } = string.Empty;
        public string ConfirmButtonText { get; private set; } = string.Empty;
        public bool IsAddNewUser { get; private set; } = true;

        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _reEnterPassword;

        [ObservableProperty]
        private IList<string> _userRoles;

        [ObservableProperty]
        private string _selectedRole;

        public void SetupViewForAddNewUser()
        {
            PopupTitle = Localization["AddNewUser"].ToString();
            ConfirmButtonText = Localization["Create"].ToString();
            IsAddNewUser = true;
            GetUserRoles();
            SelectedRole = UserRoles.FirstOrDefault() ?? string.Empty;
        }

        public void SetupViewForEditUser(Guid userId)
        {
            PopupTitle = Localization["EditUser"].ToString();
            ConfirmButtonText = Localization["Save"].ToString();
            IsAddNewUser = false;
            GetUserRoles();
            GetUserInformation(userId);
        }

        private async void GetUserInformation(Guid userId)
        {
            var user = await UserService.GetUserById(userId);
            if (user != null)
            {
                _userId = userId;
                _isActive = user.Status;
                FullName = user.FullName;
                Username = user.Username;
                SelectedRole = user.UserRole;
            }
        }

        private void GetUserRoles()
        {
            UserRoles = EnumHelper.GetDisplayNames<UserRole>().ToList();
        }

        public async Task<bool> ConfirmButtonClicked()
        {
            if (IsAddNewUser)
            {
                return await AddNewUser();
            }
            else
            {
                return await EditUser();
            }
        }

        private async Task<bool> AddNewUser()
        {
            try
            {
                LogService.LogUser(LogAction.UserActivity, $"User Page Clicked - Confirm add new user with username {Username}, full name {FullName}, role {SelectedRole}, machineId {ConfigurationService.MachineId}");
                var userRole = EnumHelper.GetEnumValue<UserRole>(SelectedRole);
                await UserService.CreateUser(FullName, Username, Md5Helper.HashMd5(Password), userRole, ConfigurationService.MachineId);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private async Task<bool> EditUser()
        {
            if (_userId == null) return false;

            try
            {
                var isActive = _isActive ? "active" : "disabled";
                LogService.LogUser(LogAction.UserActivity, $"User Page Clicked - Confirm Edit user with Id {_userId}, full name {FullName}, status {isActive}, role {SelectedRole}");
                var userRole = EnumHelper.GetEnumValue<UserRole>(SelectedRole);
                await UserService.UpdateUser((Guid)_userId,  FullName, _isActive, userRole);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
