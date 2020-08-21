using System;
using System.Globalization;
using System.Windows.Data;

namespace DataAccessLanguage.Demo
{
    class FloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (float.TryParse(value?.ToString(), out float res))
                return res;
            return null;
        }
    }
}
