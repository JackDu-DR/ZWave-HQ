using CommunityToolkit.Mvvm.ComponentModel;
using ZWave.Shared.Enums;

namespace ZWave.Models
{
    public partial class PermissionModel : BaseModel
    {
        [ObservableProperty]
        private UserRole _userRole;

        [ObservableProperty]
        private TabPage _tabPage;

        [ObservableProperty]
        private bool _accessAllowed;

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var permission = (PermissionModel)baseDTO;
            if (permission != null)
            {
                UserRole = permission.UserRole;
                TabPage = permission.TabPage;
                AccessAllowed = permission.AccessAllowed;
            }
        }
    }
}
