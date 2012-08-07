using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBComponent;
using NLog;
using System.Diagnostics;

namespace AlgorithmComponent.Algorithmes
{
   
    public class AlgorithmDijkstra: AbstractRoutingAlgorithm
    {   
        #region fields
       
        int N;
        public bool[] used;
        double[] weight;
        AlgorithmServer AServer;
        /// <summary>
        /// class for measuring speed of algorithm
        /// </summary>
        Stopwatch Clock;
        public TimeSpan time;

        #endregion
        
        #region Constructor
 
        public AlgorithmDijkstra()
        {
            AServer = new AlgorithmServer();
            N = AServer.database.getNodesCount(); 
            logger = LogManager.GetCurrentClassLogger();
            used = new bool[N];
            Clock=new Stopwatch();
        }
       
        #endregion 
       
        #region Public Dijkstra algorithm. 
        ///<summary>
        /// Returns shortest path between two nodes.
        ///</summary>
        /// <param name="start">id of start node</param>
        /// <param name="finish">id of finish node</param>
        public override List<Node> Algorithm(Node StartNode, Node FinishNode)
        {
            ///Start measuring time of this algorithm
            Clock.Start(); 
            ///array with id of nodes, from which shortest path will consist of
            int[] trace = new int[N];
            ///Gets id of start node and finish node
            List<Node> NearestPoint = new List<Node>();
            NearestPoint = AServer.database.getNearestPoint(StartNode, FinishNode);
            int start=NearestPoint[0].id;
            int finish = NearestPoint[1].id;
            logger.Info("The method 'AlgorithmComponent.Algorithmes.AlgorithmDijkstra.Algorithm', which  returns shortest path between two nodes, started.\n");
            weight=base.init(start);  
            while (true)
              {
                int curr = getMinimumVertex();
                if (curr == finish || curr == -1)
                {
                    break;
                 }
                used[curr] = true;
                foreach (Dist e in getEdges(curr))
                  if (!used[e.id_2] && weight[e.id_2] > weight[e.id_1] + e.dist)
                    {
                      weight[e.id_2] = weight[e.id_1] + e.dist;
                      trace[e.id_2] = e.id_1;
                    }
              }

            ///Stop measuring the time of this algorithm
            Clock.Stop();
            time = Clock.Elapsed; 

            return base.buildPath(trace, start, finish);
           }
        #endregion
        
        #region Private GetMinimumVertex. 
        
        /// <summary>
        /// Returns id of node with min weight.
        /// </summary>
        /// <returns></returns>
        private int getMinimumVertex()
          {
            logger.Info("The method 'AlgorithmComponent.Algorithmes.AlgorithmDijkstra.getMinimumVertex', which  returns id of node with min weight, started.\n");
            int minVert = -1;
            double minValue = double.MaxValue / 2;
            for (int i = 0; i < weight.Length; ++i)
              if (!used[i] && minValue > weight[i])
                {
                  minValue = weight[i];
                  minVert = i;
                } 
            logger.Info("Id of vertex with min weight is: {0}\n", minVert);
            return minVert;
          }
        #endregion

        #region Public getEdges.
        
        /// Why do we need this method? Why don't we use only "detEdgesForm"?
        /// <summary>
        /// Returns edges using method "DBServer.getEdgesFrom"
        /// </summary>
        public List<Dist> getEdges(int vertex)
           {
             logger.Info("The method 'AlgorithmComponent.Algorithmes.AlgorithmDijkstra.getEdges', which returns edges using method 'DBServer.getEdgesFrom', started.\n");
             return AServer.database.getEdgesFrom(vertex);
           }
        #endregion
        
        
    } 
}   
