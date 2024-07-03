using System.Globalization;

namespace ZWave.Converters
{
    public class BooleanToStringValueConverter : IValueConverter
    {
        private LocalizationResourceManager Localization => LocalizationResourceManager.Instance;

        public bool UseLocalization { get; init; } = false;
        public string TextOnTrue { get; init; } = "True";
        public string TextOnFalse { get; init; } = "False";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (UseLocalization)
            {
                return ((bool)value) ? Localization[TextOnTrue].ToString() : Localization[TextOnFalse].ToString();
            }
            return ((bool)value) ? TextOnTrue : TextOnFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
