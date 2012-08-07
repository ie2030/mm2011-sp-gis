using Server;
using System.ServiceModel;
using System;
using NLog;
namespace AlgorithmComponent
{   ///What do we connect to?
    /// <summary>
    /// Main class of component
    /// </summary>
    public class AlgorithmComponent :IComponent
    {
        #region Fields

        ServiceHost host;

        public enum AlgorithmType
        {
            Dijkstra, 
            Levit,
            Unknown
        }
        public AlgorithmType AlgorithmName; 
        

        /// <param name="logger"> This is the object of class "Logger", which has all nesessary methods for logging information about process </param>
        public static  Logger logger;
        

        #endregion

        #region Constructor

        public AlgorithmComponent()
          {
            AlgorithmName = new AlgorithmType();
            logger = LogManager.GetCurrentClassLogger();
          }

        #endregion

        #region public methods
        /// <summary>
        /// Starts component
        /// </summary>
        public void start()
        {
            logger.Warn(" Attention! When you push the button 'get_Path' at client, a lot of information will be written here. This is logged information, it's normal and usefull. "); 
            ///Do it need some exception?
            host = new ServiceHost(typeof(AlgorithmServer));
            host.Open();
        }
        #endregion
    }
}
