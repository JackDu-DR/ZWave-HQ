﻿using System.Globalization;
using ZWave.Enums;

namespace ZWave.Converters
{
    public class MotionModeToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse<MotionMode>((string)value);
        }
    }
}
