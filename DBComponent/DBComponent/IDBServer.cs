using System.ServiceModel;
using System.Collections.Generic;

namespace DBComponent
{
    [ServiceContract]
    public interface IDBServer
    {
        [OperationContract]
        Node getNearestPoint(Node curr);
        [OperationContract]
        List<Street> getStreets();
        [OperationContract]
        Node getNodeByAdress(Address addr);
    }
}
