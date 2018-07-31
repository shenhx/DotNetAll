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

namespace WpfAppControlTemplateTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<TestDataGridEntity> list = new List<TestDataGridEntity>();
            list.Add(new TestDataGridEntity { Hello1="hh1",Hello2="hh2"});
            list.Add(new TestDataGridEntity { Hello1="hh2",Hello2="hh4"});
            list.Add(new TestDataGridEntity { Hello1="hh3",Hello2= "hh5" });
            dgv1.ItemsSource = list;
        }
    }

    public class TestDataGridEntity
    {
        public string Hello1 { get; set; }
        public string Hello2 { get; set; }
    }
}
