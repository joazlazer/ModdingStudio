using ModdingStudio.Files.TreeView;
using ModdingStudio.Folders;
using ModdingStudio.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Solutions
{
    public class SolutionFiles
    {
        private Solution _sol;

        public SolutionFiles(Solution sol)
        {
            this.Solution = sol;  
        }

        public Solution Solution
        {
            get { return _sol; }
            set { _sol = value; }
        }

        public ITreeViewDataModel[] GetSolutions()
        {
            return new ITreeViewDataModel[]
            {
                Solution
            };
        }

        public IEnumerable<ITreeViewDataModel> GetProjectsAndOthers()
        {
            return new ITreeViewDataModel[]
            {
                new Folder(),
                new Folder(),
                new Project(),
                new Project()
            };
        }
    }
}
