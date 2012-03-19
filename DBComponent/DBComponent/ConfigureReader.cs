using System.Configuration;
using System.Xml;
using System.Collections.Generic;

namespace DBComponent
{
    /// <summary>
    /// Incapsulates the logic for getting database configuration 
    /// </summary>
    class ConfigureReader :IConfigurationSectionHandler
    {
        /// <summary>
        /// Reads configuration from config file
        /// </summary>
        public object Create(object parent, object configContext, XmlNode section)
        {
            List<string> config = new List<string>();
            config.Add(section.Attributes["server"].Value);
            config.Add(section.Attributes["name"].Value);
            config.Add(section.Attributes["login"].Value);
            config.Add(section.Attributes["pass"].Value);
            return config;
        }
    }
}
