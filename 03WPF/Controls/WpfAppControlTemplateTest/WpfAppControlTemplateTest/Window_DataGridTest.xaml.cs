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

namespace WpfAppControlTemplateTest
{
    /// <summary>
    /// Window_DataGridTest.xaml 的交互逻辑
    /// </summary>
    public partial class Window_DataGridTest : Window
    {
        public Window_DataGridTest()
        {
            InitializeComponent();
            List<TestEntity> list = new List<TestEntity>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new TestEntity { RoomID = "A"+i, DeviceIp = "200:10:34:" + i, Applicator = "shx" });
            }
            dataGrid1.ItemsSource = list;
        }
    }

    public class TestEntity
    {
        public string RoomID { get; set; }
        public string DeviceIp { get; set; }
        public string Applicator { get; set; }
    }
}
