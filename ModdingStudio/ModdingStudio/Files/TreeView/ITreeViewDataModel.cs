using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModdingStudio.Files.TreeView
{
    public interface ITreeViewDataModel
    {
        TreeViewItemViewModel NewTVVMInstance();
    }
}
