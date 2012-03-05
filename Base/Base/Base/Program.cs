using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Base {
    class Program {
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

        public static void printDistricts(){
            string query = "SELECT * FROM DISTRICTLIST";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                Console.Write(reader.GetInt32(0));
                Console.WriteLine(" " + reader.GetSqlString(1).ToString());
            }
            connection.Close();
        }

        public static void printStreets() {
            string query = "SELECT * FROM STREETLIST";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                Console.Write(reader.GetInt32(0));
                Console.WriteLine(" " + reader.GetSqlString(1).ToString());
            }
            connection.Close();
        }
        static void Main(string[] args) {
            createConnection();
            printDistricts();
            printStreets();
            Console.ReadLine();
        }
    }
}