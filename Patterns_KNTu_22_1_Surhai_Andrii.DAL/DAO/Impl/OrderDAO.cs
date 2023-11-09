using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class OrderDAO : IOrderDAO
    {
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO orders(user_id, status_id, comment) " +
            "VALUES (@UserId, @StatusId, @Comment);";
        private const string _updateSQL = "UPDATE orders SET userId = @UserId, status_id = @StatusId, comment = @Comment " +
            "WHERE order_id = @Id;";
        private const string _deleteSQL = "DELETE FROM orders WHERE order_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM orders WHERE order_id = @id;";
        private const string _selectAllSQL = "SELECT * FROM orders;";


        public void Create(Order order)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@UserId", order.UserId);
                _command.Parameters.AddWithValue("@StatusId", order.StatusId);
                _command.Parameters.AddWithValue("@Comment", order.Comment);
                object result = _command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
                        .Build();
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
                        .Build();
                    orders.Add(order);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return orders;
        }
    }
}
