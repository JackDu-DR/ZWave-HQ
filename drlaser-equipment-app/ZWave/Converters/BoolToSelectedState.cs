using System.Globalization;
using ZWave.Controls;

namespace ZWave.Converters
{
    public class BoolToSelectedState : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = (bool)value;

            return enabled ? StateButton.SelectedState.Left : StateButton.SelectedState.Right;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedState = (StateButton.SelectedState)value;

            return selectedState == StateButton.SelectedState.Left;
        }
    }
}
