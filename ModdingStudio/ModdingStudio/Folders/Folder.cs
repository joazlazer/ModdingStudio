using ModdingStudio.Files.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Folders
{
    public class Folder : ITreeViewDataModel
    {
        public TreeViewItemViewModel NewTVVMInstance()
        {
            return new FolderTVVM(this);
        }
    }
}
