using System.ServiceModel;

namespace DBComponent
{
    [ServiceContract]
    public interface IDBServer
    {
        [OperationContract]
        Node getNearestPoint(Node curr);
    }
}
