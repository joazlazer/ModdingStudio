using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ModdingStudio.Projects.ForgeMods
{
    public class ForgeMod : ProjectType
    {
        public ForgeMod()
        {
            this.ID = "forgeMod";
            this.Name = "Minecraft Forge Mod";
            this.Type = "Java";
            this.DefaultProjectName = "ForgeMod";
            this.Description = "A blank mod that uses Minecraft Forge to interact with Minecraft, allowing for compatibility for other mods. Most mods are of this type.";
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(@"/ModdingStudio;component/Resources/Icons/MCFMod.png", UriKind.Relative);
            logo.EndInit();
            this.Icon = logo;
        }
    }
}
