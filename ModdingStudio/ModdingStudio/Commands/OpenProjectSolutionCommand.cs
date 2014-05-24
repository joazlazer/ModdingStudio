using ModdingStudio.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Commands
{
    public class OpenProjectSolutionCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public OpenProjectSolutionCommand(MainWindowViewModel vm)
        {
            // Inject the main window viewmodel (Sometimes called Shell)
            this._vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            // Always enabled.
            return true;
        }

        public override void Execute(object parameter)
        {
            // Disp open file dialog and report back to HQ(SuperiorVM).
        }
    }
}
