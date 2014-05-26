using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Applications
{
    public static class Workspace
    {
        private static string _defProjDir;

        public static string DefaultProjectDirectory
        {
            get { return _defProjDir; }
            set { _defProjDir = value; }
        }

        public static void Init()
        {
            // Eventually a WinRegistry variable or a User-Options .mssettings (XML) file like in VS2013
            DefaultProjectDirectory = @"C:/Users/joazlazer/Development/mcdev";
        }

        public static void Destruct()
        {

        }
    }
}
