using ModdingStudio.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Commands
{
    public class NewProjectCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public NewProjectCommand(MainWindowViewModel vm)
        {
            this._vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            //
        }
    }
}
