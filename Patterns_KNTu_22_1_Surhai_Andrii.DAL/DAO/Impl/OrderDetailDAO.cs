using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class OrderDetailDAO : IOrderDetailDAO
    {
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO orders_details(order_id, instrument_id, price, quantity) " +
            "VALUES (@OrderId, @InstrumentId, @Price, @Quantity);";
        private const string _updateSQL = "UPDATE orders_details SET price = @Price, quantity = @Quantity" +
            "WHERE order_id = @OrderId AND instrument_id = @InstrumentId;";
        private const string _deleteSQL = "DELETE FROM orders_details WHERE order_id = @OrderId, instrument_id = @InstrumentId;";
        private const string _selectByIdSQL = "SELECT * FROM orders_details WHERE order_id = @OrderId AND instrument_id = @InstrumentId;";
        private const string _selectAllSQL = "SELECT * FROM orders_details;";


        public void Create(OrderDetail orderDetail)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@OrderId", orderDetail.OrderId);
                _command.Parameters.AddWithValue("@InstrumentId", orderDetail.InstrumentId);
                _command.Parameters.AddWithValue("@Price", orderDetail.Price);
                _command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                object result = _command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@Price", orderDetail.Price);
                _command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                _command.Parameters.AddWithValue("@OrderId", orderDetail.OrderId);
                _command.Parameters.AddWithValue("@InstrumentId", orderDetail.InstrumentId);
                object result = _command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Delete(int orderId, int instrumentId)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _deleteSQL;
                _command.Parameters.AddWithValue("@OrderId", orderId);
                _command.Parameters.AddWithValue("@InstrumentId", instrumentId);
                object result = _command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public OrderDetail GetById(int orderId, int instrumentId)
        {
            OrderDetail orderDetail = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectByIdSQL;
                _command.Parameters.AddWithValue("@OrderId", orderId);
                _command.Parameters.AddWithValue("@InstrumentId", instrumentId);
                object result = _command.ExecuteScalar();

                MySqlDataReader dataReader = _command.ExecuteReader();

                if (dataReader.Read())
                {
                    orderDetail = new OrderDetail.Builder()
                        .WithOrderId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithInstrumentId(Convert.ToInt32(dataReader.GetValue(1)))
                        .WithPrice(Convert.ToDouble(dataReader.GetValue(2)))
                        .WithQuantity(Convert.ToInt32(dataReader.GetValue(3)))
                        .Build();
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return orderDetail;
        }

        public List<OrderDetail> GetAll()
        {
            List<OrderDetail> ordersDetails = new List<OrderDetail>();
            OrderDetail orderDetail = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    orderDetail = new OrderDetail.Builder()
                        .WithOrderId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithInstrumentId(Convert.ToInt32(dataReader.GetValue(1)))
                        .WithPrice(Convert.ToDouble(dataReader.GetValue(2)))
                        .WithQuantity(Convert.ToInt32(dataReader.GetValue(3)))
                        .Build();
                    ordersDetails.Add(orderDetail);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return ordersDetails;
        }
    }
}
