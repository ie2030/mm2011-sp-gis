using System.Collections.Generic;
using System.ServiceModel;
using DBComponent;

namespace AlgorithmComponent
{
    [ServiceContract]
    interface IAlgorithmServer
    {
        [OperationContract]
        List<Point> getShortestPath(Point start, Point finish); 
    }
}
