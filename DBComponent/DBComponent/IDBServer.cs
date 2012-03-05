using System.ServiceModel;

namespace DBComponent {
    [ServiceContract]
    public interface IDBServer {
        [OperationContract]
        Point getNearestPoint(Point curr);
    }
}
