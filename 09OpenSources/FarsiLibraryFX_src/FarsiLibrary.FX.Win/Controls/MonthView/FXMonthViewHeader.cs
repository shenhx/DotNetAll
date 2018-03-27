using System.Windows;
using FarsiLibrary.FX.Win.Base;

namespace FarsiLibrary.FX.Win.Controls
{
    public class FXMonthViewHeader : TextCell
    {
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        static FXMonthViewHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXMonthViewHeader), new FrameworkPropertyMetadata(typeof(FXMonthViewHeader)));
        }

        #endregion
    }
}