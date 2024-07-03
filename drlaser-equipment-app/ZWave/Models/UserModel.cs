using Newtonsoft.Json.Linq;
using ZWave.Helpers;
using ZWave.Shared.Models;

namespace ZWave.Models
{
    public class UserModel : BaseModel
    {
        public Guid Id { get; set; }
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _userRole;
        public string UserRole
        {
            get => _userRole;
            set => SetProperty(ref _userRole, value);
        }

        private bool _status;
        public bool Status
        {
            get => _status;
            set 
            {
                SetProperty(ref _status, value);
            }
        }

        private string _lastLogin;
        public string LastLogin
        {
            get => _lastLogin;
            set => SetProperty(ref _lastLogin, value);
        }

        public void OnLanguageChanged()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                OnPropertyChanged(nameof(Status));
            });
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var userDto = (UserDto)baseDTO;
            Id = userDto.Id;
            FullName = userDto.FullName ?? string.Empty;
            Username = userDto.UserName;
            Status = userDto.IsActive;
            UserRole = EnumHelper.GetDisplayName(userDto.UserRole);
        }
    }
}
