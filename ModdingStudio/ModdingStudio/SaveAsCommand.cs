using ModdingStudio.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Commands
{
    public class SaveAsCommand : CommandBase
    {
        private Application.MainWindowViewModel _vm;

        public SaveAsCommand(Application.MainWindowViewModel vm)
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
            if (parameter == null)
            {
                // Use currently active document
                if (_vm.ActiveDocument != null && (_vm.ActiveDocument.GetVM() as IFileVM) != null)
                { 
                    IFileVM file = _vm.ActiveDocument.GetVM() as IFileVM; 
                }

            }
            else
            {
                // Use document specified in parameter.
                if ((parameter as IFileVM) == null) throw new ArgumentException("Save-as param not a file!!!");
                else
                {
                    IFileVM file = parameter as IFileVM;
                }
            }
            // Test :P
            _vm.TitleBase = "hhhhhhhhhhh";
        }
    }
}
