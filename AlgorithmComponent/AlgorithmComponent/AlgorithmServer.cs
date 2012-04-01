using System.Collections.Generic;
using DBComponent;

namespace AlgorithmComponent
{
    /// <summary>
    /// Incapsulates logic for finding the shortest path in graph
    /// </summary>
    class AlgorithmServer : IAlgorithmServer
    {
        #region public methods
        /// <summary>
        /// Return shortsest path betweent 2 nodes
        /// </summary>
        public List<Point> getShortestPath(Point start, Point finish)
        {
            List<Point> result = new List<Point>();
            result.Add(start);
            result.Add(finish);
            return result;
        }
        #endregion
    }
}
