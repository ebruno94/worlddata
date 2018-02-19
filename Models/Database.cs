using System;
using MySql.Data.MySqlClient;
using WorldData;

namespace WorldData.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
