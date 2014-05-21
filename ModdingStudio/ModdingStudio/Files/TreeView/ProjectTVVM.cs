using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModdingStudio.Files.TreeView
{
    public class ProjectTVVM : TreeViewItemViewModel
    {
        private Projects.Project project;

        public ProjectTVVM(Projects.Project project) : base(null, true)
        {
            // TODO: Complete member initialization
            this.project = project;
        }

        public string ProjectName
        {
            get { return "folder"; }
        }
    }
}
