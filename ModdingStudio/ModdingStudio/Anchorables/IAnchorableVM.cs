using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Anchorables
{
    public interface IAnchorableVM
    {
        IAnchorableView View { get; set; }

        void OnLoaded();
    }
}
