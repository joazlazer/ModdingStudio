using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModdingStudio.Commands
{
    public class CommandBase : ICommand
    {
        public virtual bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
