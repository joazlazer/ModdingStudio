using ModdingStudio.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Commands
{
    public class NewJavaSourceCommand : CommandBase
    {
        // Private variable to store application view model.
        private MainWindowViewModel _vm;

        public NewJavaSourceCommand(MainWindowViewModel vm)
        {
            // Set the vm to the one injected in the contructor.
            this._vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            // Nothing making it not true for now.
            return true;
        }

        public override void Execute(object parameter)
        {
            // Reroute to the main window vm.
            _vm.newJavaSource();
        }
    }
}
