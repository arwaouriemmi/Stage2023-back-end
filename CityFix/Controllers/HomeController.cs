using CityFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CityFix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            //try
            //{
                // Retrieve the connection string from appsettings.json
               // string connectionString = "Server=localhost;User ID=root;Password=root;Database=CityFix";

                // Create and use the MySqlConnection
               // using var connection = new MySqlConnection(connectionString);

                // Open the connection
               // connection.Open();

                // Check if the connection state is open
               // if (connection.State == System.Data.ConnectionState.Open)
              //  {
                    // Connection established successfully
                    // Perform any necessary database operations here

                    // Example: Retrieve data from a table
               //     string query = "SELECT *  FROM test";
                   // using var command = new MySqlCommand(query, connection);
                 //   using var reader = command.ExecuteReader();

                    // Process the data
                   // var data = new List<string>();
                    //while (reader.Read())
                    //{
                     //   string value = reader.GetString(0); // Assuming the data is in the first column (index 0)
                       // data.Add(value);
                   // }

                    // Pass the data to the view
                   // _logger.LogInformation("cc");
                    //_logger.LogInformation(data[0]);
                //}
              //  else
               /* {
                    // Connection failed
                    // Handle the failed connection scenario
                    _logger.LogError("Failed to establish database connection.");
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Connection error
                // Handle the exception or log the error
                _logger.LogError(ex, "Failed to establish database connection.");
                return View("Error");
            }
 */
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



