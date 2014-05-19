using ModdingStudio.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using ModdingStudio.Documents;
using ModdingStudio.Utilities;
using Xceed.Wpf.AvalonDock.Layout;

namespace ModdingStudio.Application
{
    public class MainWindowViewModel : DependencyObject, IAppVM
    {
        public static string defaultTitleBase = "Modding Studio";
        public static string titleConnector = " - ";
        public static Brush defaultBorderBrush = new SolidColorBrush(Color.FromRgb(104, 33, 121));

        private MainWindow _view;
        private string _titleBase;
        private string _titlePre;

        public MainWindowViewModel(MainWindow window)
        {
            this._view = window;
            this.TitlePre = "";
            this.TitleBase = defaultTitleBase;
            this.OpenFileCommand = new OpenFileCommand(this);
            this.NewJavaSourceCommand = new NewJavaSourceCommand(this);
            this.SaveCommand = new SaveCommand(this);
            this.ActiveDocuments.CollectionChanged += UpdateSaveTexts;
        }

        public MainWindow View 
        {
            get { return _view; }
            set { _view = value; }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(defaultTitleBase));

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

        public Brush WindowBorder
        {
            get { return (Brush)GetValue(WindowBorderProperty); }
            set { SetValue(WindowBorderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowBorder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowBorderProperty =
            DependencyProperty.Register("WindowBorder", typeof(Brush), typeof(MainWindowViewModel), new PropertyMetadata(defaultBorderBrush));

        public ICommand OpenFileCommand { get; set; }

        public ICommand NewJavaSourceCommand { get; set; }

        public ICommand SaveCommand { get; set; }

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
            this.SaveText = "Save " + vms.Peek().TitleName;
        }
    }
}
