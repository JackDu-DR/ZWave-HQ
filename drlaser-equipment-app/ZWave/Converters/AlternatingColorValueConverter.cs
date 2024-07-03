using System.Globalization;

namespace ZWave.Converters
{
    public class AlternatingColorValueConverter : IValueConverter
    {
        public Color EvenNumberColor { get; init; } = Colors.Gray;

        public Color OddNumberColor { get; init; } = Colors.White;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not int intValue
                ? Colors.Transparent
                : (intValue % 2) == 0
                    ? EvenNumberColor
                    : OddNumberColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("AlternatingColorValueConverter.ConvertBack");
        }
    }
}
