using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class UserRoleDAO : IUserRoleDAO
    {
        private List<IObserver> _observers;
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO users_roles(name) VALUES (@Name);";
        private const string _updateSQL = "UPDATE users_roles SET name = @Name WHERE user_role_id = @Id;";
        private const string _deleteSQL = "DELETE FROM users_roles WHERE user_role_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM users_roles WHERE user_role_id = @Id;";
        private const string _selectAllSQL = "SELECT * FROM users_roles;";


        public UserRoleDAO()
        {
            this._observers = new List<IObserver>();
        }

        public void Create(UserRole userRole)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@Name", userRole.Name);
                object result = _command.ExecuteScalar();

                Notify($"INFO. User Role with name = {userRole.Name} was CREATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public void Update(UserRole userRole)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@Name", userRole.Name);
                _command.Parameters.AddWithValue("@Id", userRole.Id);
                object result = _command.ExecuteScalar();

                Notify($"INFO. User Role with user_role_id = {userRole.Id} was UPDATED with name = {userRole.Name}.");
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

                Notify($"INFO. User Role with user_role_id = {id} was DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public UserRole GetById(int id)
        {
            UserRole userRole = null;
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
                    userRole = new UserRole.Builder()
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
                Notify($"ERROR. {ex.ToString()}.");
            }

            return userRole;
        }

        public List<UserRole> GetAll()
        {
            List<UserRole> usersRoles = new List<UserRole>();
            UserRole userRole = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    userRole = new UserRole.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithName(Convert.ToString(dataReader.GetValue(1)))
                        .Build();
                    usersRoles.Add(userRole);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }

            return usersRoles;
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
