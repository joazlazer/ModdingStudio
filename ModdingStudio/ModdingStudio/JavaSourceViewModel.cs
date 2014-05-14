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
        public JavaSourceViewModel(JavaSource view)
        {
            _view = view;
        }

        public JavaSource GetView()
        {
            return _view;
        }
    }
}
