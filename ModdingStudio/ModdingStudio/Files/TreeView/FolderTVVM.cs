using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModdingStudio.Files.TreeView
{
    public class FolderTVVM : TreeViewItemViewModel
    {
        private Folders.Folder folder;

        public FolderTVVM(Folders.Folder folder) : base(null, true)
        {
            // TODO: Complete member initialization
            this.folder = folder;
        }

        public string FolderName
        {
            get { return "folder"; }
        }
    }
}
