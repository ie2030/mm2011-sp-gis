using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace EchoComponent {
  [ServiceContract]
  public interface IEchoServer {
    [OperationContract]
    string Echo(string value);
  }
}
