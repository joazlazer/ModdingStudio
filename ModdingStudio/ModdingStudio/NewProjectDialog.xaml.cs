using MahApps.Metro.Controls;
using ModdingStudio.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModdingStudio
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : MetroWindow
    {
        private NewProjectDialogViewModel _vm;

        public NewProjectDialog()
        {
            InitializeComponent();
            this.TitleCaps = false;
            this.TitleForeground = new SolidColorBrush(Colors.Black);
            this._vm = new NewProjectDialogViewModel();
            this.AllowsTransparency = true;
            this.DataContext = _vm;

            projectList.SelectedItem = this.VM.ProjectTypes[0];
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        private bool closeStoryBoardCompleted = false;

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!closeStoryBoardCompleted)
            {
                ((Storyboard)FindResource("ExitAnimation")).Begin();
                e.Cancel = true;
            }
        }

        private void closeStoryBoard_Completed(object sender, EventArgs e)
        {
            closeStoryBoardCompleted = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public NewProjectDialogViewModel VM
        {
            get { return _vm; }
            set { _vm = value; }
        }

        private void projectName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vm.ProjectName = projectName.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _vm.onBrowseClicked();
        }

        private void projectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.OnListSelectionChanged(e);
        }

        private void projectLoc_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void solutionName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vm.SolutionName = solutionName.Text;
        }
    }
}
