﻿using System;
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
    /// Window_BarControl.xaml 的交互逻辑
    /// </summary>
    public partial class Window_BarControl : Window
    {
        public Window_BarControl()
        {
            InitializeComponent();
            kdyBarChartControl.ShowPatientsEvent += ShowPatientsList;
        }

        public void ShowPatientsList(object tag)
        {
            if (tag != null)
            {
                MessageBox.Show(tag.ToString());
            }
        }
    }
}
