using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sample.StyleAndBehaviorTest
{
    public class DataConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return false;
            if (string.IsNullOrEmpty(value.ToString())) return false;
            if (!Regex.IsMatch(value.ToString(), "^[1-9]\\d*$")) return false;
            return Convert.ToInt32(value) < 100;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
