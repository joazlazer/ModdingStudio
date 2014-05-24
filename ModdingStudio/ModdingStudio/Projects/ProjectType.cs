using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModdingStudio.Projects
{
    public class ProjectType : IProjectLifetimeHandler
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public System.Windows.Media.Imaging.BitmapImage Icon { get; set; }

        public string Type { get; set; }

        public string DefaultProjectName { get; set; }

        public string ManipulatedDPN
        {
            get
            {
                return DefaultProjectName + "1";
            }
        }

        public string Description { get; set; }
    }
}
