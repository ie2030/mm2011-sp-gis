using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Base {
    class OnDataBase {
        static SqlConnection connection;
        static internal void createConnection() {
            StringBuilder builder = new StringBuilder(@"Data Source=");
            builder.Append(@".\SQLEXPRESS");
            builder.Append(";Initial Catalog=");
            builder.Append("SpbAddresses");
            builder.Append(";Persist Security Info=True;User ID=");
            builder.Append(@"B52\Yulia");
            builder.Append(";Password=");
            builder.Append("");
            builder.Append(";Integrated Security=true;");
            connection = new SqlConnection(builder.ToString());
        }

        public static Node GetNode(int id) // Возвращает вершину графа с номером id.             
       {
            string commandStr = "SELECT * FROM NODES WHERE id = " + id.ToString() + ";";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Node res = null;            
            while (reader.Read()) {
                res = new Node(reader.GetDouble(1), reader.GetDouble(2));
                res.id = reader.GetInt32(0);
                try {
                    res.prior = reader.GetInt32(3);
                }
                catch { }
                try {
                    res.zone = reader.GetInt32(4);
                }
                catch { } 
            }            
            connection.Close();
            return res;
        }

        public static int GetNearId(double latStr, double lonStr) // Возвращает номер ближайшей 
               //вершины к точке с координатами coordinates.
           {            
               string commandStr = "SELECT TOP 1 * FROM NODES ORDER BY (NODES.lat - " +
                   latStr.ToString() + ")*(NODES.lat - " + latStr.ToString() + ") + (NODES.lon - " + lonStr.ToString() +
                   ")*(NODES.lon - " + lonStr.ToString() + ")";
               SqlCommand command = new SqlCommand(commandStr, connection);
               connection.Open();
               SqlDataReader reader = command.ExecuteReader();
               reader.Read();
               Node res = new Node(reader.GetDouble(1), reader.GetDouble(2));
               res.id = reader.GetInt32(0);
               connection.Close();
               return res.id;
           }

        public static int GetMaxId() // Возвращается максимально возможный номер вершины.
         {
            string commandStr = "SELECT MAX(id) FROM NODES;";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int res = reader.GetInt32(0);
            connection.Close();
            return res;
         }

         public static List<Int32> GiveReverseWay(int id) // Возвращает массив idшников, которые 
             //являются теми вершинами из которых дуги указывают на вершину с данным 
             //номером id.
         {
             string commandStr = "SELECT * FROM DIST WHERE DIST.id_2 = " + id.ToString() + ";";
             SqlCommand command = new SqlCommand(commandStr, connection);
             connection.Open();
             SqlDataReader reader = command.ExecuteReader();

             List<Int32> res = new List<Int32>();
             while (reader.Read()) {                 
                 res.Add(reader.GetInt32(1));
             }
             connection.Close();
             return res;
         }

        public static List<Int32> GiveDirectWay(int id) // Возвращает массив idшников, которые 
             //являются теми вершинами, в которые выходят дуги их данной вершины.
          {
             string commandStr = "SELECT * FROM DIST WHERE DIST.id_1 = " + id.ToString() + ";";
             SqlCommand command = new SqlCommand(commandStr, connection);
             connection.Open();
             SqlDataReader reader = command.ExecuteReader();

             List<Int32> res = new List<Int32>();
             while (reader.Read()) {
                 res.Add(reader.GetInt32(2));
             }
             connection.Close();
             return res;
         } 

        static void Main(string[] args) {
            createConnection();            
            Console.ReadLine();
        }
    }

    public class Node {
        public Node(double _lat, double _lon) {
            lat = _lat;
            lon = _lon;
        }
        public double lat, lon;
        public int id, prior, zone;
    }

    class Address 
    {
        public int id, id_node, id_district, id_street, h_num;
        char corp_num;
        string corp_name;
    }

    class Dist 
    {
        public float dist,time;
        public int id,id_1,id_2;
    }

    class District 
    {
        public string name;
        public int id;
    }

    class DSConnection 
    {
        public int id, id_street, id_district;
    }

    class SName 
    {
        int id, id_node;
        string name;
    }

    class Street 
    {
        public string name,type;
        public int id;
    }
    
}