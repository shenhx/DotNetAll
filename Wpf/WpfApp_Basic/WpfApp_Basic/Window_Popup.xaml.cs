using KDY.IP.DOC.Uc;
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
using System.Windows.Shapes;

namespace WpfApp_Basic
{
    /// <summary>
    /// Window_Popup.xaml 的交互逻辑
    /// </summary>
    public partial class Window_Popup : Window
    {
        PopupDragMoveBehavior be = new PopupDragMoveBehavior(0, 0);
        public Window_Popup()
        {
            InitializeComponent();
            
        }

        private void btnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            popupPatients.IsOpen = true;
        }

        private void PateintsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            popupPatients.IsOpen = false;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            be.Attach(popupPatients);
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            be.Detach();
        }
    }
}
