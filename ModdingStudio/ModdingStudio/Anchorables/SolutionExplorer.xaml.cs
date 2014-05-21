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
using Xceed.Wpf.AvalonDock.Layout;

namespace ModdingStudio.Anchorables
{
    /// <summary>
    /// Interaction logic for SolutionExplorer.xaml
    /// </summary>
    public partial class SolutionExplorer : LayoutAnchorable, IAnchorableView
    {
        private IAnchorableVM _vm;
        private Applications.IAppVM _supVM;

        public SolutionExplorer()
        {
            InitializeComponent();
            this.ViewModel = new SolutionExplorerViewModel(this);
        }
        public SolutionExplorer(Applications.MainWindowViewModel supVM)
        {
            this._supVM = supVM;
            InitializeComponent();
            this.ViewModel = new SolutionExplorerViewModel(this);
            this.Title = "Solution Explorer";
            this.AutoHideWidth = 200;
        }

        public IAnchorableVM ViewModel { get { return _vm; } set { _vm = value; } }


        public IAppVM SuperiorViewModel
        {
            get { return _supVM; }
            set { _supVM = value; }
        }
    }
}
