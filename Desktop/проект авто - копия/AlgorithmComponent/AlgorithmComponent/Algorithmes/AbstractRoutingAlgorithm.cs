using System;
using System.Collections.Generic;
using System.Text;
using DBComponent;
using NLog;   

namespace AlgorithmComponent.Algorithmes
{
    public abstract class AbstractRoutingAlgorithm
    {
        #region fields

        /// <param name="database">Object of class DBServer</param>
        public DBServer database;
        /// <param name="logger"> This is the object of class "Logger", 
        /// which has all nesessary methods for logging information about process </param>
        static public Logger logger;

        #endregion

        #region Constructor
        
        public AbstractRoutingAlgorithm()
        {
            database = new DBServer();
            logger = LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region algorithm

        /// <summary>
        /// abstract algorithm for getting shortest path
        /// </summary>
        /// <param name="start">start node</param>
        /// <param name="finish">finish node</param>
        /// <returns></returns>
        public abstract List<Node> Algorithm(Node start, Node finish);

        #endregion

        #region buildPath
        /// <summary>
       ///  Returns path from start to finish nodes
       /// </summary>
       /// <param name="start">id of start node</param>
       /// <param name="finish">id of finish node</param>
       /// <param name="trace">array with id of nodes, from which shortest path consists of</param>
       /// <returns></returns>
       /// Is it ok to write this method here, but call him from classes of algorithmes?
       public List<Node> buildPath(int[] trace, int start, int finish)
       {
           logger.Info("The method 'AlgorithmComponent.Algorithmes.buildPath', which makes path from list of node's id, started.\n");
           List<Node> path = new List<Node>();
           int currVertex = finish;
           while (currVertex != start)
           {
               if (currVertex == 0) return null;
               path.Add(database.getNode(currVertex));
               currVertex = trace[currVertex];
           }
           path.Add(database.getNode(start));
           path.Reverse();
           return path;
       }
      #endregion

        #region init
        ///<summary>
        /// Initialisation of array with lengths of shortest paths from start to all vertices
        /// </summary>
        /// <param name="start">id of start node</param>
        public double[] init(int start)
        {
           logger.Info("The method 'AlgorithmComponent.Algorithmes.AlgorithmDijkstra.init', which  initialise of array with lengths of shortest paths from start to all vertices, started.\n");
           int N = database.getNodesCount();
           double[] weight = new double[N];
           for (int i = 0; i < weight.Length; i++)
               weight[i] = double.MaxValue / 2;
           weight[start] = 0;
           ///How output the massiv?
           ///logger.Info("This is the array of weights: ");
           return (weight);
       }

       #endregion
    }
}
