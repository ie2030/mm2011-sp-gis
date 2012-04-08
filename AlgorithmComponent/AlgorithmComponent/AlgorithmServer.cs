using System.Collections.Generic;
using DBComponent;
using System;
namespace AlgorithmComponent
{
    /// <summary>
    /// Incapsulates logic for finding the shortest path in graph
    /// </summary>
    class AlgorithmServer : IAlgorithmServer
    {
        #region private fields
        
        private int[] trace;
        private double[] weight;
        private bool[] used;
        private DBServer database;

        #endregion

        #region Constructor
        public AlgorithmServer()
        {
            database = new DBServer();
        }
        #endregion

        #region public methods
        /// <summary>
        /// Return shortsest path betweent 2 nodes
        /// </summary>
        public List<Node> getShortestPath(Node start, Node finish)
        {
           int startID = database.getNearestPoint(start).id;
           int finishID = database.getNearestPoint(finish).id;
           dijkstra(startID, finishID);
           return buildPath(startID, finishID);
        }
        #endregion

        #region private methods

        /// <summary>
        /// Initialisation of Dijkstra algorithm
        /// </summary>
        /// <param name="start">id of start node</param>
        private void init(int start)
        {
            int N = database.getNodesCount();
            weight = new double[N];
            trace = new int[N];
            used = new bool[N];

            for (int i = 0; i < weight.Length; i++)
                weight[i] = double.MaxValue / 2;
            weight[start] = 0;
        }
        /// <summary>
        /// Dijkstra algorithm
        /// </summary>
        /// <param name="start">id of start node</param>
        /// <param name="finish">id of finish node</param>
        private void dijkstra(int start, int finish)
        {
            init(start);
            while (true)
            {
                int curr = getMinimumVertex();
                if (curr == finish || curr == -1) return;
                used[curr] = true;
                foreach (Dist e in getEdges(curr))
                    if (!used[e.id_2] && weight[e.id_2] > weight[e.id_1] + e.dist)
                    {
                        weight[e.id_2] = weight[e.id_1] + e.dist;
                        trace[e.id_2] = e.id_1;
                    }
            }
        }
        /// <summary>
        /// Returns id of node with min weight
        /// </summary>
        /// <returns></returns>
        private int getMinimumVertex()
        {
            int minVert = -1;
            double minValue = double.MaxValue / 2;
            for (int i = 0; i < weight.Length; ++i)
                if (!used[i] && minValue > weight[i])
                {
                    minValue = weight[i];
                    minVert = i;
                }
            return minVert;
        }
        /// <summary>
        /// Returns edges 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        private List<Dist> getEdges(int vertex)
        {
            return database.getEdgesFrom(vertex);
        }
        /// <summary>
        /// Returns path from start to finish nodes
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        private List<Node> buildPath(int start, int finish)
        {
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
    }
}
