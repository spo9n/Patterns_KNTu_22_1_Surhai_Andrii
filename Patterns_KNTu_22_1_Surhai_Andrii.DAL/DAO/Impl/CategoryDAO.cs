﻿using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl
{
    public class CategoryDAO : ICategoryDAO
    {
        private List<IObserver> _observers;
        private DatabaseConnection _connection = DatabaseConnection.Instance;
        private MySqlCommand _command;

        private const string _insertSQL = "INSERT INTO categories(name) VALUES (@Name);";
        private const string _updateSQL = "UPDATE categories SET name = @Name WHERE category_id = @Id;";
        private const string _deleteSQL = "DELETE FROM categories WHERE category_id = @Id;";
        private const string _selectByIdSQL = "SELECT * FROM categories WHERE category_id = @Id;";
        private const string _selectAllSQL = "SELECT * FROM categories;";


        public CategoryDAO()
        {
            this._observers = new List<IObserver>();
        }

        public void Create(Category category)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _insertSQL;
                _command.Parameters.AddWithValue("@Name", category.Name);
                object result = _command.ExecuteScalar();

                Notify($"INFO. Category with name = {category.Name} was CREATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public void Update(Category category)
        {
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _updateSQL;
                _command.Parameters.AddWithValue("@Name", category.Name);
                _command.Parameters.AddWithValue("@Id", category.Id);
                object result = _command.ExecuteScalar();

                Notify($"INFO. Category with category_id = {category.Id} was UPDATED with name = {category.Name}.");
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

                Notify($"INFO. Category with category_id = {id} was DELETED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }
        }

        public Category GetById(int id)
        {
            Category category = null;
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
                    category = new Category.Builder()
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

            return category;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            Category category = null;
            try
            {
                _connection.GetConnection();
                _command = _connection.GetConnection().CreateCommand();
                _command.CommandText = _selectAllSQL;
                object result = _command.ExecuteNonQuery();

                MySqlDataReader dataReader = _command.ExecuteReader();

                while (dataReader.Read())
                {
                    category = new Category.Builder()
                        .WithId(Convert.ToInt32(dataReader.GetValue(0)))
                        .WithName(Convert.ToString(dataReader.GetValue(1)))
                        .Build();
                    categories.Add(category);
                }

                dataReader.Close();
                _connection.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Notify($"ERROR. {ex.ToString()}.");
            }

            return categories;
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
