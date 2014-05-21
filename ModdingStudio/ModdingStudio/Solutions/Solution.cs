using ModdingStudio.Files.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Solutions
{
    public class Solution : ITreeViewDataModel
    {
        private SolutionFiles _files;

        public Solution(string dir)
        {
            this.Directory = dir;
            this.DisplayName = "Solution 'Sample' (0 Projects)";
        }

        public void loadFromFile()
        {
            this._files = new SolutionFiles(this);
        }

        public void onClosing(bool saveToo)
        {
            if (saveToo) this.saveToFIle();
        }

        public void saveToFIle()
        {
            
        }

        private string _directory;

        public string Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }

        public SolutionFiles Files
        {
            get { return _files; }
            set { _files = value; }
        }

        public string DisplayName { get; set; }

        public TreeViewItemViewModel NewTVVMInstance()
        {
            return new SolutionTVVM(this);
        }
    }
}
