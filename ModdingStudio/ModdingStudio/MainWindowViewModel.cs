using ModdingStudio.Anchorables;
using ModdingStudio.Commands;
using ModdingStudio.Documents;
using ModdingStudio.Projects;
using ModdingStudio.Solutions;
using ModdingStudio.Utilities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xceed.Wpf.AvalonDock.Layout;

namespace ModdingStudio.Applications
{
    public class MainWindowViewModel : DependencyObject, IAppVM
    {
        public static string defaultTitleBase = "Modding Studio";
        public static string titleConnector = " - ";
        public static Brush defaultBorderBrush = new SolidColorBrush(Color.FromRgb(104, 33, 121));

        private SolutionExplorer solExp;
        private MainWindow _view;
        private string _titleBase;
        private string _titlePre;
        private Solutions.Solution _currentSolution;

        public MainWindowViewModel(MainWindow window)
        {
            this._view = window;
            this.TitlePre = "";
            this.TitleBase = defaultTitleBase;
            this.OpenFileCommand = new OpenFileCommand(this);
            this.NewJavaSourceCommand = new NewJavaSourceCommand(this);
            this.SaveCommand = new SaveCommand(this);
            this.SaveAsCommand = new SaveAsCommand(this);
            this.SaveAllCommand = new SaveAllCommand(this);
            this.CloseCommand = new CloseCommand(this);
            this.OpenProjectSolutionCommand = new OpenProjectSolutionCommand(this);
            this.NewProjectCommand = new NewProjectCommand(this);
            this.ShowSolutionExplorerCommand = new ShowSolutionExplorerCommand(this);
            this.ActiveDocuments.CollectionChanged += UpdateSaveTexts;
            this.CurrentSolution = new Solution(@"C:/Johnson.txt");
            this.CurrentSolution.loadFromFile();
            MainProjectTypes.RegisterAll();
        }

        public MainWindow View 
        {
            get { return _view; }
            set { _view = value; }
        }

        public void RefreshSE()
        {

        }

        public string TitlePre
        {
            get { return _titlePre; }
            set { _titlePre = value;
                  this.Title = String.IsNullOrEmpty(value.Trim()) ? this.TitleBase : value + titleConnector + this.TitleBase; }
        }

        public string TitleBase
        {
            get { return _titleBase; }
            set { _titleBase = value;
            this.Title = String.IsNullOrEmpty(this.TitlePre.Trim()) ? value : this.TitlePre + titleConnector + value;
            }
        }

        public void DisplayFile(string p)
        {
            string fileName = p.Substring(p.LastIndexOf("\\")).Remove(0, 1);
            string fileExt = fileName.Substring(fileName.LastIndexOf("."));
            switch (fileExt)
            {
                case ".java":
                    {
                        JavaSource java = new JavaSource(p, fileName, this);
                        java.GetVM().OnLoaded();
                        var firstDocumentPane = View.dockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
                        if (firstDocumentPane != null)
                        {
                            firstDocumentPane.Children.Add(java);
                            java.IsSelected = true;
                        }
                        break;
                    }
            }
        }

        public void LayoutPropChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
        {

        }

        public void newJavaSource()
        {
            JavaSource java = new JavaSource("Untitled.java", this);
            java.GetVM().OnLoaded();
            var firstDocumentPane = View.dockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (firstDocumentPane != null)
            {
                firstDocumentPane.Children.Add(java);
                java.IsSelected = true;
            }
        }

        public void NewActiveDocument(IDocumentView newDoc)
        {
            if (newDoc == null) throw new ArgumentNullException();
            if (ActiveDocuments.Contains(newDoc)) ActiveDocuments.Remove(newDoc);
            ActiveDocuments.Push(newDoc);
            this.TitlePre = ActiveDocument.GetVM().TitleName;
        }

        public IDocumentView ActiveDocument
        {
            get { return ActiveDocuments.Peek(); }
        }

