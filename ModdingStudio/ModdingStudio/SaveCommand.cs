using ModdingStudio.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Commands
{
    public class SaveCommand : CommandBase
    {
        private Application.MainWindowViewModel _vm;

        public SaveCommand(Application.MainWindowViewModel vm)
        {
            this._vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            if (_vm.ActiveDocument != null && (_vm.ActiveDocument.GetVM() as IFileVM) != null) return true;
            else return false;
        }

        public override void Execute(object parameter)
        {
            
            IFileVM file = (IFileVM)_vm.ActiveDocument.GetVM();
            if (file.ExistsOnFileSystem)
                file.SaveFile();
            else
            {
                this._vm.SaveAsCommand.Execute(null);
            }
        }
    }
}
