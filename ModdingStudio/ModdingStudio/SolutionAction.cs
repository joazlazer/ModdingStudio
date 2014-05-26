using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio
{
    public enum SolutionAction
    {
        [Description("Create New Solution")]
        CreateNewSolution,
        [Description("Add To Solution")]
        AddToSolution
    }
}
