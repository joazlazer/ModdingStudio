using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModdingStudio
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : MetroWindow
    {
        public NewProjectDialog()
        {
            InitializeComponent();
            //this.BorderThickness = new Thickness(1.0D);
            //this.BorderBrush = new SolidColorBrush(Color.FromRgb(122, 167, 213));
            this.TitleCaps = false;
            this.TitleForeground = new SolidColorBrush(Colors.Black);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
