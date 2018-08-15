using System;
namespace BDAT1004.Models
{
    public class FullDataModel
    {
        public long IncidentId { get; set; } 
        public String CaseNumber { get; set; } 
        public String DayOfWeek { get; set; } 
        public long HourOfDay { get; set; } 
        public String IncidentDateTime { get; set; } 
        public String ParentIncidentType { get; set; } 
        public String IncidentType { get; set; } 
        public double Latitude { get; set; } 
        public double Longitude { get; set; } 
        public String Description { get; set; }
    }
}
