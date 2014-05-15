using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Documents
{
    public class JavaSourceViewModel : DocumentViewModel
    {
        private JavaSource _view;
        private string _filePath;
        private string _fileName;

        public JavaSourceViewModel(JavaSource view)
        {
            _view = view;
        }

        new public JavaSource GetView()
        {
            return _view;
        }

        public string FilePath 
        {
            get { return _filePath; }
            set { _filePath = value;
            this.GetView().textBox.Text = JavaSourceProvider.readAllLinesFromFile(this.FilePath);
            }
        }
        public string FileName 
        {
            get { return _fileName; }
            set { _fileName = value;
                    this.GetView().Title = value;
            }
        }
    }
}
