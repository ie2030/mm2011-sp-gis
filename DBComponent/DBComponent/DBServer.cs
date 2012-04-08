using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
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
        /// Returns nearest point in database
        /// </summary>
        public Node getNearestPoint(Node curr)
        {
            string latStr = curr.lat.ToString(), lngStr = curr.lon.ToString();
            string commandStr = "SELECT TOP 1 * FROM NODES ORDER BY (NODES.lat - " +
                latStr + ")*(NODES.lat - " + latStr + ") + (NODES.lon - " + lngStr +
                ")*(NODES.lon - " + lngStr + ")";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Node res = new Node(reader.GetDouble(1), reader.GetDouble(2));
            res.id = reader.GetInt32(0);
            connection.Close();
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
                catch { }
                try
                {
                    curr.zone = reader.GetInt32(4);
                }
                catch { } 
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
            while (reader.Read())
            {
                res = new Node(reader.GetDouble(1), reader.GetDouble(2));
                res.id = reader.GetInt32(0);
                try
                {
                    res.prior = reader.GetInt32(3);
                }
                catch { }
                try
                {
                    res.zone = reader.GetInt32(4);
                }
                catch { }
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
        #endregion

    }
}
