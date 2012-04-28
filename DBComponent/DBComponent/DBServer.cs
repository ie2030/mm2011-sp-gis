using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace DBComponent
{
    /// <summary>
    /// Incapsulates the logic for working with database
    /// </summary>
    public class DBServer :IDBServer
    {
        SqlConnection connection;

        #region public methods
        /// <summary>
        /// Constructor. Creates connection to the database
        /// </summary>
        public DBServer()
        {
            List<string> config = (List<string>)ConfigurationSettings.GetConfig("database");
            StringBuilder builder = new StringBuilder(@"Data Source=");
            builder.Append(config[0]);
            builder.Append(";Initial Catalog=");
            builder.Append(config[1]);
            builder.Append(";Persist Security Info=True;User ID=");
            builder.Append(config[2]);
            builder.Append(";Password=");
            builder.Append(config[3]);
            builder.Append(";Integrated Security=true;");
            connection = new SqlConnection(builder.ToString());
        }

        /// <summary>
        /// Returns nearest point on road in database
        /// </summary>
        public Node getNearestPoint(Node curr)
        {
            List<Node> nodes = getNodes();
            Node res = null;
            double dist = double.MaxValue;
            foreach (Node node in nodes)
            {
                if (node.prior == 0) 
                    continue;
                double currDist = countDist(curr, node);
                if (currDist < dist)
                {
                    res = node;
                    dist = currDist;
                }
            }
            return res;
        }

        public int getNodesCount()
        {
            string commandStr = "SELECT MAX(id) FROM NODES;";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int res = reader.GetInt32(0);
            connection.Close();
            return res + 1;
        }

        public List<Node> getNodes()
        {
            string commandStr = "SELECT * FROM NODES;";

            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Node> res = new List<Node>();
            while (reader.Read())
            {
                Node curr = new Node(reader.GetDouble(1), reader.GetDouble(2));
                curr.id = reader.GetInt32(0);
                try
                {
                    curr.prior = reader.GetInt32(3);
                }
                catch { curr.prior = 0; }
                try
                {
                    curr.zone = reader.GetInt32(4);
                }
                catch { curr.zone = 0; } 
                res.Add(curr);
            }
            connection.Close();
            return res;
        }

        public Node getNode(int id)
        {
            string commandStr = "SELECT * FROM NODES WHERE id = " + id.ToString() + ";";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Node res = null;
            if (reader.Read())
            {
                res = new Node(reader.GetDouble(1), reader.GetDouble(2));
                res.id = reader.GetInt32(0);
                try
                {
                    res.prior = reader.GetInt32(3);
                }
                catch { res.prior = 0; }
                try
                {
                    res.zone = reader.GetInt32(4);
                }
                catch { res.zone = 0; }
            }
            connection.Close();
            return res;
        }

        public List<Dist> getEdges()
        {
            string commandStr = "SELECT * FROM DIST;";

            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Dist> res = new List<Dist>();
            while (reader.Read())
            {
                Dist curr = new Dist();
                curr.id = reader.GetInt32(0);
                curr.id_1 = reader.GetInt32(1);
                curr.id_2 = reader.GetInt32(2);
                curr.dist = reader.GetDouble(3);
                curr.time = reader.GetDouble(4);
                res.Add(curr);
            }
            connection.Close();
            return res;
        }

        public List<Dist> getEdgesFrom(int vertex)
        {
            string commandStr = "SELECT * FROM DIST WHERE DIST.id_1 = " + vertex.ToString() + ";";

            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Dist> res = new List<Dist>();
            while (reader.Read())
            {
                Dist curr = new Dist();
                curr.id = reader.GetInt32(0);
                curr.id_1 = reader.GetInt32(1);
                curr.id_2 = reader.GetInt32(2);
                curr.dist = reader.GetDouble(3);
                curr.time = reader.GetDouble(4);
                res.Add(curr);
            }
            connection.Close();
            return res;
        }

        public List<Street> getStreets()
        {
            string commandStr = "SELECT * FROM STREETLIST;";

            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Street> res = new List<Street>();
            while (reader.Read())
            {
                Street curr = new Street();
                curr.id = reader.GetInt32(0);
                curr.name = reader.GetString(1);
                curr.type = reader.GetString(2);
                res.Add(curr);
            }
            connection.Close();
            return res;
        }

        public Node getNodeByAdress(Address addr)
        {
            string commandStr = String.Format("select * from NODES join ADDRESS on NODES.id = ADDRESS.id_node where ADDRESS.id_street = {0}", addr.id_street);
            if (addr.h_num != -1)
                commandStr += " and ADDRESS.h_num = " + addr.h_num.ToString();

            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Node res = null;
            if (reader.Read())
            {
                res = new Node(reader.GetDouble(1), reader.GetDouble(2));
                res.id = reader.GetInt32(0);
                try
                {
                    res.prior = reader.GetInt32(3);
                }
                catch { res.prior = 0; }
                try
                {
                    res.zone = reader.GetInt32(4);
                }
                catch { res.zone = 0; }
            }

            connection.Close();
            return res;
        }

        #endregion

        #region private methods
        private double countDist(Node st, Node ed)
        {
            double toRad = Math.PI / 180;
            double lat1 = st.lat * toRad, lng1 = st.lon * toRad, lat2 = ed.lat * toRad, lng2 = ed.lon * toRad;
            double temp = Math.Sin((lat2 - lat1) / 2);
            temp *= temp;
            temp += Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin((lng2 - lng1) / 2) * Math.Sin((lng2 - lng1) / 2);
            temp = Math.Sqrt(temp);
            temp = 2 * Math.Asin(temp);
            return temp * 6372795;
        }
        #endregion
    }
}
