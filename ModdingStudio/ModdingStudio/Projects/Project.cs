using ModdingStudio.Files.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModdingStudio.Projects
{
    public class Project : ITreeViewDataModel
    {
        public TreeViewItemViewModel NewTVVMInstance()
        {
            return new ProjectTVVM(this);
        }
    }
}
