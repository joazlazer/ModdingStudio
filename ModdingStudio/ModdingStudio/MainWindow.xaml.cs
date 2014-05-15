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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModdingStudio;

namespace ModdingStudio.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel _vm;
        public MainWindow()
        {
            _vm = new MainWindowViewModel(this);
            InitializeComponent();
            this.DataContext = this.VM;
        }

        public MainWindowViewModel VM 
        {
            get { return _vm; }
            set { _vm = value; }
        }

        public void LayoutRoot_PropertyChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            _vm.LayoutPropChanging(sender, e);
        }
    }
}
