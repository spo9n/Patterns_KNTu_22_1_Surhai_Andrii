using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class BrandDAO : IBrandDAO
    {
        private DAOObserver _observer = new DAOObserver();
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO brands(name) VALUES (@Name);";
        private const string _updateSQL = "UPDATE brands SET name = @Name WHERE brand_id = @Id;";
        private const string _deleteSQL = "DELETE FROM brands WHERE brand_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM brands WHERE brand_id = @Id;";
        private const string _selectAllSQL = "SELECT * FROM brands;";


        public BrandDAO()
        {
            _observer.Attach(new Logger());
        }
        

        public void Create(Brand brand)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@Name", brand.Name);
                object result = _command.ExecuteScalar();

                _observer.Notify($"INFO. Brand with name = {brand.Name} was CREATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public void Update(Brand brand)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@Name", brand.Name);
                _command.Parameters.AddWithValue("@Id", brand.Id);
                object result = _command.ExecuteScalar();

                _observer.Notify($"INFO. Brand with brand_id = {brand.Id} was UDPATED with name = {brand.Name}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _deleteSQL;
                _command.Parameters.AddWithValue("@Id", id);
                object result = _command.ExecuteScalar();

                _observer.Notify($"INFO. Brand with brand_id = {id} was DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public Brand GetById(int id)
        {
            Brand brand = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectByIdSQL;
                _command.Parameters.AddWithValue("@Id", id);
                object result = _command.ExecuteScalar();

                MySqlDataReader dataReader = _command.ExecuteReader();

                if (dataReader.Read())
                {
                    brand = new Brand.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithName(Convert.ToString(dataReader.GetValue(1)))
                        .Build();
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }

            return brand;
        }

        public List<Brand> GetAll()
        {
            List<Brand> brands = new List<Brand>();
            Brand brand = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    brand = new Brand.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithName(Convert.ToString(dataReader.GetValue(1)))
                        .Build();
                    brands.Add(brand);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }

            return brands;
        }
    }
}
