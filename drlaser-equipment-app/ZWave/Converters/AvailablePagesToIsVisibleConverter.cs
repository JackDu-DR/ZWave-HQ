using System.Globalization;
using ZWave.Shared.Enums;

namespace ZWave.Converters
{
    public class AvailablePagesToIsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var permittedPages = (IEnumerable<TabPage>)value;
            var tabPage = (TabPage)parameter;

            return permittedPages != null && permittedPages.Contains(tabPage);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
