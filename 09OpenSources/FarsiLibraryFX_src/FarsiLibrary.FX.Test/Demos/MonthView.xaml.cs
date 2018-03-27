using System.Windows.Controls;

namespace FarsiLibrary.FX.Test.Demos
{
    /// <summary>
    /// Interaction logic for demoBasicProperties.xaml
    /// </summary>
    public partial class MonthView : Page
    {
        public MonthView()
        {
            InitializeComponent();
        }

        private void cmbDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDirection.SelectedItem != null)
            {
                ComboBoxItem item = cmbDirection.SelectedItem as ComboBoxItem;
                System.Diagnostics.Debug.Assert(item != null);

                string dir = item.Tag as string;
                if (dir == "LTR")
                {
                    mv.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                }
                else if (dir == "RTL")
                {
                    mv.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                }
            }
        }
    }
}
