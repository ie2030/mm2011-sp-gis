using System.Collections.Generic;
using DBComponent;
using System;
using AlgorithmComponent.Algorithmes;
using System.Xml;
using System.IO;
using NLog;
using System.ServiceModel;
using System.Configuration;

namespace AlgorithmComponent
{
    /// <summary>
    /// Incapsulates logic for finding the shortest path in graph
    /// </summary>
    public class AlgorithmServer : IAlgorithmServer
    {
        #region  fields
       
        /// <param name="database">Object of class DBServer</param>
        public DBServer database;  
        /// <param name="logger"> This is the object of class "Logger", 
        /// which has all nesessary methods for logging information about process </param>
        public static Logger logger;
       

        #endregion

        #region Constructor
        
        public AlgorithmServer()
        {
            database = new DBServer();
            logger = LogManager.GetCurrentClassLogger();
        }
        #endregion   

        #region getShortestPath
        ///<summary>
        /// Returns shortsest path betweent 2 nodes
        ///</summary>

        public List<Node> getShortestPath(Node start, Node finish)
         {   
             ///Write information to log.txt
             logger.Info("The method 'AlgorithmComponent.AlgorithmServer.GetShortesrPath', which manages with different shortest-path algorithmes, started./n");

             ///Get the names of algorithmes from config
             List<string> AlgorithmNames = (List<string>)ConfigurationSettings.GetConfig("algorithmes");

             ///Makes different algorithmes find the shortest path
             List<Node> ShortestPathDijkstra=new List<Node>();
             List<Node> ShortestPathLevit = new List<Node>();
             ///List to concat paths, because method return only one list
             List<Node> ShortestPath = new List<Node>();

             AlgorithmLevit Levit = new AlgorithmLevit();
             AlgorithmDijkstra Dijkstra = new AlgorithmDijkstra();
             foreach (string AlgorithmName in AlgorithmNames)
               {   
                  ///Choose nesessary alghorithm, dependent from AlghorithmName. "Swith" is used, because there will be several alghorithmes in the future
                  switch (AlgorithmName)
                    {
                        case "Dijkstra":
                            {
                               
                                ///Returns ShortestPath, found by dijkstra algorithm
                                ShortestPathDijkstra = Dijkstra.Algorithm(start, finish);
                                ///Add the path to one common list
                                ShortestPath.AddRange(ShortestPathDijkstra);
                                ///dot, separating points of paths in list ShortestPath
                                Node dot = new Node(-1,-1);
                                dot.id = -1; 
                                ShortestPath.Add(dot);
                                break;
                            }
                        case "Levit": 
                            {
                                ///Returns ShortestPath, found by dijkstra algorithm
                                ShortestPathLevit = Levit.Algorithm(start, finish);
                                ///Add the path to one common list
                                ShortestPath.AddRange(ShortestPathLevit);
                                ///dot, separating points of paths in list ShortestPath
                                Node dot = new Node(-1,-1);
                                dot.id = -1; 
                                ShortestPath.Add(dot);
                                break;
                              
                            }
                        default:
                            {
                                ///Write error message to log
                                logger.Error("Wrong name of algorithm read from config. This name is: '{0}'. The faultexception will be throwed to client.\n", AlgorithmName);

                                ///Throwing exeption to client
                                
                                ///Writing details of exception
                                ReadingFromConfigFault wrf = new ReadingFromConfigFault();
                                wrf.Operation = "The error during the reading some names of algorithmes from config.";
                                wrf.Problem_message = "Algorithm with this name doesn't exist.";
                                wrf.WrongName=AlgorithmName;
                                string reason = "Wrong name read from config";

                                ///Save some right pathes to sent them to client
                                wrf.RightPathes = ShortestPath;

                                ///comparing time of the algorithmes
                                if (Levit.time > Dijkstra.time)
                                {
                                    double dif = Levit.time.TotalMilliseconds - Dijkstra.time.TotalMilliseconds;
                                    logger.Warn(" Algorithm Dijkstra is faster!!! The difference in time is: '{0}' miliseconds.\n", dif);
                                }
                                else
                                {
                                    double dif = -Levit.time.TotalMilliseconds + Dijkstra.time.TotalMilliseconds;
                                    logger.Warn(" Algorithm Levit is faster!!! The difference in time is: '{0}' milliseconds.\n", dif);
                                }

                                throw new FaultException<ReadingFromConfigFault>(wrf, reason);

                            } 
                    }
                 
               }

             ///Тут должен быть какой-то код, который выберет один из них. 
             ///Пока что пускай рисует оба маршрута, они будут добавлены в один список.

             ///comparing time of the algorithmes
             if (Levit.time > Dijkstra.time)
             {
                 double dif = Levit.time.TotalMilliseconds - Dijkstra.time.TotalMilliseconds;
                 logger.Warn(" Algorithm Dijkstra is faster!!! The difference in time is: '{0}' milliseconds.\n", dif);
             }
             else
             {
                 double dif = -Levit.time.TotalMilliseconds + Dijkstra.time.TotalMilliseconds;
                 logger.Warn(" Algorithm Levit is faster!!! The difference in time is: '{0}' milliseconds.\n", dif);
             }
             
             return ShortestPath;
         }

        #endregion

       


        
    }
}
