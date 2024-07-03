using System.Globalization;
using ZWave.Controls;

namespace ZWave.Converters
{
    public class SelectedTabToCommandPanelVisibleConverter : IValueConverter
    {
        private readonly IEnumerable<TabType> _hasCommandPanelTabs = new List<TabType>() { TabType.MotionAxis, TabType.LaserBasic };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tabItem = (CustomTabItem)value;
            return _hasCommandPanelTabs.Contains(tabItem.TabType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
