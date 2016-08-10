using System;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Entities.Models
{
    public class IncidentActivityListingView
    {
        public Int32 Id { get; set; }
        public Int32 IncidentId { get; set; }
        public DateTime DateTimeOfActivity { get; set; }
        public Int32 ApplicationUserId { get; set; }
        public IncidentActivityLogType LogType { get; set; }
        public String Payload { get; set; }
        public String Username { get; set; }
    }
}