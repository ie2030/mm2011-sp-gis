using Server;
using System.ServiceModel;

namespace AlgorithmComponent
{
    /// <summary>
    /// Main class of component
    /// </summary>
    public class AlgorithmComponent : IComponent
    {
        ServiceHost host;

        #region public methods
        /// <summary>
        /// Starts component
        /// </summary>
        public void start()
        {
            host = new ServiceHost(typeof(AlgorithmServer));
            host.Open();
        }
        #endregion
    }
}
