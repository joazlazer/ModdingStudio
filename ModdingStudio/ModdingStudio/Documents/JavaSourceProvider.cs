using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio
{
    public static class JavaSourceProvider
    {
        public static string readAllLinesFromFile(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public static void saveAllLinesToFile(string path, string lines)
        {
            System.IO.File.WriteAllText(path, lines);
            FileInfo f = new FileInfo(path);
            f.MoveTo(Path.ChangeExtension(path, ".java"));
        }
    }
}
