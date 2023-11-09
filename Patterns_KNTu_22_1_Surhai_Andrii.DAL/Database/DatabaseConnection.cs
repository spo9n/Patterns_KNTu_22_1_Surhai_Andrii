using MySql.Data.MySqlClient;
using System.Data;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database
{
    public class DatabaseConnection
    {
        //private static MySqlConnection _connection;

        //private static string _connectionString = "Server=localhost;Database=db_patterns;Uid=root;Pwd=123321;";


        //private DatabaseConnection() { }

        //public static MySqlConnection Connection
        //{
        //    get
        //    {

        //        if (_connection == null)
        //        {
        //            _connection = new MySqlConnection(_connectionString);
        //        }
        //        if (_connection.State != ConnectionState.Open)
        //        {
        //            _connection.Open();
        //        }
        //        return _connection;
        //    }
        //}


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



        //public static MySqlConnection Connection
        //{
        //    get
        //    {

        //        if (_connection == null)
        //        {
        //            _connection = new MySqlConnection(_connectionString);
        //        }
        //        if (_connection.State != ConnectionState.Open)
        //        {
        //            _connection.Open();
        //        }
        //        return _connection;
        //    }
        //}













        //public static MySqlConnection GetConnection()
        //{
        //    if (_connection == null)
        //    {
        //        _connection = new MySqlConnection(_connectionString);
        //    }
        //    if (_connection.State != ConnectionState.Open)
        //    {
        //        _connection.Open();
        //    }
        //    return _connection;
        //}

        //public void OpenConnection()
        //{
        //    if (_connection.State == ConnectionState.Closed)
        //    {
        //        _connection.Open();
        //    }
        //}

        //public void CloseConnection()
        //{
        //    if (_connection.State == ConnectionState.Open)
        //    {
        //        _connection.Close();
        //    }
        //}

        //public void Dispose()
        //{
        //    if (_connection != null)
        //    {
        //        _connection.Dispose();
        //        _connection = null;
        //    }
        //}

    }
}
