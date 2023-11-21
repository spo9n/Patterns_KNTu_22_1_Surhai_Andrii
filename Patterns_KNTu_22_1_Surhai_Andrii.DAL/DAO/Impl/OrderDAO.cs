using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class OrderDAO : IOrderDAO
    {
        private DAOObserver _observer = new DAOObserver();
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO orders(user_id, status_id, comment) " +
            "VALUES (@UserId, @StatusId, @Comment);" +
            "SELECT LAST_INSERT_ID();";
        private const string _updateSQL = "UPDATE orders SET user_id = @UserId, status_id = @StatusId, comment = @Comment " +
            "WHERE order_id = @Id;";
        private const string _deleteSQL = "DELETE FROM orders WHERE order_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM orders WHERE order_id = @id;";
        private const string _selectAllSQL = "SELECT * FROM orders;";


        public OrderDAO()
        {
            _observer.Attach(new Logger());
        }


        public int Create(Order order)
        {
            int lastInsertedId = -1;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@UserId", order.UserId);
                _command.Parameters.AddWithValue("@StatusId", order.StatusId);
                _command.Parameters.AddWithValue("@Comment", order.Comment);

                lastInsertedId = Convert.ToInt32(_command.ExecuteScalar());

                _observer.Notify($"INFO. Order with user_id = {order.UserId}, status_id = {order.StatusId}, " +
                    $"comment = {order.Comment} was CREATED.");

                return lastInsertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
                return lastInsertedId;
            }
        }

        public void Update(Order order)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@UserId", order.UserId);
                _command.Parameters.AddWithValue("@StatusId", order.StatusId);
                _command.Parameters.AddWithValue("@Comment", order.Comment);
                _command.Parameters.AddWithValue("@Id", order.Id);
                object result = _command.ExecuteScalar();

                _observer.Notify($"INFO. Order with order_id = {order.Id} was UPDATED with user_id = {order.UserId}, " +
                    $"status_id = {order.StatusId}, comment = {order.Comment}.");
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
                _observer.Notify($"INFO. Order with order_id = {id} was DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public Order GetById(int id)
        {
            Order order = null;
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
                    order = new Order.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithUserId(Convert.ToInt32(dataReader.GetValue(1)))
                        .WithStatusId(Convert.ToInt32(dataReader.GetValue(2)))
                        .WithComment(Convert.ToString(dataReader.GetValue(3)))
                        .WithCreatedAt(Convert.ToDateTime(dataReader.GetValue(4)))
                        .WithUpdatedAt(Convert.ToDateTime(dataReader.GetValue(5)))
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

            return order;
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            Order order = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    order = new Order.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithUserId(Convert.ToInt32(dataReader.GetValue(1)))
                        .WithStatusId(Convert.ToInt32(dataReader.GetValue(2)))
                        .WithComment(Convert.ToString(dataReader.GetValue(3)))
                        .WithCreatedAt(Convert.ToDateTime(dataReader.GetValue(4)))
                        .WithUpdatedAt(Convert.ToDateTime(dataReader.GetValue(5)))
                        .Build();
                    orders.Add(order);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }

            return orders;
        }
    }
}
