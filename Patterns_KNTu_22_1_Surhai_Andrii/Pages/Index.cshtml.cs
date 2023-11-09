using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Database;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages
{
    public class IndexModel : PageModel
    {
        //public string ConnectionStatusMessage { get; private set; }

        //public Instrument instrument = new Instrument.Builder().WithName("123").WithCategoryId(1).Build();

        //public void OnGet()
        //{
        //    // This is an empty method for the initial page load.
        //}

        //public void OnPost()
        //{
        //    // Attempt to test the database connection
        //    if (TestDatabaseConnection())
        //    {
        //        ConnectionStatusMessage = "Database connection is successful.";
        //    }
        //    else
        //    {
        //        ConnectionStatusMessage = "Database connection failed.";
        //    }
        //}

        //private bool TestDatabaseConnection()
        //{
        //    using (MySqlConnection connection = DatabaseConnection.Connection)
        //    {
        //        try
        //        {
        //            return true;
        //        }
        //        catch (MySqlException ex)
        //        {
        //            return false;
        //        }
        //    }
        //}

        [BindProperty]
        public string TestResult { get; set; }

        public void OnGet()
        {
            // Initialize the page
        }

        public void OnPost()
        {
            if (Request.Form.ContainsKey("testConnectionButton"))
            {
                var connection = DatabaseConnection.Instance.GetConnection();
                try
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Optionally, you can perform a test query here, like "SELECT 1", to verify the connection.
                    connection.Close();
                    TestResult = "Database connection test succeeded.";
                }
                catch (MySqlException ex)
                {
                    TestResult = $"Database connection test failed: {ex.Message}";
                }
            }
        }

    }
}