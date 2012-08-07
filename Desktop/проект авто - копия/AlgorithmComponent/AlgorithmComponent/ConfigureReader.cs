using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using NLog;
using System.ServiceModel;

namespace AlgorithmComponent
{

 internal class ConfigureReader : IConfigurationSectionHandler
    {

        public string AName; 
        /// <summary>
        /// Creates an object using context section
        /// </summary>
        public object Create(object parent, object configContext, XmlNode section)
        {
            /// <param name="logger"> This is the object of class "Logger", which has all nesessary methods for logging information about process </param>
            Logger logger = LogManager.GetCurrentClassLogger();
            ///Write info about the method to log.txt
            logger.Info("The method 'AlgorithmComponent.ConfigureReader.Create', which reads the name of routing algorithm from 'app.config', started.\n");

            ///Reading the name of algorithm from app.config
            List<string> AlgorithmNames=new List<string>();
            try
              {
                foreach (XmlNode node in section.ChildNodes)
                  {   
                    if (node.Name == "algorithm")
                      { 
                        AlgorithmNames.Add(node.Attributes["name"].Value);
                        logger.Info("The name of algorithm was read from config. This name is:'{0}'.", node.Attributes["name"].Value);
                      }
                                
                  }
              }
                       
            catch (Exception Ex)
                {  
                  ///Write error message to log.txt
                  logger.Error(Ex.Message+Ex.HelpLink+Ex.InnerException+Ex.Source+Ex.StackTrace);

                  ///Throwing exeption to client
                  ReadingFromConfigFault rcf = new ReadingFromConfigFault();
                  rcf.Operation = "Can not read the name of algorithm from config";
                  rcf.Problem_message = Ex.Message;
                  rcf.Problem_HelpLink = Ex.HelpLink;
  
                  throw new FaultException<ReadingFromConfigFault>(rcf);
                      
                }
                  
            return AlgorithmNames;
        }
        
    }
}
