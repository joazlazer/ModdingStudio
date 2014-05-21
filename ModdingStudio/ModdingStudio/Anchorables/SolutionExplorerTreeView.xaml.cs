using ModdingStudio.Applications;
using ModdingStudio.Files.TreeView;
using ModdingStudio.Solutions;
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

namespace ModdingStudio.Anchorables
{
    /// <summary>
    /// Interaction logic for SolutionExplorerTreeView.xaml
    /// </summary>
    public partial class SolutionExplorerTreeView : UserControl
    {
        public SolutionExplorerTreeView()
        {
            InitializeComponent();


            ITreeViewDataModel[] solutions = ((MainWindowViewModel)Application.Instance.ApplicationViewModel).CurrentSolution.Files.GetSolutions();
            var newArray = Array.ConvertAll(solutions, item => (Solution)item);
            WorkspaceTVVM viewModel = new WorkspaceTVVM(newArray);
            base.DataContext = viewModel;
        }
    }
}
