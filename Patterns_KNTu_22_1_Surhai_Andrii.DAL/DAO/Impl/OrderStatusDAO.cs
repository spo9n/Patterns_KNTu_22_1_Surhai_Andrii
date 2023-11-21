using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class OrderStatusDAO : IOrderStatusDAO
    {
        private DAOObserver _observer = new DAOObserver();
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO orders_statuses(name) VALUES (@Name);";
        private const string _updateSQL = "UPDATE orders_statuses SET name = @Name WHERE status_id = @Id;";
        private const string _deleteSQL = "DELETE FROM orders_statuses WHERE status_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM orders_statuses WHERE status_id = @id;";
        private const string _selectAllSQL = "SELECT * FROM orders_statuses;";


        public OrderStatusDAO()
        {
            _observer.Attach(new Logger());
        }


        public void Create(OrderStatus orderStatus)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@Name", orderStatus.Name);
                object result = _command.ExecuteScalar();

                _observer.Notify($"INFO. OrderStatus with name = {orderStatus.Name} was CREATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public void Update(OrderStatus orderStatus)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@Name", orderStatus.Name);
                _command.Parameters.AddWithValue("@Id", orderStatus.Id);
                object result = _command.ExecuteScalar();

                _observer.Notify($"INFO. OrderStatus with status_id = {orderStatus.Id} was UPDATED with name = {orderStatus.Name}.");
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

                _observer.Notify($"INFO. OrderStatus with status_id = {id} was DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public OrderStatus GetById(int id)
        {
            OrderStatus orderStatus = null;
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
                    orderStatus = new OrderStatus.Builder()
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

            return orderStatus;
        }

        public List<OrderStatus> GetAll()
        {
            List<OrderStatus> ordersStatuses = new List<OrderStatus>();
            OrderStatus orderStatus = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    orderStatus = new OrderStatus.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithName(Convert.ToString(dataReader.GetValue(1)))
                        .Build();
                    ordersStatuses.Add(orderStatus);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _observer.Notify($"ERROR. {ex.ToString()}.");
            }

            return ordersStatuses;
        }
    }
}
