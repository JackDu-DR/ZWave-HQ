using CommonLib.Enums;
using System.Globalization;
using ZWave.Helpers;

namespace ZWave.Converters
{
    public class OperationalStatusToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (LaserOperation)value;
            switch (status)
            {
                case LaserOperation.Operation:
                    return ColorsHelper.Green6;
                case LaserOperation.Standby:
                    return ColorsHelper.Yellow7;
                case LaserOperation.Housekeeping:
                    return ColorsHelper.Blue5;
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
