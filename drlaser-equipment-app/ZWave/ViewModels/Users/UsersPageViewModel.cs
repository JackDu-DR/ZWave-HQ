using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZWave.Extensions;
using ZWave.Helpers;
using ZWave.Models;
using ZWave.PopupBuilders;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Users
{
    public partial class UsersPageViewModel : UserBaseViewModel
    {
        private System.Timers.Timer typingTimer;

        [ObservableProperty]
        private IList<UserModel> _users;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    StartTimer();
                }
            }
        }

        [RelayCommand]
        void Refresh()
        {
            FetchUsers();
        }

        [RelayCommand]
        void ConfigureRoleAccess()
        {
            LogService.LogUser(LogAction.UserActivity, "Users Page Clicked - Config role permissions button");
            var popup = new TabPagePermissionPopupBuilder().Build();
            popup.OpenPopup();
        }

        [RelayCommand]
        void AddUser()
        {
            LogService.LogUser(LogAction.UserActivity, "Users Page Clicked - Add new user button");
            var popup = new AddNewUserPopupBuilder().Build();
            popup.OnConfirmClose += OnConfirmClose;
            popup.OpenPopup();
        }

        [RelayCommand]
        void Edit(Guid id)
        {
            LogService.LogUser(LogAction.UserActivity, $"Users Page Clicked - Edit button of user with Id {id} ");
            var popup = new EditUserPopupBuilder(id).Build();
            popup.OnConfirmClose += OnConfirmClose;
            popup.OpenPopup();
        }

        [RelayCommand]
        void ToggleStatus(Guid id)
        {
            UpdateUserStatus(id);
        }

        [RelayCommand]
        void ChangePassword(Guid id)
        {
            LogService.LogUser(LogAction.UserActivity, $"Users Page Clicked - Change password button of user with Id {id}");
            var popup = new ChangePasswordPopupBuilder(id).Build();
            popup.OpenPopup();
        }

        public UsersPageViewModel()
        {
            _users = new List<UserModel>();
            FetchUsers();
            Localization.PropertyChanged += OnLanguageChanged;
        }

        private async void FetchUsers()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                LogService.LogUser(LogAction.UserActivity, $"Users Page Search - {SearchText}");
                var users = await UserService.SearchUsers(SearchText);
                Users = users.ToList();
            }
            else
            {
                var users = await UserService.GetUsers();
                Users = users.ToList();
            }
        }

        private void OnConfirmClose(object sender, EventArgs e)
        {
            FetchUsers();
        }

        private async void UpdateUserStatus(Guid userId)
        {
            try
            {
                var user = Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    var userRole = EnumHelper.GetEnumValue<UserRole>(user.UserRole);
                    await UserService.UpdateUser(userId, user.FullName, !user.Status, userRole);
                    FetchUsers();
                }
            }
            catch
            {
                // Log Error
            }
        }

        private void OnLanguageChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            foreach (var user in Users)
            {
                user.OnLanguageChanged();
            }
        }

        private void StartTimer()
        {
            if (typingTimer == null)
            {
                typingTimer = new System.Timers.Timer(500); // 500 milliseconds delay
                typingTimer.AutoReset = false;
                typingTimer.Elapsed += (sender, e) =>
                {
                    typingTimer.Stop();
                    Task.Run(() =>
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            FetchUsers();
                        });
                    });
                };
            }
            else
            {
                typingTimer.Stop();
            }

            typingTimer.Start();
        }
    }
}
