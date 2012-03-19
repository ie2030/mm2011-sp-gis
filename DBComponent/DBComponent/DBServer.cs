using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
namespace DBComponent
{
    /// <summary>
    /// Incapsulates the logic for working with database
    /// </summary>
    class DBServer :IDBServer
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
        public Point getNearestPoint(Point curr)
        {
            string latStr = curr.lat.ToString(), lngStr = curr.lng.ToString();
            string commandStr = "SELECT TOP 1 * FROM NODES ORDER BY (NODES.lat - " +
                latStr + ")*(NODES.lat - " + latStr + ") + (NODES.lon - " + lngStr +
                ")*(NODES.lon - " + lngStr + ")";
            SqlCommand command = new SqlCommand(commandStr, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Point res = new Point(reader.GetDouble(1), reader.GetDouble(2));
            int id = reader.GetInt32(0);
            commandStr = "SELECT name FROM SNAME WHERE SNAME.id_note = " + id.ToString() + ";";
            command.CommandText = commandStr;
            reader.Close();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                res.name = reader.GetSqlString(0).ToString();
            }
            connection.Close();
            return res;
        }
        #endregion

    }
}
