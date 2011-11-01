using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Server {

  class ConfigureReader:IConfigurationSectionHandler {
    // Class which gets all components from app.config
    public object Create(object parent, object configContext, XmlNode section) {
      List<IComponent> components = new List<IComponent>();
      foreach (XmlNode node in section.ChildNodes)
        if (node.Name == "component")
          try {
            object curr = Activator.CreateInstance(Type.GetType(node.Attributes["type"].Value));
            IComponent component = (IComponent)curr;
            components.Add(component);
          }
          catch (ArgumentNullException) {
            Console.WriteLine("Error: Component '" + node.Attributes["type"].Value + "' is not found");
            continue;
          }
          catch (NullReferenceException) {
            Console.WriteLine("Error: Attribute 'type' is not found");
            continue;
          }
          catch (InvalidCastException) {
            Console.WriteLine("Error: " + node.Attributes["type"].Value + " don't implements IComponent");
            continue;
          }
          catch {
            Console.WriteLine("Unknown Error");
            continue;
          }
      return components;
    }
  }

  class StartServer {
    static void Main(string[] args) {
      List<IComponent> components = (List<IComponent>)ConfigurationSettings.GetConfig("components");
      foreach (IComponent component in components) {
        try {
          component.start();
        }
        catch {
          Console.WriteLine("Error while starting " + component.GetType());
          continue;
        }
        Console.WriteLine(component.GetType() + " is started");
      }
      Console.WriteLine("Press Enter to exit");
      Console.ReadLine();
    }
  }
}
