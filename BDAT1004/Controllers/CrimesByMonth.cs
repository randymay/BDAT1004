using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDAT1004.Models;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc; 

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BDAT1004.Controllers
{
    public class CrimesByMonthController : Controller
    {
        #region Get data method.  

        /// <summary>  
        /// GET: /Home/GetData  
        /// </summary>  
        /// <returns>Return data</returns>  
        public ActionResult GetData()
        {
            // Create result object as a placeholder to hold results  
            JsonResult result = null;

            try
            {
                // Retrieve records
                List<CrimesByMonthModel> data = this.LoadData();

                // Convert data to JSON object
                result = new JsonResult(data);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return data in JSON format 
            return result;
        }

        #endregion

        #region Load Data  

        /// <summary>  
        /// Load data method.  
        /// </summary>  
        /// <returns>Returns - Data</returns>  
        private List<CrimesByMonthModel> LoadData()
        {
            // Initialization.  
            List<CrimesByMonthModel> results = new List<CrimesByMonthModel>();

            try
            {
                // Connect to DB
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "bdat.database.windows.net";
                builder.UserID = "adminbdat";
                builder.Password = "Georgian123";
                builder.InitialCatalog = "DataProgramming";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    // Prepare SQL
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select crime_year, crime_month, count(case_number) as crime_count ");
                    sb.Append("from(select year(incident_datetime) as crime_year, month(incident_datetime) as crime_month, case_number as case_number ");
                    sb.Append("from dbo.Crime_Data ");
                    sb.Append("where year(incident_datetime) >= 2016 ");
                    sb.Append("and year(incident_datetime) <= 2017) as year_data ");
                    sb.Append("group by crime_year, crime_month ");
                    sb.Append("order by crime_year, crime_month");
                    String sql = sb.ToString();

                    // Open a Connection to the DB
                    connection.Open();

                    // Execute SQL
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read Data
                            while (reader.Read())
                            {
                                // Initialize Model record  
                                CrimesByMonthModel result = new CrimesByMonthModel();

                                // Set values in model record
                                result.Month = reader.GetInt32(0);
                                result.Year = reader.GetInt32(1);
                                result.CrimeCount = reader.GetInt32(2);
                                

                                // Add model record to collection
                                results.Add(result);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return results;
        }

        #endregion
    }
}