        public void DocumentClosed(JavaSource newDoc)
        {
            if (newDoc == null) throw new ArgumentNullException();
            if (ActiveDocuments.Contains(newDoc)) ActiveDocuments.Remove(newDoc);
            if (ActiveDocument != null) this.TitlePre = ActiveDocument.GetVM().TitleName;
            else this.TitlePre = "";
        }

        public void UpdateSaveTexts(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RemovableStack<IFileVM> vms = new RemovableStack<IFileVM>();
            foreach(IDocumentView current in ActiveDocuments)
            {
                if ((current.GetVM() as IFileVM) != null)
                {
                    vms.Push(current.GetVM() as IFileVM);
                }
            }

            if (vms.Peek() != null)
            { 
                this.SaveText = "Save " + vms.Peek().TitleName;
                this.SaveAsText = "Save " + vms.Peek().TitleName + " As...";
            }  
            else
            {
                this.SaveText = "Save";
                this.SaveAsText = "Save As...";
            }
        }


        public void showSolutionExplorer(bool makeActive)
        {
            if(solExp == null)
            {
                solExp = new SolutionExplorer(this);
                solExp.ViewModel.OnLoaded();
                var firstAnchorablePane = View.dockingManager.Layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault();
                if (firstAnchorablePane != null)
                {
                    firstAnchorablePane.Children.Add(solExp);
                }
            }
            else
            {
                solExp.Show();
            }

            if(makeActive)
            {
                solExp.IsActive = true;
                solExp.IsSelected = true;
            }
        }

        public Solution CurrentSolution
        {
            get { return _currentSolution; }
            set { _currentSolution = value; }
        }

        #region DependencyProperties

        public string SaveAsText
        {
            get { return (string)GetValue(SaveAsTextProperty); }
            set { SetValue(SaveAsTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SaveAsText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaveAsTextProperty =
            DependencyProperty.Register("SaveAsText", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata("Save As..."));

        public string SaveText
        {
            get { return (string)GetValue(SaveTextProperty); }
            set { SetValue(SaveTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SaveText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaveTextProperty =
            DependencyProperty.Register("SaveText", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata("Save"));

        public RemovableStack<IDocumentView> ActiveDocuments
        {
            get { return (RemovableStack<IDocumentView>)GetValue(ActiveDocumentsProperty); }
            set { SetValue(ActiveDocumentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveDocuments.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveDocumentsProperty =
            DependencyProperty.Register("ActiveDocuments", typeof(RemovableStack<IDocumentView>), typeof(MainWindowViewModel), new PropertyMetadata(new RemovableStack<IDocumentView>()));

        public Brush WindowBorder
        {
            get { return (Brush)GetValue(WindowBorderProperty); }
            set { SetValue(WindowBorderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowBorder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowBorderProperty =
            DependencyProperty.Register("WindowBorder", typeof(Brush), typeof(MainWindowViewModel), new PropertyMetadata(defaultBorderBrush));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(defaultTitleBase));



        public double BorderOpacity
        {
            get { return (double)GetValue(BorderOpacityProperty); }
            set { SetValue(BorderOpacityProperty, value);
                  this.View.GlowBrush.Opacity = value;
                  this.View.NonActiveGlowBrush.Opacity = value;
                  this.View.BorderBrush.Opacity = value;
            }
        }

        // Using a DependencyProperty as the backing store for BorderOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderOpacityProperty =
            DependencyProperty.Register("BorderOpacity", typeof(double), typeof(MainWindowViewModel), new PropertyMetadata(1.0D));

        #endregion

        #region Commands

        public ICommand OpenFileCommand { get; set; }

        public ICommand NewJavaSourceCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand SaveAsCommand { get; set; }

        public ICommand SaveAllCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand OpenProjectSolutionCommand { get; set; }

        public ICommand NewProjectCommand { get; set; }

        public ICommand ShowSolutionExplorerCommand { get; set; }

        #endregion
    }
}
