using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModdingStudio.Commands
{
    public class OpenFileCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public override void Execute(object parameter)
        {
            
        }
    }
}
