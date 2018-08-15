﻿using System;
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
    public class HourlyCrimeTrendsController : Controller
    {
        #region Get data method.  
        /// <summary>  
        /// GET: /HourlyCrimeTrends/GetData  
        /// </summary>  
        /// <returns>Return data</returns>  

        public ActionResult GetData()
        {
            // Create resultMonthly object as a placeholder to hold results  
            JsonResult resultHourly = null;

            try
            {
                // Retrieve records
                List<HourlyCrimeTrendsModel> data6 = this.LoadData();

                // Convert data to JSON object
                resultHourly = new JsonResult(data6);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return data in JSON format 
            return resultHourly;
        }

        #endregion

        #region Load Data  

        /// <summary>  
        /// Load data method.  
        /// </summary>  
        /// <returns>Returns - Data</returns>  
        private List<HourlyCrimeTrendsModel> LoadData()
        {
            // Initialization.  
            List<HourlyCrimeTrendsModel> resultsHourly = new List<HourlyCrimeTrendsModel>();

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
                    sb.Append("select Crime_hour, count(case_number) as crime_count ");
                    sb.Append("from(select year(incident_datetime) as crime_year, hour_of_day((incident_datetime) as crime_hour), case_number as case_number ");
                    sb.Append("from dbo.Crime_Data ");
                    sb.Append("where year(incident_datetime) >= 2013 ");
                    sb.Append("and year(incident_datetime) <= 2017) as year_data ");
                    sb.Append("group by Crime_hour");
                    sb.Append("order by Crime_hour");
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
                                HourlyCrimeTrendsModel resultHourly = new HourlyCrimeTrendsModel();

                                // Set values in model record
                                resultHourly.Crime_hour = reader.GetInt32(1);
                                resultHourly.CrimeCount = reader.GetInt32(2);

                                // Add model record to collection
                                resultsHourly.Add(resultHourly);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return resultsHourly;
        }

        #endregion
    }
}
