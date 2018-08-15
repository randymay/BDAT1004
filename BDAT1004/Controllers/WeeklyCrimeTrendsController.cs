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
    public class WeeklyCrimeTrendsController : Controller
    {
        #region Get data method.  
        /// <summary>  
        /// GET: /WeeklyCrimeTrends/GetData  
        /// </summary>  
        /// <returns>Return data</returns>  

        public ActionResult GetData()
        {
            // Create resultWeekly object as a placeholder to hold results  
            JsonResult resultWeekly = null;

            try
            {
                // Retrieve records
                List<WeeklyCrimeTrendsModel> data4 = this.LoadData();

                // Convert data to JSON object
                resultWeekly = new JsonResult(data4);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return data in JSON format 
            return resultWeekly;
        }

        #endregion

        #region Load Data  

        /// <summary>  
        /// Load data method.  
        /// </summary>  
        /// <returns>Returns - Data</returns>  
        private List<WeeklyCrimeTrendsModel> LoadData()
        {
            // Initialization.  
            List<WeeklyCrimeTrendsModel> resultsWeekly = new List<WeeklyCrimeTrendsModel>();

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
                    sb.Append("select crime_day, crime_year, count(case_number) as crime_count ");
                    sb.Append("from(select datename(dw, incident_datetime) as crime_day,year(incident_datetime) as crime_year,datepart(dw,incident_datetime) as day, case_number as case_number ");
                    sb.Append("from dbo.Crime_Data ");
                    sb.Append("where year(incident_datetime) >= 2013 ");
                    sb.Append("and year(incident_datetime) <= 2017) as year_data ");
                    sb.Append("group by crime_day,crime_year ");
                    sb.Append("order by day");
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
                                WeeklyCrimeTrendsModel resultWeekly = new WeeklyCrimeTrendsModel();

                                // Set values in model record
                                resultWeekly.Day = reader.GetString(0);
                                resultWeekly.CrimeCount = reader.GetInt32(1);

                                // Add model record to collection
                                resultsWeekly.Add(resultWeekly);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return resultsWeekly;
        }

        #endregion
    }
}
