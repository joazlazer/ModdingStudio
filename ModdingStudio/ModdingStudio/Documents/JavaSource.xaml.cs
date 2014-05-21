using ModdingStudio.Applications;
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
    public partial class JavaSource : IDocumentView
    {
        public Brush brushA = new SolidColorBrush(Color.FromRgb(20, 20, 200));
        public Brush brushB = new SolidColorBrush(Color.FromRgb(200, 40, 40));
        public Brush brushC = new SolidColorBrush(Color.FromRgb(20, 200, 40));
        public Brush brushD = new SolidColorBrush(Color.FromRgb(70, 157, 35));

        private JavaSourceViewModel _vm;
        internal IAppVM _supVM;

        public JavaSource(string path, string fileName, IAppVM supVM)
        {
            InitializeComponent();
            _vm = new JavaSourceViewModel(this, path);
            this._vm.FileName = fileName;
            this._supVM = supVM;
        }

        public JavaSource(string fileName, IAppVM supVM)
        {
            InitializeComponent();
            _vm = new JavaSourceViewModel(this);
            this._vm.FileName = fileName;
            this._supVM = supVM;
            this._vm.ExistsOnFileSystem = false;
        }

        private void textBox_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (_vm != null) _vm.OnTextChanged(sender, e);
        }

        new public IDocumentVM GetVM()
        {
            return _vm;
        }

        private void DocumentView_IsActiveChanged(object sender, EventArgs e)
        {
            //this.rectA.Fill = this.IsActive ? brushA : brushB;
            ((MainWindowViewModel)_supVM).NewActiveDocument(this);
        }

        private void DocumentView_IsSelectedChanged(object sender, EventArgs e)
        {
            //this.rectB.Fill = this.IsSelected ? brushC : brushD;
            //Random rand  = new Random();
            //if (this.IsSelected == false) ((MainWindowViewModel)_supVM).WindowBorder = new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)));
            //else ((MainWindowViewModel)_supVM).TitleBase = "Modding Studio " + (rand.Next(30) * rand.Next(30) * rand.Next(30)).ToString();
        }

        public override string ToString()
        {
            return this._vm.TitleName;
        }

        private void LayoutDocument_Closed(object sender, EventArgs e)
        {
            ((MainWindowViewModel)_supVM).DocumentClosed(this);
        }
    }
}
