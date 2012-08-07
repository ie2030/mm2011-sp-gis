using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBComponent;
using NLog;
using System.Diagnostics;


namespace AlgorithmComponent.Algorithmes
{
    class AlgorithmLevit: AbstractRoutingAlgorithm  
      {

        #region fields

        int N;
        AlgorithmServer AServer;
        double[] weight;
        /// <summary>
        /// class for measuring speed of algorithm
        /// </summary>
        Stopwatch Clock;
        public TimeSpan time;

        #endregion 

        #region Constructor

 
        public AlgorithmLevit()
        {
            Clock = new Stopwatch();
            AServer = new AlgorithmServer();
            N = AServer.database.getNodesCount(); 
            logger = LogManager.GetCurrentClassLogger();
           
        }
       
        #endregion 

        #region Public Levit's algorithm. 
        ///<summary>
        /// Returns shortest path between two nodes.
        ///</summary>
        /// <param name="start">id of start node</param>
        /// <param name="finish">id of finish node</param>
        public override List<Node> Algorithm(Node StartNode, Node FinishNode)
        {
            ///Start measuring time of this algorithm
            Clock.Start(); 
            logger.Info("The method 'AlgorithmComponent.Algorithmes.AlgorithmLevit.Algorithm', which returns shortest path between two nodes, started.\n");
            ///Gets id of start node and finish node
            List<Node> NearestPoint = new List<Node>();
            NearestPoint = AServer.database.getNearestPoint(StartNode, FinishNode);
            int start = NearestPoint[0].id;
            int finish = NearestPoint[1].id;
            ///array with id of nodes, from which shortest path will consist of
            int[] trace = new int[N];
            ///array with lengths of shortest paths from start to all vertices
            weight = base.init(start);
            ///The list M0-includes the vertices with allready computed shortest peaths to them from start node
            List<int> M0 = new List<int>();
            ///The list M1-includes the vertices with partially computed shortest peaths to them from start node
            List<int> M1 = new List<int>();
            ///The list M1-includes the vertices with not yet computed shortest peaths to them from start node
            List<int> M2 = new List<int>();

            ///Initialization of lists
            M1.Add(start);
            foreach (Node point in DBServer.nodes) 
                M2.Add(point.id);

            ///id of current vertex
            int curr_id;
            int count=0;
            while (M1 != null)
            {
                curr_id = M1[0];
                M1.RemoveAt(0);
                if (count < 5600)
                {
                    M0.Add(curr_id);
                    count++;
                }
                else
                    break;
                
                foreach (Dist d in AServer.database.getEdgesFrom(curr_id))
                {
                   
                    if (M1.Contains(d.id_2))
                    {
                        if (weight[d.id_2] > weight[d.id_1] + d.dist)
                            weight[d.id_2] = weight[d.id_1] + d.dist;
                    }
                    if (M0.Contains(d.id_2))
                    {
                        if (weight[d.id_2] > weight[d.id_1] + d.dist)
                        {
                            weight[d.id_2] = weight[d.id_1] + d.dist;
                            ///Is this right??
                            M1.Insert(0, d.id_2);
                            M0.Remove(d.id_2);
                        }
                    }
                    if ((!M1.Contains(d.id_2)) & (!M0.Contains(d.id_2)))  
                    {
                        M1.Add(d.id_2);
                        M2.Remove(d.id_2);
                        weight[d.id_2] = weight[d.id_1] + d.dist;
                        trace[d.id_2] = d.id_1;
                    }
                }
                if (curr_id == finish || curr_id == -1)
                    break;   
            }

            ///Stop measuring the time of this algorithm
            Clock.Stop();
            time=Clock.Elapsed; 

            return base.buildPath(trace, start, finish);
        }
        
        #endregion
    }  
}
