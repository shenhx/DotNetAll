using System.Windows;
using System.Windows.Controls;

namespace FarsiLibrary.FX.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ThemeInfo prevTheme = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public App MyApplication
        {
            get { return Application.Current as App; }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = cmbThemes.SelectedValue as ComboBoxItem;
            
            
            if(item != null && item.Tag != null)
            {
                string themeName = item.Tag as string;
                ThemeInfo theme = MyApplication.AvailableThemes[themeName];
                Application.Current.Resources = theme.SkinTheme;

                if (prevTheme != null && Application.Current.Resources.MergedDictionaries.Contains(prevTheme.SystemTheme))
                    Application.Current.Resources.MergedDictionaries.Remove(prevTheme.SystemTheme);

                if (!Application.Current.Resources.MergedDictionaries.Contains(theme.SystemTheme))
                    Application.Current.Resources.MergedDictionaries.Add(theme.SystemTheme);

                prevTheme = theme;
            }
            else
            {
                Application.Current.Resources = null;
            }
        }
    }
}
