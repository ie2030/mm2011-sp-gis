using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Base 
{
    class OnDataBase 
    {
        static SqlConnection connection;
        static internal void createConnection() 
        {
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

        public VertexInDateBase GetVertex(int id) // Возвращает вершину графа с номером 
            //id. А именно заполненный экземпляр класса VertexInDateBase у которого в 
            //свою очередь нужно будет заполнить массив исходящих дуг, каждая из которых 
            //представляет экземпляр класса ArcInDateBase.
        {
            string commandStr = "SELECT * FROM NODES WHERE id = " + id.ToString() + ";";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();            
            reader.Read();
            PointCoordinates coord = new PointCoordinates(reader.GetDouble(1), reader.GetDouble(2));
            PriorityVertex prior = PriorityVertex.Object;
            List<ArcInDateBase> arcs = getEdgesFrom(id);
            VertexInDateBase res = new VertexInDateBase(id,coord,prior,arcs);           
            connection.Close();
            return res;
        }

        public static int GetNearId(PointCoordinates coordinates) // Возвращает номер ближайшей 
               //вершины к точке с координатами coordinates.
        {            
            string commandStr = "SELECT TOP 1 * FROM NODES ORDER BY (NODES.lat - " +
                   coordinates.X.ToString() + ")*(NODES.lat - " + coordinates.X.ToString() + ") + (NODES.lon - " 
                   + coordinates.Y.ToString() + ")*(NODES.lon - " + coordinates.Y.ToString() + ")";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();          
            int id = reader.GetInt32(0);
            connection.Close();
            return id;
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
            while (reader.Read()) 
            {                 
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
            while (reader.Read()) 
            {
                res.Add(reader.GetInt32(2));
            }
            connection.Close();
            return res;
        }

        public List<ArcInDateBase> getEdgesFrom(int vertex) // Возвращает массив вершин
            //в которые выходят дуги их данной вершины.
        {
            string commandStr = "SELECT * FROM DIST WHERE DIST.id_1 = " + vertex.ToString() + ";";

            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<ArcInDateBase> res = new List<ArcInDateBase>();
            while (reader.Read()) 
            {
                List<PointCoordinates> track = new List<PointCoordinates>();
                ArcInDateBase curr = new ArcInDateBase(reader.GetInt32(2), reader.GetTimeSpan(4), reader.GetInt32(3), track);           
                res.Add(curr);
            }
            connection.Close();
            return res;
        }

        static void Main(string[] args) 
        {
            createConnection();            
            Console.ReadLine();
        }
    }

//Classes
    class VertexInDateBase 
    {
        public readonly int Id; // Номер этой вершины
        public readonly PointCoordinates Coordinates; // Координаты этой вершины
        public readonly PriorityVertex Priority; // Приоритет этой вершины
        public List<ArcInDateBase> Arcs; // Дуги этой вершины

        public VertexInDateBase(int id, PointCoordinates coordinates, PriorityVertex priorityVertex, List<ArcInDateBase> arcs) 
        {
            if (id == null ||
               coordinates == null ||
               arcs == null)
            {
                throw new ArgumentNullException();
            }
            if (arcs.Count == 0) 
            {
                throw new ArgumentException();
            }

        }
    }

    class ArcInDateBase 
    {
        public readonly int Id; // куда приходит дуга
        public readonly TimeSpan Time; // вес этой дуги
        public readonly double Distance;
        public readonly List<PointCoordinates> Track; // Координаты отображения дуги

        public ArcInDateBase(int id, TimeSpan time, double distance, List<PointCoordinates> track) 
        {
            if (id == null ||
                time == null ||
                track == null ||
                distance == null) 
            {
                throw new ArgumentNullException();
            }
            if (track.Count < 2) 
            {
                throw new ArgumentException();
            }

            this.Distance = distance;
            this.Id = id;
            this.Time = time;
            this.Track = track;
        }
    }

    public enum PriorityVertex 
    {
        Object, // Строение или любой объект вне дороги
        Road // Дорога
    }

    class PointCoordinates 
    {
        public readonly double X;
        public readonly double Y;

        public static double Distance(PointCoordinates point1, PointCoordinates point2) 
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public double Distance(PointCoordinates point2) 
        {
            return Math.Sqrt(Math.Pow(X - point2.X, 2) + Math.Pow(Y - point2.Y, 2));
        }

        public PointCoordinates(double x, double y) 
        {
            if (x == null ||
                y == null) 
            {
                throw new ArgumentNullException();
            }
            this.X = x;
            this.Y = y;
        }       
    }

  /*  public class Node 
    {
        public Node(double _lat, double _lon) 
        {
            lat = _lat;
            lon = _lon;
        }
        public double lat, lon;
        public int id, prior, zone;
    }*/

    class Address 
    {
        public int id, id_node, id_district, id_street, h_num;
        char corp_num;
        string corp_name;
    }

    class Dist 
    {
        public double dist;
        public TimeSpan time;
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
