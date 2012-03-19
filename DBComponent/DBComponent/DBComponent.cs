using Server;
using System.ServiceModel;

namespace DBComponent
{
    /// <summary>
    /// Main class of component.
    /// </summary>
    class DBComponent :IComponent
    {
        private ServiceHost host;
        
        #region public methods
        /// <summary>
        /// Starts component.
        /// </summary>
        public void start()
        {
            host = new ServiceHost(typeof(DBServer));
            host.Open();
        }
        #endregion

    }
}
