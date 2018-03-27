using System;
using System.Globalization;
using System.Windows.Data;
using FarsiLibrary.FX.Utils;

namespace FarsiLibrary.FX.Win.Converters
{
    public class YearMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is DateTime)
            {
                DateTime dt = (DateTime)value;
                string mode = "Year";

                if(parameter != null)
                    mode = parameter.ToString().ToUpper();

                switch(mode.ToUpper())
                {
                    case "YEAR":
                        return GetYearValue(dt, culture);

                    case "MONTH":
                        return GetMonthValue(dt);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("One way conversion.");
        }

        private static string GetYearValue(DateTime dt, CultureInfo culture)
        {
            if(CultureHelper.IsFarsiCulture)
            {
                PersianDate pd = dt;
                return pd.Year.ToString(culture);
            }
            else
            {
                return dt.Year.ToString(culture);
            }
        }

        private static string GetMonthValue(DateTime dt)
        {
            if(CultureHelper.IsFarsiCulture)
            {
                PersianDate pd = dt;
                return PersianDate.PersianMonthNames.Default[pd.Month];
            }
            else
            {
                return CultureHelper.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month);
            }
        }
    }
}