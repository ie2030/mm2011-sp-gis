﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Server
{
    /// <summary>
    /// Start the server. Holds the entry point
    /// </summary>
    public class StartServer
    {
        #region public methods
        /// <summary>
        /// Entry point of the application
        /// </summary>
        public static void Main(string[] args)
        {
            List<IComponent> components = (List<IComponent>)ConfigurationSettings.GetConfig("components");
            foreach (IComponent component in components)
            {
                try
                {
                    component.start();
                }
                catch
                {
                    Console.WriteLine("Error while starting " + component.GetType());
                    continue;
                }
                Console.WriteLine(component.GetType() + " is started");
            }
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
        #endregion
    }
}
