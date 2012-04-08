using System.Collections.Generic;
using System.ServiceModel;
using DBComponent;

namespace AlgorithmComponent
{
    [ServiceContract]
    interface IAlgorithmServer
    {
        [OperationContract]
        List<Node> getShortestPath(Node start, Node finish); 
    }
}
