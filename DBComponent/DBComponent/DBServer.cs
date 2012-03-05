using System.Text;
using System.Data.SqlClient;

namespace DBComponent {
  /// <summary>
  /// Incapsulates the logic for working with database
  /// </summary>
  class DBServer : IDBServer{
      #region internal fields
      static internal SqlConnection connection;
      static internal string name, login, pass, server;
      #endregion
      
      #region internal methods  
      /// <summary>
      /// Creates connection to database with settings from config
      /// </summary>
      static internal void createConnection() {
          StringBuilder builder = new StringBuilder( @"Data Source=");
          builder.Append(server);
          builder.Append(";Initial Catalog=");
          builder.Append(name);
          builder.Append(";Persist Security Info=True;User ID=");
          builder.Append(login);
          builder.Append(";Password=");
          builder.Append(pass);
          builder.Append(";Integrated Security=true;");
          connection = new SqlConnection(builder.ToString());
      }
      #endregion

      #region public methods
      /// <summary>
      /// Return nearest point in database
      /// </summery>
      public Point getNearestPoint(Point curr){
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
          if (reader.HasRows) {
              reader.Read();
              res.name = reader.GetSqlString(0).ToString();
          }
          connection.Close();
          return res;
      }
      #endregion  
  }
}
