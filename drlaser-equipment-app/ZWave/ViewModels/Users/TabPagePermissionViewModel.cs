using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZWave.Models;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels.Users
{
    public partial class TabPagePermissionViewModel : UserBaseViewModel
    {
        [ObservableProperty]
        private IEnumerable<PermissionModel> _roleAdminPermissions;

        [ObservableProperty]
        private IEnumerable<PermissionModel> _roleOperatorPermissions;

        [ObservableProperty]
        private IEnumerable<PermissionModel> _roleServiceEngineerPermissions;

        [ObservableProperty]
        private IEnumerable<PermissionModel> _roleProductionEngineerPermissions;

        [RelayCommand]
        void Save()
        {
            SavePermissions();
        }

        public TabPagePermissionViewModel()
        {
            RoleAdminPermissions = new List<PermissionModel>();
            RoleOperatorPermissions = new List<PermissionModel>();
            RoleServiceEngineerPermissions = new List<PermissionModel>();
            RoleProductionEngineerPermissions = new List<PermissionModel>();
            GetAllRolePermissionControls();
        }

        private async void SavePermissions()
        {
            LogService.LogUser(LogAction.UserActivity, "User Page Clicked - Saved change role permissions button");
            await PermissionService
                .SetPermissions(RoleAdminPermissions
                .Concat(RoleOperatorPermissions)
                .Concat(RoleServiceEngineerPermissions)
                .Concat(RoleProductionEngineerPermissions));
        }

        private async void GetAllRolePermissionControls()
        {
            var allRolePermissions = await PermissionService.GetAllPermissions();
            RoleAdminPermissions = allRolePermissions.Where(rp => rp.UserRole == UserRole.Admin);
            RoleOperatorPermissions = allRolePermissions.Where(rp => rp.UserRole == UserRole.Operator);
            RoleServiceEngineerPermissions = allRolePermissions.Where(rp => rp.UserRole == UserRole.ServiceEngineer);
            RoleProductionEngineerPermissions = allRolePermissions.Where(rp => rp.UserRole == UserRole.ProductionEngineer);
        }
    }
}
