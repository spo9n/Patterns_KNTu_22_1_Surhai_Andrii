using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class UserDAO : IUserDAO
    {
        private List<IObserver> _observers;
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO users(user_role_id, surname, name, patronymic, email, phone, username, password) " +
            "VALUES (@UserRoleId, @Surname, @Name, @Patronymic, @Email, @Phone, @Username, @Password);";
        private const string _updateSQL = "UPDATE users SET user_role_id = @UserRoleId, surname = @Surname, name = @Name, " +
            "patronymic = @Patronymic, email = @Email, phone = @Phone, username = @Username, password = @Password WHERE user_id = @Id;";
        private const string _deleteSQL = "DELETE FROM users WHERE user_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM users WHERE user_id = @id;";
        private const string _selectByUsernameSQL = "SELECT * FROM users WHERE username = @Username;";
        private const string _selectAllSQL = "SELECT * FROM users;";


        public UserDAO()
        {
            this._observers = new List<IObserver>();
        }


        public void Create(User user)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@UserRoleId", user.UserRoleId);
                _command.Parameters.AddWithValue("@Surname", user.Surname);
                _command.Parameters.AddWithValue("@Name", user.Name);
                _command.Parameters.AddWithValue("@Patronymic", user.Patronymic);
                _command.Parameters.AddWithValue("@Email", user.Email);
                _command.Parameters.AddWithValue("@Phone", user.Phone);
                _command.Parameters.AddWithValue("@Username", user.Username);
                _command.Parameters.AddWithValue("@Password", user.Password);
                object result = _command.ExecuteScalar();

                Notify($"INFO. User with user_role_id = {user.UserRoleId}, surname = {user.Surname}, name = {user.Name}, patronymic = {user.Patronymic}, " +
                    $"email = {user.Email}, phone = {user.Phone}, username = {user.Username}, password = {user.Password} was CREATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public void Update(User user)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@UserRoleId", user.UserRoleId);
                _command.Parameters.AddWithValue("@Surname", user.Surname);
                _command.Parameters.AddWithValue("@Name", user.Name);
                _command.Parameters.AddWithValue("@Patronymic", user.Patronymic);
                _command.Parameters.AddWithValue("@Email", user.Email);
                _command.Parameters.AddWithValue("@Phone", user.Phone);
                _command.Parameters.AddWithValue("@Id", user.Id);
                object result = _command.ExecuteScalar();

                Notify($"INFO. User with user_id = {user.Id} was UPDATED with user_role_id = {user.UserRoleId}, surname = {user.Surname}, name = {user.Name}, " +
                    $"patronymic = {user.Patronymic}, email = {user.Email}, phone = {user.Phone}, username = {user.Username}, password = {user.Password}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
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

                Notify($"INFO. User with user_id = {id} was DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
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
                        .WithUserRoleId(Convert.ToInt32(dataReader.GetValue(1)))
                        .WithSurname(Convert.ToString(dataReader.GetValue(2)))
                        .WithName(Convert.ToString(dataReader.GetValue(3)))
                        .WithPatronymic(Convert.ToString(dataReader.GetValue(4)))
                        .WithEmail(Convert.ToString(dataReader.GetValue(5)))
                        .WithPhone(Convert.ToString(dataReader.GetValue(6)))
                        .WithUsername(Convert.ToString(dataReader.GetValue(7)))
                        .WithPassword(Convert.ToString(dataReader.GetValue(8)))
                        .Build();
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }

            return user;
        }

        public User GetByUsername(string username)
        {
            User user = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectByUsernameSQL;
                _command.Parameters.AddWithValue("@Username", username);
                object result = _command.ExecuteScalar();

                MySqlDataReader dataReader = _command.ExecuteReader();

                if (dataReader.Read())
                {
                    user = new User.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithUserRoleId(Convert.ToInt32(dataReader.GetValue(1)))
                        .WithSurname(Convert.ToString(dataReader.GetValue(2)))
                        .WithName(Convert.ToString(dataReader.GetValue(3)))
                        .WithPatronymic(Convert.ToString(dataReader.GetValue(4)))
                        .WithEmail(Convert.ToString(dataReader.GetValue(5)))
                        .WithPhone(Convert.ToString(dataReader.GetValue(6)))
                        .WithUsername(Convert.ToString(dataReader.GetValue(7)))
                        .WithPassword(Convert.ToString(dataReader.GetValue(8)))
                        .Build();
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
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
                        .WithUserRoleId(Convert.ToInt32(dataReader.GetValue(1)))
                        .WithSurname(Convert.ToString(dataReader.GetValue(2)))
                        .WithName(Convert.ToString(dataReader.GetValue(3)))
                        .WithPatronymic(Convert.ToString(dataReader.GetValue(4)))
                        .WithEmail(Convert.ToString(dataReader.GetValue(5)))
                        .WithPhone(Convert.ToString(dataReader.GetValue(6)))
                        .WithUsername(Convert.ToString(dataReader.GetValue(7)))
                        .WithPassword(Convert.ToString(dataReader.GetValue(8)))
                        .Build();
                    users.Add(user);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }

            return users;
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

    }
}
