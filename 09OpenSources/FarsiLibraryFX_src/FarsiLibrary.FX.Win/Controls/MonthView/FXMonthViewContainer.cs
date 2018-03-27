using System.Windows.Controls;

namespace FarsiLibrary.FX.Win.Controls
{
    public class FXMonthViewContainer : ListBox
    {
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is FXMonthViewItem;
        }

        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {
            return new FXMonthViewItem();
        }
    }
}