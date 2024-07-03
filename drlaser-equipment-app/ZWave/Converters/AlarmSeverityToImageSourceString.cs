using CommonLib.Enums;
using System.Globalization;

namespace ZWave.Converters
{
    public class AlarmSeverityToImageSourceString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((AlarmSeverity)value)
            {
                case AlarmSeverity.High:
                    return "high_severity_dot.png";
                case AlarmSeverity.Medium:
                    return "medium_severity_dot.png";
                case AlarmSeverity.Low:
                    return "low_severity_dot.png";
                default:
                    throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
