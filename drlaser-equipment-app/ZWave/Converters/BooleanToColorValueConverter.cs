using System.Globalization;

namespace ZWave.Converters
{
    public class BooleanToColorValueConverter : IValueConverter
    {
        public Color ColorOnTrue { get; init; } = Color.FromArgb("#D8F2DF");

        public Color ColorOnFalse { get; init; } = Color.FromArgb("#D6DBDB");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? ColorOnTrue : ColorOnFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
