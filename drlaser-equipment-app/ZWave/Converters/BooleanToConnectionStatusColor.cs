using System.Globalization;
using ZWave.Helpers;

namespace ZWave.Converters
{
    public class BooleanToConnectionStatusColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? ColorsHelper.Green6 : ColorsHelper.Red6;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
