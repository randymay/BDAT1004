using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDAT1004.Models;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BDAT1004.Controllers
{
    public class Top5CrimesByHour : Controller
    {
        #region Get data method.  

        /// <summary>  
        /// GET: /Home/GetData  
        /// </summary>  
        /// <returns>Return data</returns>  
        public ActionResult GetData()
        {
            // Create result object as a placeholder to hold results  
            JsonResult result = new JsonResult(null);

            try
            {
                // Initialization.  
                Dictionary<Int32, Dictionary<String, Int32>> dbResults = new Dictionary<int, Dictionary<string, int>>();

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
                        sb.Append("select crime_data.crime_year, ");
                        sb.Append("crime_data.incident_type_primary, ");
                        sb.Append("avg(crime_data.hour_of_day) as avg_hour ");
                        sb.Append("from(select year(incident_datetime) as crime_year, ");
                        sb.Append("incident_type_primary, ");
                        sb.Append("hour_of_day ");
                        sb.Append("from dbo.Crime_Data ");
                        sb.Append("where incident_type_primary in ");
                        sb.Append("(select top 5 incident_type_primary from dbo.Crime_Data group by incident_type_primary)) as crime_data ");
                        sb.Append("group by crime_year, incident_type_primary ");
                        sb.Append("order by crime_year, incident_type_primary desc ");
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
                                    int year = reader.GetInt32(0);
                                    String type = reader.GetString(1);
                                    int value = reader.GetInt32(2);

                                    Dictionary<String, Int32> rowResult;
                                    if (!dbResults.ContainsKey (year)) {
                                        rowResult = new Dictionary<string, int>();
                                    } else {
                                        rowResult = dbResults[year];
                                    }
                                    rowResult[type] = value;

                                    dbResults[year] = rowResult;
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

                Top5CrimeHours results = new Top5CrimeHours();

                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                foreach (KeyValuePair<Int32, Dictionary<String, Int32>> rows in dbResults) {
                    Top5CrimeHoursValues values = new Top5CrimeHoursValues();
                    if (!results.columnHeaders.Contains("Year")) {
                        results.columnHeaders.Add("Year");    
                    }

                    Int32 crimeYear = rows.Key;
                    values.Year = crimeYear;

                    foreach (KeyValuePair<String, Int32> rowValue in rows.Value)
                    {
                        String crimeName = textInfo.ToTitleCase(rowValue.Key.ToLower());
                        Int32 crimeHour = rowValue.Value;

                        if (!results.columnHeaders.Contains(crimeName))
                        {
                            results.columnHeaders.Add(crimeName);
                        }

                        int crimeNameIndex = results.columnHeaders.IndexOf(crimeName);

                        if (crimeNameIndex == 1) 
                        {
                            values.CrimeHour1 = crimeHour;
                        } else if (crimeNameIndex == 2)
                        {
                            values.CrimeHour2 = crimeHour;
                        }
                        else if (crimeNameIndex == 3)
                        {
                            values.CrimeHour3 = crimeHour;
                        }
                        else if (crimeNameIndex == 4)
                        {
                            values.CrimeHour4 = crimeHour;
                        }
                        else if (crimeNameIndex == 5)
                        {
                            values.CrimeHour5 = crimeHour;
                        }
                    }
                    results.columnValues.Add(values);
                }

                // Convert data to JSON object
                result = new JsonResult(results);
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
    }
}
