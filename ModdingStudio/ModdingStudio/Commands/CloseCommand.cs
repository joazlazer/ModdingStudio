using ModdingStudio.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Layout;

namespace ModdingStudio.Commands
{
    public class CloseCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public CloseCommand(MainWindowViewModel vm)
        {
            this._vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            if (_vm.ActiveDocument != null && (_vm.ActiveDocument as LayoutDocument) != null) return true;
            else return false;
        }

        public override void Execute(object parameter)
        {
            ((LayoutDocument)_vm.ActiveDocument).Close();
        }
    }
}
