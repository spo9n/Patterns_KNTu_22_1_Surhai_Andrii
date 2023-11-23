using MySql.Data.MySqlClient;
using System.Data;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance = null;
        private MySqlConnection _connection;
        private static readonly string _connectionString = "Server=localhost;Database=db_patterns;Uid=root;Pwd=123321;";


        private DatabaseConnection()
        {
            _connection = new MySqlConnection(_connectionString);
        }


        public static DatabaseConnection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnection();
                }

                return _instance;
            }
        }


        public MySqlConnection GetConnection()
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new MySqlConnection(_connectionString);
                }
                else if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return _connection;
        }


        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
