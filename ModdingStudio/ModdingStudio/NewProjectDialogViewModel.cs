using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ModdingStudio.Projects;
using System.Collections.ObjectModel;

namespace ModdingStudio
{
    public class NewProjectDialogViewModel : DependencyObject
    {
        private NewProjectDialog _view;

        public NewProjectDialog View
        {
            get { return _view; }
            set { _view = value; }
        }

        public NewProjectDialogViewModel(NewProjectDialog view)
        {
            this.View = view;
                   ProjectTypeRegistry.TypesChanged += OnProjectsChanged;
            foreach (KeyValuePair<string, ProjectType> typePair in ProjectTypeRegistry.RegisteredTypes)
            {
                if (typePair.Key == typePair.Value.ID)
                {
                    if(!ProjectTypes.Contains(typePair.Value)) ProjectTypes.Add(typePair.Value);
                }
            }
        }

        public void OnProjectsChanged(EventArgs e, ProjectType p)
        {
            ProjectTypes.Add(p);
        }

        #region DependencyProperties

        public ObservableCollection<ProjectType> ProjectTypes
        {
            get { return (ObservableCollection<ProjectType>)GetValue(ProjectTypesProperty); }
            set { SetValue(ProjectTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectTypesProperty =
            DependencyProperty.Register("ProjectTypes", typeof(ObservableCollection<ProjectType>), typeof(NewProjectDialogViewModel), new PropertyMetadata(new ObservableCollection<ProjectType>()));

        public string CurrentPropertyType
        {
            get { return (string)GetValue(CurrentPropertyTypeProperty); }
            set { SetValue(CurrentPropertyTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPropertyName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPropertyTypeProperty =
            DependencyProperty.Register("CurrentPropertyType", typeof(string), typeof(NewProjectDialogViewModel), new PropertyMetadata(""));

        public string CurrentPropertyDescription
        {
            get { return (string)GetValue(CurrentPropertyDescriptionProperty); }
            set { SetValue(CurrentPropertyDescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPropertyDescription.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPropertyDescriptionProperty =
            DependencyProperty.Register("CurrentPropertyDescription", typeof(string), typeof(NewProjectDialogViewModel), new PropertyMetadata(""));

        public bool CreateDirForSolution
        {
            get { return (bool)GetValue(CreateDirForSolutionProperty); }
            set { SetValue(CreateDirForSolutionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CreateDirForSolution.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CreateDirForSolutionProperty =
            DependencyProperty.Register("CreateDirForSolution", typeof(bool), typeof(NewProjectDialogViewModel), new PropertyMetadata(true));

        public string ProjectName
        {
            get { return (string)GetValue(ProjectNameProperty); }
            set { SetValue(ProjectNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectNameProperty =
            DependencyProperty.Register("ProjectName", typeof(string), typeof(NewProjectDialogViewModel), new PropertyMetadata(""));

        public string ProjectLocation
        {
            get { return (string)GetValue(ProjectLocationProperty); }
            set { SetValue(ProjectLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProjectLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectLocationProperty =
            DependencyProperty.Register("ProjectLocation", typeof(string), typeof(NewProjectDialogViewModel), new PropertyMetadata(""));

        public SolutionAction SolutionAction
        {
            get { return (SolutionAction)GetValue(SolutionActionProperty); }
            set { SetValue(SolutionActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolutionAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolutionActionProperty =
            DependencyProperty.Register("SolutionAction", typeof(SolutionAction), typeof(NewProjectDialogViewModel), new PropertyMetadata(SolutionAction.Create_New_Solution));

        public string SolutionName
        {
            get { return (string)GetValue(SolutionNameProperty); }
            set { SetValue(SolutionNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolutionName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolutionNameProperty =
            DependencyProperty.Register("SolutionName", typeof(string), typeof(NewProjectDialogViewModel), new PropertyMetadata("")); 

#endregion

        public void onBrowseClicked()
        {

        }

        public void OnListSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ProjectType pt = (ProjectType)e.AddedItems[0];
            this.CurrentPropertyType = pt.Type;
            this.SolutionName = pt.ManipulatedDPN;
            this.ProjectName = pt.ManipulatedDPN;
        }
    }
}
