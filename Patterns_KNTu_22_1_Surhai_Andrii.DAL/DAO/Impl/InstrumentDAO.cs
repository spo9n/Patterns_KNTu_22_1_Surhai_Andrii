using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class InstrumentDAO : IInstrumentDAO
    {
        private List<IObserver> _observers;
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO instruments(name, category_id, brand_id, country_id, year, price, quantity, description) " +
            "VALUES (@Name, @CategoryId, @BrandId, @CountryId, @Year, @Price, @Quantity, @Description);";
        private const string _updateSQL = "UPDATE instruments SET name = @Name, category_id = @CategoryId, brand_id = @BrandId, " +
            "country_id = @CountryId, year = @Year, price = @Price, quantity = @Quantity, description = @Description WHERE instrument_id = @Id;";
        private const string _deleteSQL = "DELETE FROM instruments WHERE instrument_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM instruments WHERE instrument_id = @id;";
        private const string _selectAllSQL = "SELECT * FROM instruments;";


        public InstrumentDAO()
        {
            this._observers = new List<IObserver>();
        }

        public void Create(Instrument instrument)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@Name", instrument.Name);
                _command.Parameters.AddWithValue("@CategoryId", instrument.CategoryId);
                _command.Parameters.AddWithValue("@BrandId", instrument.BrandId);
                _command.Parameters.AddWithValue("@CountryId", instrument.CountryId);
                _command.Parameters.AddWithValue("@Year", instrument.Year);
                _command.Parameters.AddWithValue("@Price", instrument.Price);
                _command.Parameters.AddWithValue("@Quantity", instrument.Quantity);
                _command.Parameters.AddWithValue("@Description", instrument.Description);
                object result = _command.ExecuteScalar();

                Notify($"INFO. Instrument with name = {instrument.Name}, category_id = {instrument.CategoryId}, " +
                    $"brand_id = {instrument.BrandId}, country_id = {instrument.CountryId}, year = {instrument.Year}, price = {instrument.Price}, " +
                    $"quantity = {instrument.Quantity}, description = {instrument.Description} was CREATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public void Update(Instrument instrument)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@Name", instrument.Name);
                _command.Parameters.AddWithValue("@CategoryId", instrument.CategoryId);
                _command.Parameters.AddWithValue("@BrandId", instrument.BrandId);
                _command.Parameters.AddWithValue("@CountryId", instrument.CountryId);
                _command.Parameters.AddWithValue("@Year", instrument.Year);
                _command.Parameters.AddWithValue("@Price", instrument.Price);
                _command.Parameters.AddWithValue("@Quantity", instrument.Quantity);
                _command.Parameters.AddWithValue("@Description", instrument.Description);
                _command.Parameters.AddWithValue("@Id", instrument.Id);
                object result = _command.ExecuteScalar();

                Notify($"INFO. Instrument with instrument_id = {instrument.Id} was UPDATED with name = {instrument.Name}, category_id = {instrument.CategoryId}, " +
                    $"brand_id = {instrument.BrandId}, country_id = {instrument.CountryId}, year = {instrument.Year}, price = {instrument.Price}, " +
                    $"quantity = {instrument.Quantity}, description = {instrument.Description}.");
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

                Notify($"INFO. Instrument with instrument_id = {id} was DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public Instrument GetById(int id)
        {
            Instrument instrument = null;
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
                    instrument = new Instrument.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithName(Convert.ToString(dataReader.GetValue(1)))
                        .WithCategoryId(Convert.ToInt32(dataReader.GetValue(2)))
                        .WithBrandId(Convert.ToInt32(dataReader.GetValue(3)))
                        .WithCountryId(Convert.ToInt32(dataReader.GetValue(4)))
                        .WithYear(Convert.ToInt32(dataReader.GetValue(5)))
                        .WithPrice(Convert.ToInt32(dataReader.GetValue(6)))
                        .WithQuantity(Convert.ToInt32(dataReader.GetValue(7)))
                        .WithDescription(Convert.ToString(dataReader.GetValue(8)))
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

            return instrument;
        }

        public List<Instrument> GetAll()
        {
            List<Instrument> instruments = new List<Instrument>();
            Instrument instrument = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    instrument = new Instrument.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithName(Convert.ToString(dataReader.GetValue(1)))
                        .WithCategoryId(Convert.ToInt32(dataReader.GetValue(2)))
                        .WithBrandId(Convert.ToInt32(dataReader.GetValue(3)))
                        .WithCountryId(Convert.ToInt32(dataReader.GetValue(4)))
                        .WithYear(Convert.ToInt32(dataReader.GetValue(5)))
                        .WithPrice(Convert.ToInt32(dataReader.GetValue(6)))
                        .WithQuantity(Convert.ToInt32(dataReader.GetValue(7)))
                        .WithDescription(Convert.ToString(dataReader.GetValue(8)))
                        .Build();
                    instruments.Add(instrument);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }

            return instruments;
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
