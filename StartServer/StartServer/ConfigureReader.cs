using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using Server;

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
            foreach (XmlNode node in section.ChildNodes)
                if (node.Name == "component")
                    try
                    {
                        object curr = Activator.CreateInstance(Type.GetType(node.Attributes["type"].Value));
                        IComponent component = (IComponent)curr;
                        components.Add(component);
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Error: Component '" + node.Attributes["type"].Value + "' is not found");
                        continue;
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Error: Attribute 'type' is not found");
                        continue;
                    }
                    catch (InvalidCastException)
                    {
                        Console.WriteLine("Error: " + node.Attributes["type"].Value + " don't implements IComponent");
                        continue;
                    }
                    catch
                    {
                        Console.WriteLine("Unknown Error");
                        continue;
                    }
            return components;
        }
        #endregion
    }
}