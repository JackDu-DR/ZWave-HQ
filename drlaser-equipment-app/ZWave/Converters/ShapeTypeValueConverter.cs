using System.Globalization;
using ZWave.Enums;

namespace ZWave.Converters
{
    public class ShapeTypeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse<ShapeType>((string)value);
        }
    }
}
