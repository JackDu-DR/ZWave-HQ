using System.Globalization;
using ZWave.Helpers;

namespace ZWave.Converters
{
    public class BooleanToEmissionStatusColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? ColorsHelper.Red6 : ColorsHelper.Green6;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
