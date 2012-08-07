using System.Collections.Generic;
using System.ServiceModel;
using DBComponent;



namespace AlgorithmComponent
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    interface IAlgorithmServer
    {   
       
        ///<summary>
        ///Returns list of nodes, from wich shortest path consists
        ///</summary>
        [OperationContract]
        [FaultContract(typeof(ReadingFromConfigFault))]
        List<Node> getShortestPath(Node start, Node finish);
        
    }
}
