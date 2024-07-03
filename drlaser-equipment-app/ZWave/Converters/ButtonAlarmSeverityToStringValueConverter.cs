using System.Globalization;
using ZWave.ViewModels;

namespace ZWave.Converters
{
    public class ButtonAlarmSeverityToStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ButtonAlarmSeverity)value == (ButtonAlarmSeverity)Enum.Parse(typeof(ButtonAlarmSeverity), (string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
