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

namespace WpfApp_Basic
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            menuControlList.Add(cobNurseLevel);
            menuControlList.Add(spHello1);
            menuControlList.Add(spHello2);
            menuControlList.Add(spHello3);
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var spObject = sender as StackPanel;
            if(spObject != null)
            {
                e.Handled = true;
                HighLightCurrentControl(spObject);
            }
        }

        List<FrameworkElement> menuControlList = new List<FrameworkElement>();

        private void HighLightCurrentControl(FrameworkElement element)
        {
            foreach (var item in menuControlList)
            {
                if (item.GetType() == typeof(ComboBox))
                {
                    var cobObject = (ComboBox)item;
                    if (element.Name == item.Name)
                    {
                        cobObject.SetBinding(ComboBox.ForegroundProperty, new Binding() { TargetNullValue = new SolidColorBrush(Color.FromRgb(65, 155, 249)) });
                    }
                    else
                    {
                        cobObject.SetBinding(ComboBox.ForegroundProperty, new Binding() { TargetNullValue = new SolidColorBrush(Color.FromRgb(0, 0, 0)) });
                    }
                }
                else if (item.GetType() == typeof(StackPanel))
                {
                    var spObject = (StackPanel)item;
                    if (element.Name == item.Name)
                    {
                        spObject.SetBinding(TextBlock.ForegroundProperty, new Binding() { TargetNullValue = new SolidColorBrush(Color.FromRgb(65, 155, 249)) });
                    }
                    else
                    {
                        spObject.SetBinding(TextBlock.ForegroundProperty, new Binding() { TargetNullValue = new SolidColorBrush(Color.FromRgb(0, 0, 0)) });
                    }
                }
                else
                    continue;
            }
        }

        private void cobNurseLevel_Selected(object sender, RoutedEventArgs e)
        {
            var cobObject = sender as ComboBox;
            if (cobObject != null)
            {
                e.Handled = true;
                HighLightCurrentControl(cobObject);
            }
        }
    }
}
