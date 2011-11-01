using System;
using EchoClient.ServiceReference;

namespace EchoClient {
  class Program {
    static void Main(string[] args) {
      EchoServerClient proxy = new EchoServerClient();
      string s = "EchoClient";
      Console.WriteLine(s);
      Console.WriteLine(proxy.Echo(s));
      Console.ReadLine();
    }
  }
}
