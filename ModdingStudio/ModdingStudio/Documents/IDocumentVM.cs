using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModdingStudio;
using ModdingStudio.Applications;

namespace ModdingStudio.Documents
{
    public interface IDocumentVM
    {
        IDocumentView GetView();
        void OnLoaded();
        void OnClosing();
        IAppVM GetSuperiorVM();
        string TitleName { get; set; }
    }
}
