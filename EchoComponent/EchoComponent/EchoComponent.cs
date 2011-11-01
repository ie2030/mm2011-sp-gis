using System.ServiceModel;
using Server;

namespace EchoComponent {

  public class EchoServer:IEchoServer {
    // Simple WCF Service
    public string Echo(string value) {
      return "got: " + value;
    }
  }

  public class EchoComponent: IComponent  {
    // Component which starts EchoServer
    public void start() {
      ServiceHost host = new ServiceHost(typeof(EchoServer));
      host.Open();
    }

  }
}
