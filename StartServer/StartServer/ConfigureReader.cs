using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Reflection;
using System.IO;

namespace Server
{
    /// <summary>
    /// Incapsulates the logic for getting the server components from config
    /// </summary>
    internal class ConfigureReader : IConfigurationSectionHandler
    {
        #region public methods
        /// <summary>
        /// Creates an object using context section
        /// </summary>
        public object Create(object parent, object configContext, XmlNode section)
        {
            List<IComponent> components = new List<IComponent>();
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            foreach (XmlNode node in section.ChildNodes)
                if (node.Name == "component")
                  try {
                    string dllPath = directory + node.Attributes["path"].Value;
                    Assembly assembl = Assembly.LoadFile(dllPath);
                    object curr = Activator.CreateInstance(assembl.GetType(node.Attributes["type"].Value));
                    IComponent component = (IComponent)curr;
                    components.Add(component);
                  }
                  catch (ArgumentNullException) {
                    Console.WriteLine("Error: Component '" + node.Attributes["type"].Value + "' is not found");
                    continue;
                  }
                  catch (NullReferenceException) {
                    Console.WriteLine("Error: Attribute 'type' or 'path' is not found");
                    continue;
                  }
                  catch (InvalidCastException) {
                    Console.WriteLine("Error: " + node.Attributes["type"].Value + " don't implements IComponent");
                    continue;
                  }
                  catch (FileNotFoundException) {
                    Console.WriteLine("Error: " + node.Attributes["path"].Value + " is not found");
                    continue;
                  }
                  catch {
                    Console.WriteLine("Unknown Error");
                    continue;
                  }
            return components;
        }
        #endregion
    }
}