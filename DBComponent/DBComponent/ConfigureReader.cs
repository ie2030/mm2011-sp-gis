using System.Configuration;
using System.Xml;
namespace DBComponent {

    class ConfigureReader : IConfigurationSectionHandler {
        #region public methods        
        /// <summary>
        /// Read configuration and create connection to database
        /// </summary>
        public object Create(object parent, object configContext, XmlNode section) {
            DBServer.name = section.Attributes["name"].Value;
            DBServer.login = section.Attributes["login"].Value;
            DBServer.pass = section.Attributes["pass"].Value;
            DBServer.server = section.Attributes["server"].Value;
            DBServer.createConnection();
            return null;
        }
        #endregion
    }
}
