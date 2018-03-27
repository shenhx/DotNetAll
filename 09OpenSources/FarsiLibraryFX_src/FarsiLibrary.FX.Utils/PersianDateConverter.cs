using System;

namespace FarsiLibrary.FX.Utils
{
    /// <summary>Class to convert PersianDate into normal DateTime value and vice versa.
    /// <seealso cref="PersianDate"/>
    /// </summary>
    /// <remarks>
    /// You can use <c>FarsiLibrary.FX.Utils.FarsiDate.Now</c> property to access current Date.
    /// </remarks>
    public sealed class PersianDateConverter
    {
        #region Fields

        static private readonly double Solar = 365.25;
        static private readonly int GYearOff = 226894;
        static private readonly int[,] gdaytable = new int[,] { { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }, { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 } };
        static private readonly int[,] jdaytable = new int[,] { { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 }, { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 30 } };
        static private readonly string[] weekdays = new string[] { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
        static private readonly string[] weekdaysabbr = new string[] { "ش", "ی", "د", "س", "چ", "پ", "ج" };

        #endregion

        #region Props

        /// <summary>
        /// Array of Day Table for Gregorian Days.
        /// </summary>
        internal static int[,] GDayTable
        {
            get
            {
                return gdaytable;
            }
        }

        /// <summary>
        /// Array of Day Table for Jalali Days.
        /// </summary>
        internal static int[,] JDayTable
        {
            get
            {
                return jdaytable;
            }
        }

        /// <summary>
        /// Array of WeekDay names for Persian Weekdays. This array is a collection of abbreviated weekday names. The abbreviation name is just the first character of normal weekday names.
        /// </summary>
        internal static string[] WeekDaysAbbr
        {
            get
            {
                return weekdaysabbr;
            }
        }

        internal static string[] WeekDays
        {
            get
            {
                return weekdays;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if a specified Persian year is a leap one.
        /// </summary>
        /// <param name="iJYear"></param>
        /// <returns>returns 1 if the year is leap, otherwise returns 0.</returns>
        static private int JLeap(int iJYear)
        {
            //Is jalali year a leap year?
            int tmp;

            Math.DivRem(iJYear, 33, out tmp);
            if ((tmp == 1) || (tmp == 5) || (tmp == 9) || (tmp == 13) || (tmp == 17) || (tmp == 22) || (tmp == 26) || (tmp == 30))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Checks if a specified Gregorian year is a leap one.
        /// </summary>
        /// <param name="GregYear"></param>
        /// <returns>returns 1 if the year is leap, otherwise returns 0.</returns>
        static private int GLeap(int GregYear)
        {
            //Is gregorian year a leap year?
            int Mod4, Mod100, Mod400;

            Math.DivRem(GregYear, 4, out Mod4);
            Math.DivRem(GregYear, 100, out Mod100);
            Math.DivRem(GregYear, 400, out Mod400);

            if (((Mod4 == 0) && (Mod100 != 0)) || (Mod400 == 0))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        static private int GregDays(int iGYear, int iGMonth, int iGDay)
        {
            //Calculate total days of gregorian from calendar base
            int Div4, Div100, Div400;
            int iLeap;
            Div4 = (iGYear - 1) / 4;
            Div100 = (iGYear - 1) / 100;
            Div400 = (iGYear - 1) / 400;
            iLeap = GLeap(iGYear);
            for (int iCounter = 0; iCounter < iGMonth - 1; iCounter++)
            {
                iGDay = iGDay + GDayTable[iLeap, iCounter];
            }

            return ((iGYear - 1) * 365 + iGDay + Div4 - Div100 + Div400);
        }

        static private int JLeapYears(int iJYear)
        {

            int iLeap, iCurrentCycle, Div33;
            int iCounter;

            Div33 = iJYear / 33;
            iCurrentCycle = iJYear - (Div33 * 33);
            iLeap = (Div33 * 8);
            if (iCurrentCycle > 0)
            {
                for (iCounter = 1; iCounter <= 18; iCounter = iCounter + 4)
                {
                    if (iCounter > iCurrentCycle)
                        break;
                    iLeap++;
                }
            }
            if (iCurrentCycle > 21)
            {
                for (iCounter = 22; iCounter <= 31; iCounter = iCounter + 4)
                {
                    if (iCounter > iCurrentCycle)
                        break;
                    iLeap++;
                }

            }
            return iLeap;
        }

        internal static int JalaliDays(int iJYear, int iJMonth, int iJDay)
        {
            //Calculate total days of jalali years from the base calendar
            int iTotalDays, iLeap;

            iLeap = JLeap(iJYear);
            for (int i = 0; i < iJMonth - 1; i++)
            {
                iJDay = iJDay + JDayTable[iLeap, i];
            }

            iLeap = JLeapYears(iJYear - 1);
            iTotalDays = ((iJYear - 1) * 365 + iLeap + iJDay);

            return iTotalDays;
        }

        /// <overloads>Has two overloads.</overloads>
        /// <summary>Converts a Gregorian Date of type <c>System.DateTime</c> class to Persian Date.</summary>
        /// <param name="date">DateTime to evaluate</param>
        /// <returns>string representation of Jalali Date</returns>
        public static PersianDate ToPersianDate(string date)
        {
            return ToPersianDate(Convert.ToDateTime(date));
        }


        /// <summary>
        /// Converts a Gregorian Date of type <c>String</c> and a <c>TimeSpan</c> into a Persian Date.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static PersianDate ToPersianDate(string date, TimeSpan time)
        {
            PersianDate pd = ToPersianDate(date);
            pd.Hour = time.Hours;
            pd.Minute = time.Minutes;
            pd.Second = time.Seconds;

            return pd;
        }

        /// <summary>
        /// Converts a Gregorian Date of type <c>String</c> class to Persian Date.
        /// </summary>
        /// <param name="dt">Date to evaluate</param>
        /// <returns>string representation of Jalali Date.</returns>
        public static PersianDate ToPersianDate(DateTime dt)
        {
            int iGYear, iGMonth, iGDay;
            int iTotalDays, iCounter;
            int iJYear, iJMonth, iJDay, iLeap;

            //DateTime dteDate = Convert.ToDateTime(date);

            iGYear = dt.Year;
            iGMonth = dt.Month;
            iGDay = dt.Day;

            //Calculate total days from the base of gregorian calendar
            iTotalDays = GregDays(iGYear, iGMonth, iGDay);
            iTotalDays = iTotalDays - GYearOff;

            //Calculate total jalali years passed
            iJYear = (int)(iTotalDays / (Solar - 0.25 / 33));
            //Calculate passed leap years
            iLeap = JLeapYears(iJYear);

            //Calculate total days from the base of jalali calendar
            iJDay = iTotalDays - (365 * iJYear + iLeap);
            //Calculate the correct year of jalali calendar
            iJYear++;

            if (iJDay == 0)
            {
                iJYear--;
                if (JLeap(iJYear) == 1)
                {
                    iJDay = 366;
                }
                else
                {
                    iJDay = 365;
                }
            }
            else
            {
                if ((iJDay == 366) && (JLeap(iJYear) != 1))
                {
                    iJDay = 1;
                    iJYear++;
                }
            }


            //Calculate correct month of jalali calendar
            iLeap = JLeap(iJYear);
            for (iCounter = 0; iCounter <= 12; iCounter++)
            {
                if (iJDay <= JDayTable[iLeap, iCounter])
                {
                    break;
                }
                iJDay = iJDay - JDayTable[iLeap, iCounter];
            }

            iJMonth = iCounter + 1;

            return new PersianDate(iJYear, iJMonth, iJDay, dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// Converts a Persian Date of type <c>String</c> to Gregorian Date of type <c>DateTime</c> class.
        /// </summary>
        /// <param name="date">Date to evaluate</param>
        /// <returns>Gregorian DateTime representation of evaluated Jalali Date.</returns>
        public static DateTime ToGregorianDateTime(string date)
        {
            PersianDate pd = new PersianDate(date);
            return Convert.ToDateTime(ToGregorianDate(pd));
        }

        public static DateTime ToGregorianDateTime(PersianDate date)
        {
            return Convert.ToDateTime(ToGregorianDate(date));
        }

        /// <summary>
        /// Converts a Persian Date of type <c>String</c> to Gregorian Date of type <c>String</c>.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Gregorian DateTime representation in string format of evaluated Jalali Date.</returns>
        static public string ToGregorianDate(PersianDate date)
        {
            int iJYear = date.Year;
            int iJMonth = date.Month;
            int iJDay = date.Day;

            //Continue
            int iTotalDays, iGYear, iGMonth, iGDay;
            int Div4, Div100, Div400;
            int iGDays;
            int i, leap;

            iTotalDays = JalaliDays(iJYear, iJMonth, iJDay);
            iTotalDays = iTotalDays + GYearOff;
            iGYear = (int)(iTotalDays / (Solar - 0.25 / 33));

            Div4 = iGYear / 4;
            Div100 = iGYear / 100;
            Div400 = iGYear / 400;

            iGDays = iTotalDays - (365 * iGYear) - (Div4 - Div100 + Div400);
            iGYear = iGYear + 1;

            if (iGDays == 0)
            {
                iGYear--;
                if (GLeap(iGYear) == 1)
                {
                    iGDays = 366;
                }
                else
                {
                    iGDays = 365;
                }
            }
            else
            {
                if (iGDays == 366 && GLeap(iGYear) != 1)
                {
                    iGDays = 1;
                    iGYear++;
                }
            }

            leap = GLeap(iGYear);
            for (i = 0; i <= 12; i++)
            {
                if (iGDays <= GDayTable[leap, i])
                {
                    break;
                }

                iGDays = iGDays - GDayTable[leap, i];
            }

            iGMonth = i + 1;
            iGDay = iGDays;

            return (toDouble(iGMonth) + "/" + toDouble(iGDay) + "/" + iGYear + " " + toDouble(date.Hour) + ":" + toDouble(date.Minute) + ":" + toDouble(date.Second));
        }

        /// <summary>
        /// Adds to single day or months a preceding zero
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static string toDouble(int i)
        {
            if (i > 9)
            {
                return i.ToString();
            }
            else
            {
                return "0" + i;
            }
        }

        internal static string DayOfWeek(PersianDate date)
        {
            if (!date.IsNull)
            {
                DateTime dt = ToGregorianDateTime(date);
                return DayOfWeek(dt);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets Persian Weekday name from specified Gregorian Date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        internal static string DayOfWeek(DateTime date)
        {
            string DayOfWeek = date.DayOfWeek.ToString().ToLower();
            string day;

            switch (DayOfWeek)
            {
                case "saturday":
                    day = PersianDate.PersianWeekDayNames.Default.Shanbeh;
                    break;
                case "sunday":
                    day = PersianDate.PersianWeekDayNames.Default.Yekshanbeh;
                    break;
                case "monday":
                    day = PersianDate.PersianWeekDayNames.Default.Doshanbeh;
                    break;
                case "tuesday":
                    day = PersianDate.PersianWeekDayNames.Default.Seshanbeh;
                    break;
                case "wednesday":
                    day = PersianDate.PersianWeekDayNames.Default.Chaharshanbeh;
                    break;
                case "thursday":
                    day = PersianDate.PersianWeekDayNames.Default.Panjshanbeh;
                    break;
                case "friday":
                    day = PersianDate.PersianWeekDayNames.Default.Jomeh;
                    break;
                default:
                    day = String.Empty;
                    break;
            }

            return (day);
        }

        /// <summary>
        /// Returns number of days in specified month number.
        /// </summary>
        /// <param name="MonthNo">Month no to evaluate in integer</param>
        /// <returns>number of days in the evaluated month</returns>
        internal static int MonthDays(int MonthNo)
        {
            return (JDayTable[1, MonthNo - 1]);
        }

        #endregion
    }
}
