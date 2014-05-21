using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModdingStudio;

namespace ModdingStudio.Documents
{
    public interface IFileVM : IDocumentVM
    {
        void SaveFile();
        bool ExistsOnFileSystem { get; set; }
        bool IsUnsaved { get; set; }
        string FileExt { get; set; }
    }
}
