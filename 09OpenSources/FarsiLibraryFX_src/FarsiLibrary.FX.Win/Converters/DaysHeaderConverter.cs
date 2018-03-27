using System;
using System.Globalization;
using System.Windows.Data;
using FarsiLibrary.FX.Utils;

namespace FarsiLibrary.FX.Win.Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    public class DaysHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                int val = int.Parse(value.ToString());
                return GetDayName(val);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("One way conversion.");
        }

        /// <summary>
        /// Gets DayName based on the culture.
        /// </summary>
        /// <param name="dayValue"></param>
        /// <returns></returns>
        private static string GetDayName(int dayValue)
        {
            if(CultureHelper.IsFarsiCulture)
            {
                return PersianDate.PersianWeekDayAbbr.Default[dayValue];
            }
            else
            {
                return CultureHelper.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[dayValue].Substring(0, 1);
            }
        }
    }
}