using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModdingStudio.Documents
{
    public interface IDocumentView
    {
        IDocumentVM GetVM();
    }
}
