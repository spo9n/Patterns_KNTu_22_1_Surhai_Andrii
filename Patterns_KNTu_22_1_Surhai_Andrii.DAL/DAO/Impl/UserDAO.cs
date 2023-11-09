using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class UserDAO : IUserDAO
    {
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO users(surname, name, patronymic, email, phone) " +
            "VALUES (@Surname, @Name, @Patronymic, @Email, @Phone);";
        private const string _updateSQL = "UPDATE users SET surname = @Surname, name = @Name, patronymic = @Patronymic, email = @Email, phone = @Phone " +
            "WHERE user_id = @Id;";
        private const string _deleteSQL = "DELETE FROM users WHERE user_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM users WHERE user_id = @id;";
        private const string _selectAllSQL = "SELECT * FROM users;";


        public void Create(User user)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@Surname", user.Surname);
                _command.Parameters.AddWithValue("@Name", user.Name);
                _command.Parameters.AddWithValue("@Patronymic", user.Patronymic);
                _command.Parameters.AddWithValue("@Email", user.Email);
                _command.Parameters.AddWithValue("@Phone", user.Phone);
                object result = _command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Update(User user)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@Surname", user.Surname);
                _command.Parameters.AddWithValue("@Name", user.Name);
                _command.Parameters.AddWithValue("@Patronymic", user.Patronymic);
                _command.Parameters.AddWithValue("@Email", user.Email);
                _command.Parameters.AddWithValue("@Phone", user.Phone);
                _command.Parameters.AddWithValue("@Id", user.Id);
                object result = _command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public User GetById(int id)
        {
            User user = null;
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
                    user = new User.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithSurname(Convert.ToString(dataReader.GetValue(1)))
                        .WithName(Convert.ToString(dataReader.GetValue(2)))
                        .WithPatronymic(Convert.ToString(dataReader.GetValue(3)))
                        .WithEmail(Convert.ToString(dataReader.GetValue(4)))
                        .WithPhone(Convert.ToString(dataReader.GetValue(5)))
                        .Build();
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return user;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            User user = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    user = new User.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithSurname(Convert.ToString(dataReader.GetValue(1)))
                        .WithName(Convert.ToString(dataReader.GetValue(2)))
                        .WithPatronymic(Convert.ToString(dataReader.GetValue(3)))
                        .WithEmail(Convert.ToString(dataReader.GetValue(4)))
                        .WithPhone(Convert.ToString(dataReader.GetValue(5)))
                        .Build();
                    users.Add(user);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return users;
        }
    }
}
