using ModdingStudio.Application;
using ModdingStudio.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Commands
{
    public class SaveAllCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public SaveAllCommand(MainWindowViewModel vm)
        {
            this._vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            foreach (IDocumentView current in _vm.ActiveDocuments)
            {
                if ((current.GetVM() as IFileVM) != null)
                {
                    IFileVM currentFile = current.GetVM() as IFileVM;
                    if (currentFile.ExistsOnFileSystem)
                        currentFile.SaveFile();
                    else
                    {
                        this._vm.SaveAsCommand.Execute(currentFile);
                    }
                }
            }
        }
    }
}
