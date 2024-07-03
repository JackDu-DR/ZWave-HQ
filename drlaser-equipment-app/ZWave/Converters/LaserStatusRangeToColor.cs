using CommonLib.Enums;
using System.Globalization;
using ZWave.Helpers;

namespace ZWave.Converters
{
    public class LaserStatusRangeToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (LaserStatusRange)value;
            switch (status) {
                case LaserStatusRange.WithinRange:
                    return ColorsHelper.Green6;
                case LaserStatusRange.Warning:
                    return ColorsHelper.Yellow7;
                case LaserStatusRange.OutOfRange:
                    return ColorsHelper.Red6;
                case LaserStatusRange.Unknown:
                    return ColorsHelper.GreyNeutral8;
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
