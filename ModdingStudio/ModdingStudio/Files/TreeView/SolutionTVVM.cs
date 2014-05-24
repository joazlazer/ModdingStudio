using ModdingStudio.Folders;
using ModdingStudio.Projects;
using ModdingStudio.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Files.TreeView
{
    public class SolutionTVVM : TreeViewItemViewModel
    {
        private Solution _solution;

        public SolutionTVVM(Solution solution) : base(null, true)
        {
            this._solution = solution;
        }

        public Solution Solution
        {
            get { return _solution; }
            set { _solution = value; }
        }

        public string SolutionName
        {
            get { return _solution.DisplayName; }
        }

        protected override void LoadChildren()
        {
            foreach (ITreeViewDataModel context in Solution.Files.GetProjectsAndOthers())
            {
                base.Children.Add(context.NewTVVMInstance());
            }
        }
    }
}
