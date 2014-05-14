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

namespace ModdingStudio.Documents
{
    /// <summary>
    /// Interaction logic for JavaSource.xaml
    /// </summary>
    public partial class JavaSource : DocumentView
    {
        private JavaSourceViewModel _vm;
        public JavaSource()
        {
            InitializeComponent();
            _vm = new JavaSourceViewModel(this);
        }

        private void textBox_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        public JavaSourceViewModel GetVM()
        {
            return _vm;
        }
    }
}
