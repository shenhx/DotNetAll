using System;
using System.ComponentModel;
using System.Globalization;
using FarsiLibrary.FX.Win.ThemeInfo;

namespace FarsiLibrary.FX.Win.Converters
{
    public class ThemeConverter : TypeConverter
    {
        #region Themes

        private static readonly GenericTheme generic;
        private static readonly GlassTheme glass;
        private static readonly ClassicTheme classic;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        static ThemeConverter()
        {
            classic = new ClassicTheme();
            glass = new GlassTheme();
            generic = new GenericTheme();
        }

        #endregion

        #region Methods

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string text = value as string;
            if (text == null)
            {
                return base.ConvertFrom(context, culture, value);
            }

            switch (text.ToLowerInvariant())
            {
                case "classic":
                    return classic;

                case "glass":
                    return glass;

                case "generic":
                    return generic;
            }

            throw new ArgumentException("The specified Theme is not implemented.", "value");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (((destinationType != null) && (value is Theme)) && (destinationType == typeof(string)))
            {
                return value.GetType().Name;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion
    }
}