using ModdingStudio.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Anchorables
{
    public interface IAnchorableView
    {
        IAnchorableVM ViewModel { get; set; }

        IAppVM SuperiorViewModel { get; set; }
    }
}
