using System.ServiceModel;
using System.Collections.Generic;

namespace DBComponent
{
    [ServiceContract]
    public interface IDBServer
    {
        [OperationContract]
        List <Node> getNearestPoint(Node StartNode, Node Finish_Node);
        [OperationContract]
        List<Street> getStreets();
        [OperationContract]
        Node getNodeByAdress(Address addr);
    }
}
