using System.Configuration;
using Server;
using System.ServiceModel;

namespace DBComponent {
    /// <summary>
    /// Main class of component.
    /// </summary>
    class DBComponent: IComponent {
        ServiceHost host;

        #region public methods
        /// <summary>
        /// Starts component
        /// </summary>
        public void start() {
            ConfigurationSettings.GetConfig("database");
            host = new ServiceHost(typeof(DBServer));
            host.Open();
        }
        #endregion
    }
}
