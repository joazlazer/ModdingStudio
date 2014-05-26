using ModdingStudio.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ModdingStudio.Projects
{
    /// <summary>
    /// Static class to register all of the project types included in Modding Studio alone
    /// (Without addons which I plan to implement)
    /// </summary>
    public static class MainProjectTypes
    {
        /// <summary>
        /// Registers the default project types.
        /// </summary>
        public static void RegisterAll()
        {
            Reg(new ForgeMods.ForgeMod());
            //Reg("aabba", "Forge Mod", "Java", "ForgeMod", @"/ModdingStudio;component/Resources/Icons/MCFMod.png", "A blank mod that uses Minecraft Forge to interact with Minecraft, allowing for compatibility for other mods. Most mods are of this type.");

            //This eventually will be implemented.
            //Reg("aabbc", "Forge Core Mod", "Java", "CoreMod", @"/ModdingStudio;component/Resources/Icons/CoreMod.png");
        }

        /// <summary>
        /// A factory method (Not really a factory) that initializes a new project type
        /// with the specified properties and returns it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="defaultPN"></param>
        /// <returns></returns>
        public static ProjectType InitType(string id, string name, string type, string defaultPN)
        {
            ProjectType pt = new ProjectType();
            pt.ID = id;
            pt.Name = name;
            pt.Type = type;
            pt.DefaultProjectName = defaultPN;
            return pt;
        }

        /// <summary>
        /// Adds a thumbnail to the project type.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="kind"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static ProjectType AddThumbnail(string location, UriKind kind, ProjectType pt)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(location, kind);
            logo.EndInit();
            pt.Icon = logo;
            return pt;
        }

        /// <summary>
        /// Adds a thumbnail to the project type and configures the uri used
        /// to determine the location of the thumbnail to be indeterminate on whether
        /// it is absolute or relative. This overload is mainly meant for code breverity.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static ProjectType AddThumbnail(string location, ProjectType pt)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(location, UriKind.RelativeOrAbsolute);
            logo.EndInit();
            pt.Icon = logo;
            return pt;
        }

        /// <summary>
        /// Registers the project type with the static class ProjectTypeRegistry.
        /// </summary>
        /// <param name="pt"></param>
        public static void Register(ProjectType pt)
        {
            Projects.ProjectTypeRegistry.RegisterProjectType(pt);
        }

        /// <summary>
        /// Fully registers a new project type.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="defaultPN"></param>
        /// <param name="thumbLocation"></param>
        public static void Reg(string id, string name, string type, string defaultPN, string thumbLocation)
        {
            Register(AddThumbnail(thumbLocation, InitType(id, name, type, defaultPN)));
        }

        public static void Reg(string id, string name, string type, string defaultPN, string thumbLocation, string description)
        {
            ProjectType pt = AddThumbnail(thumbLocation, InitType(id, name, type, defaultPN));
            pt.Description = description;
            Register(pt);
        }

        public static void Reg(ProjectType pt)
        {
            Register(pt);
        }
    }
}
