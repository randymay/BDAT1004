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
    public class CrimeDataController : Controller
    {
        #region Get data method.  

        /// <summary>  
        /// GET: /Rest/AllData  
        /// </summary>  
        /// <returns>Return data</returns> 
        [HttpGet] 
        [Produces("application/json")]
        public ActionResult All()
        {
            // Create result object as a placeholder to hold results  
            JsonResult result = new JsonResult(null);

            try
            {
                // Retrieve records
                // Initialization.  
                List<FullDataModel> data = new List<FullDataModel>();

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
                        sb.Append("select ");
                        sb.Append("incident_id, ");
                        sb.Append("case_number, ");
                        sb.Append("day_of_week, ");
                        sb.Append("hour_of_day, ");
                        sb.Append("incident_datetime, ");
                        sb.Append("parent_incident_type, ");
                        sb.Append("incident_type_primary, ");
                        sb.Append("latitude, ");
                        sb.Append("longitude, ");
                        sb.Append("incident_description ");
                        sb.Append("from dbo.Crime_Data ");
                        sb.Append("order by incident_datetime asc");
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
                                    // Add model record to collection
                                    data.Add(convertRecordToModel(reader));
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

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

        #region Get data by range method.  

        /// <summary>  
        /// GET: /Rest/AllData  
        /// </summary>  
        /// <returns>Return data</returns>  
        [HttpGet]
        [Produces("application/json")]
        public ActionResult DateRange(String startDate, String endDate)
        {
            // Create result object as a placeholder to hold results  
            JsonResult result = new JsonResult(null);

            try
            {
                // Retrieve records
                // Initialization.  
                List<FullDataModel> data = new List<FullDataModel>();

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
                        sb.Append("select ");
                        sb.Append("incident_id, ");
                        sb.Append("case_number, ");
                        sb.Append("day_of_week, ");
                        sb.Append("hour_of_day, ");
                        sb.Append("incident_datetime, ");
                        sb.Append("parent_incident_type, ");
                        sb.Append("incident_type_primary, ");
                        sb.Append("latitude, ");
                        sb.Append("longitude, ");
                        sb.Append("incident_description ");
                        sb.Append("from dbo.Crime_Data ");
                        sb.Append("where incident_datetime >= '");
                        sb.Append(startDate);
                        sb.Append("' and incident_datetime <= '");
                        sb.Append(endDate);
                        sb.Append("' order by incident_datetime asc");
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
                                    // Add model record to collection
                                    data.Add(convertRecordToModel(reader));
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

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

        #region Get data by ID method.  

        /// <summary>  
        /// GET: /Rest/AllData  
        /// </summary>  
        /// <returns>Return data</returns>  
        [HttpGet]
        [Produces("application/json")]
        public ActionResult Id(int IncidentId)
        {
            // Create result object as a placeholder to hold results  
            JsonResult result = new JsonResult(null);

            try
            {
                // Retrieve records
                // Initialization.  
                List<FullDataModel> data = new List<FullDataModel>();

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
                        sb.Append("select ");
                        sb.Append("incident_id, ");
                        sb.Append("case_number, ");
                        sb.Append("day_of_week, ");
                        sb.Append("hour_of_day, ");
                        sb.Append("incident_datetime, ");
                        sb.Append("parent_incident_type, ");
                        sb.Append("incident_type_primary, ");
                        sb.Append("latitude, ");
                        sb.Append("longitude, ");
                        sb.Append("incident_description ");
                        sb.Append("from dbo.Crime_Data ");
                        sb.Append("where incident_id = ");
                        sb.Append(IncidentId);
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
                                    // Add model record to collection
                                    data.Add(convertRecordToModel(reader));
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

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

        #region Convert Record to Model Region
        private FullDataModel convertRecordToModel(SqlDataReader reader)
        {
            FullDataModel dataRecord = new FullDataModel();

            // Set values in model record
            dataRecord.IncidentId = reader.GetInt64(0);
            dataRecord.CaseNumber = reader.GetString(1);
            dataRecord.DayOfWeek = reader.GetString(2);
            dataRecord.HourOfDay = reader.GetInt32(3);
            dataRecord.IncidentDateTime = reader.GetString(4);
            dataRecord.ParentIncidentType = reader.GetString(5);
            dataRecord.IncidentType = reader.GetString(6);
            dataRecord.Latitude = reader.GetDouble(7);
            dataRecord.Longitude = reader.GetDouble(8);
            dataRecord.Description = reader.GetString(9);

            return dataRecord;
        }
        # endregion
    }
}
