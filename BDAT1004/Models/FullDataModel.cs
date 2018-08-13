using System;
namespace BDAT1004.Models
{
    public class FullDataModel
    {
        public int IncidentId { get; set; } 
        public String CaseNumber { get; set; } 
        public int DayOfWeek { get; set; } 
        public int HourOfDay { get; set; } 
        public DateTime IncidentDateTime { get; set; } 
        public String ParentIncidentType { get; set; } 
        public String IncidentType { get; set; } 
        public float Latitude { get; set; } 
        public float Longitude { get; set; } 
        public String Description { get; set; }
    }
}
