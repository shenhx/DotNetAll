using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FarsiLibrary.FX.Test.Demos
{
    /// <summary>
    /// Interaction logic for DatePicker.xaml
    /// </summary>
    public partial class DatePicker : Page
    {
        public DatePicker()
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
                    dp.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                }
                else if (dir == "RTL")
                {
                    dp.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                }
            }
        }
    }
}
