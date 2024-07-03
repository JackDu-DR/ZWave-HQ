using System.Globalization;
using ZWave.Models;
using ZWave.Shared.Enums;

namespace ZWave.Converters
{
    public class TabPagePermissionToBooleanConverter : IValueConverter
    {
        private IEnumerable<PermissionModel> _permissionModels;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rolePagePermissions = (IEnumerable<PermissionModel>)value;

            _permissionModels = rolePagePermissions;

            var tabPage = (TabPage)parameter;
            return rolePagePermissions != null && rolePagePermissions.FirstOrDefault(r => r.TabPage == tabPage && r.AccessAllowed) != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_permissionModels == null)
            {
                return null;
            }

            var tabPage = (TabPage)parameter;
            var permission = _permissionModels.FirstOrDefault(r => r.TabPage == tabPage);
            if (permission != null)
            {
                permission.AccessAllowed = (bool)value;
            }

            return _permissionModels;
        }
    }
}
