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
    public class MainWindowViewModel : ApplicationViewModel
    {
        public static string defaultTitleBase = "Modding Studio";
        public static string titleConnector = " - ";
        public static Brush defaultBorderBrush = new SolidColorBrush(Color.FromRgb(104, 33, 121));

        private MainWindow _view;
        private string _titleBase;
        private string _titlePre;
        private RemovableStack<DocumentViewModel> _orderedFocusedDocs;

        public MainWindowViewModel(MainWindow window)
        {
            this._view = window;
            this.TitlePre = "";
            this.TitleBase = defaultTitleBase;
            this.OpenFileCommand = new OpenFileCommand(this);
            this.FocusedDocuments = new RemovableStack<DocumentViewModel>();
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

        public void DisplayFile(string p)
        {
            string fileName = p.Substring(p.LastIndexOf("\\")).Remove(0, 1);
            string fileExt = fileName.Substring(fileName.LastIndexOf("."));
            switch (fileExt)
            {
                case ".java":
                    {
                        JavaSource java = new JavaSource(p, fileName);
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

        public RemovableStack<DocumentViewModel> FocusedDocuments 
        {
            get { return _orderedFocusedDocs; }
            set { _orderedFocusedDocs = value; }
        }

        public DocumentViewModel LastFocusedDocument 
        {
            get { return _orderedFocusedDocs.Peek(); }
        }

        public void NewLastFocusedDocument()
        {
            DocumentView docV = (DocumentView)this.View.dockingManager.Layout.LastFocusedDocument;
            DocumentViewModel docVM = docV.GetVM();
            if (FocusedDocuments.Contains(docVM))
            {
                FocusedDocuments.Remove(docVM);
            }
            FocusedDocuments.Push(docVM);
        }

        public void LayoutPropChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "LastFocusedDocument" && this.View.dockingManager.Layout.LastFocusedDocument != null && (this.View.dockingManager.Layout.LastFocusedDocument as DocumentView) != null)
            {
                NewLastFocusedDocument();
            }
        }
    }
}
