using ModdingStudio.Solutions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Files.TreeView
{
    public class WorkspaceTVVM
    {
        readonly Collection<SolutionTVVM> _solutions;
        private Solution[] solutions;

        public WorkspaceTVVM(Solution[] solutions)
        {
            _solutions = new Collection<SolutionTVVM>(
                (from solution in solutions
                 select new SolutionTVVM(solution))
                .ToList());
        }

        public WorkspaceTVVM()
        {
            _solutions = new Collection<SolutionTVVM>();
        }

        public Collection<SolutionTVVM> Solutions
        {
            get { return _solutions; }
        }
    }
}
