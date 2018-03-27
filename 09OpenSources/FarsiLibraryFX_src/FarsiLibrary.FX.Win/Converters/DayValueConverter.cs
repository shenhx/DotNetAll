using System;
using System.Globalization;
using System.Windows.Data;
using FarsiLibrary.FX.Utils;
using FarsiLibrary.FX.Win.Controls;

namespace FarsiLibrary.FX.Win.Converters
{
    public class DayValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is CalendarDay)
            {
                CalendarDay cd = (CalendarDay)value;
                DateTime dt = cd.Date;
                if(CultureHelper.IsFarsiCulture)
                {
                    PersianDate pd = dt.Date;
                    return pd.Day;
                }
                else
                {
                    return dt.Day;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}