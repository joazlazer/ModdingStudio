using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModdingStudio.Projects
{
    public static class ProjectTypeRegistry
    {
        private static Dictionary<string, ProjectType> _types = new Dictionary<string, ProjectType>();

        public delegate void TypesChangedEventHandler(EventArgs e, ProjectType newType);
        public static event TypesChangedEventHandler TypesChanged;

        public static Dictionary<string, ProjectType> RegisteredTypes
        {
            get { return _types; }
            set { _types = value; }
        }

        /// <summary>
        /// A method to register a type of property.
        /// 
        /// Note that in any place where a project type is used,
        /// ProjectType typeA;
        /// ProjectTypeRegistry.RegisteredTypes.TryGetValue(typeUsedInContext.ID, typeA);
        /// (typeUsedInContext == typeA) is always true
        /// </summary>
        /// <param name="typeToRegister"></param>
        public static void RegisterPropertyType(ProjectType typeToRegister)
        {
            if (typeToRegister == null) throw new ArgumentNullException();
            if (String.IsNullOrEmpty(typeToRegister.ID.Trim())) throw new ArgumentException("New project type to be registered's ID string is Null! This is... Unacceptable!!!");
            if (!RegisteredTypes.ContainsKey(typeToRegister.ID))
            {
                RegisteredTypes.Add(typeToRegister.ID, typeToRegister);
                if(TypesChanged != null) TypesChanged(new EventArgs(), typeToRegister);
            }
        }
    }
}
