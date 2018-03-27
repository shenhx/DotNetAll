using System;
using System.Globalization;
using System.Threading;

namespace FarsiLibrary.FX.Utils
{
    /// <summary>
    /// Base culture information
    /// </summary>
    public static class CultureHelper
    {
        private static readonly CultureInfo faCulture = new CultureInfo("fa-IR");
        private static readonly CultureInfo arCulture = new CultureInfo("ar-SA");
        private static readonly CultureInfo neuCulture = CultureInfo.InvariantCulture;
        private static readonly PersianCalendar pc = new PersianCalendar();
        private static readonly HijriCalendar hc = new HijriCalendar();
        private static readonly GregorianCalendar gc = new GregorianCalendar();
        
        /// <summary>
        /// Currently selected UICulture
        /// </summary>
        public static CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
        }

        /// <summary>
        /// Instance of Arabic culture
        /// </summary>
        public static CultureInfo ArabicCulture
        {
            get { return arCulture; }
        }

        /// <summary>
        /// Instance of Farsi culture
        /// </summary>
        public static CultureInfo FarsiCulture
        {
            get { return faCulture; }
        }

        /// <summary>
        /// Instance of Neutral culture
        /// </summary>
        public static CultureInfo NeutralCulture
        {
            get { return neuCulture; }
        }

        /// <summary>
        /// Checks if the current UICulture is set to farsi.
        /// </summary>
        public static bool IsFarsiCulture
        {
            get { return CurrentCulture.Equals(faCulture); }
        }

        /// <summary>
        /// Checks if the current UICulture is set to arabic.
        /// </summary>
        public static bool IsArabicCulture
        {
            get { return CurrentCulture.Equals(arCulture); }
        }

        /// <summary>
        /// Checks if the current UICulture is set to neutral.
        /// </summary>
        public static bool IsDefaultCulture
        {
            get { return CurrentCulture.Equals(neuCulture); }
        }

        /// <summary>
        /// Returns the day of week based on calendar.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public static int GetDayOfWeek(DateTime dt, Calendar calendar)
        {
            if(calendar.Equals(pc))
            {
                PersianDate pd = dt;
                return (int)pd.DayOfWeek;
            }
            else if(calendar.Equals(hc))
            {
                return (int)dt.DayOfWeek;
            }
            else
            {
                return (int)dt.DayOfWeek;
            }
        }

        /// <summary>
        /// Returns the default calendar for the current culture.
        /// </summary>
        /// <returns></returns>
        public static Calendar CurrentCalendar
        {
            get
            {
                if (IsFarsiCulture)
                {
                    return pc;
                }
                else if (IsArabicCulture)
                {
                    return hc;
                }
                else
                {
                    return gc;
                }
            }
        }

        public static DateTime MinCultureDateTime
        {
            get
            {
                if (IsFarsiCulture)
                {
                    return PersianDate.MinValue;
                }
                else
                {
                    return new DateTime(1753, 1, 1);
                }
            }
        }

        public static DateTime MaxCultureDateTime
        {
            get
            {
                if (IsFarsiCulture)
                {
                    return PersianDate.MaxValue;
                }
                else
                {
                    return new DateTime(9998, 12, 31);
                }
            }
        }
    }
}