using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace TransportManagementSystem.util
{
    public class DBUtil
    {
       
        public static string GetConnectionString()
        {
            return "Server=(localdb)\\MSSQLLocalDB;Database=TransportDB;Trusted_Connection=True;";
        }

        public static string GetConnectionStringFromFile(string fileName)
        {
            var config = File.ReadAllLines(fileName)
                .ToDictionary(line => line.Split('=')[0].Trim(), line => line.Split('=')[1].Trim());

            return config["ConnectionString"];
        }

        public static SqlConnection GetConnection()
        {
            string connStr = GetConnectionString();
            return new SqlConnection(connStr);
        }

        public static SqlConnection GetConnection(string fileName)
        {
            string connStr = GetConnectionStringFromFile(fileName);
            return new SqlConnection(connStr);
        }
    }
}
