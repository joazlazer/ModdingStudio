using ModdingStudio.Application;
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
        private MainWindowViewModel _vm;

        public OpenFileCommand(MainWindowViewModel vm)
        {
            _vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Title = "Open File";
            dlg.Filter = "Java Source Files (*.java)|*.java|All Files(*.*)|*.*";
            dlg.Multiselect = false;

            if (dlg.ShowDialog() != true) { return; }

            _vm.DisplayFile(dlg.FileName);
        }
    }
}
