using ModdingStudio.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Commands
{
    public class SaveAsCommand : CommandBase
    {
        private Applications.MainWindowViewModel _vm;

        public SaveAsCommand(Applications.MainWindowViewModel vm)
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
                    // Explicitly cast the active document to a file view model.
                    IFileVM file = _vm.ActiveDocument.GetVM() as IFileVM;

                    // Create save file dialog.
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = file.TitleName; // Default file name
                    dlg.DefaultExt = file.FileExt; // Default file extension
                    dlg.Filter = "All Files (*.*)|*.*"; // Filter files by extension
                    dlg.CheckPathExists = true;
                    dlg.AddExtension = true;

                    // Show save file dialog box
                    Nullable<bool> result = dlg.ShowDialog();

                    // Process save file dialog box results
                    if (result == true)
                    {
                        // Save document
                        file.SaveFile(dlg.FileName);
                    }
                }

            }
            else
            {
                // Use document specified in parameter.
                if ((parameter as IFileVM) == null) throw new ArgumentException("Save-as param not a file!!!");
                else
                {
                    // Explicitly cast the document vm from the parameter to a file view model.
                    IFileVM file = parameter as IFileVM;

                    // Create save file dialog.
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = file.TitleName; // Default file name
                    dlg.DefaultExt = file.FileExt; // Default file extension
                    dlg.Filter = "All Files (*.*)|*.*"; // Filter files by extension
                    dlg.CheckPathExists = true;
                    dlg.AddExtension = true;

                    // Show save file dialog box
                    Nullable<bool> result = dlg.ShowDialog();

                    // Process save file dialog box results
                    if(result == true)
                    {
                        // Save document
                        file.SaveFile(dlg.FileName);
                    }
                }
            }
        }
    }
}
