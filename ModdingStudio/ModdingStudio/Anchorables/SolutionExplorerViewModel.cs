using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModdingStudio.Anchorables
{
    public class SolutionExplorerViewModel : DependencyObject, IAnchorableVM
    {
        private IAnchorableView _view;

        public SolutionExplorerViewModel(SolutionExplorer view)
        {
            this._view = view;
        }

        public IAnchorableView View
        {
            get { return _view; }
            set { _view = value; }
        }

        public void OnLoaded()
        {
            // Do nothing for now.
        }
    }
}
