using System;

namespace BC.EQCS.Models
{
    public class IncidentActivityListingModel
    {
        public Int32 Id { get; set; }

        public Int32 IncidentId { get; set; }

        public DateTime DateTimeOfActivity { get; set; }

        public String Username { get; set; }

        public String LogType { get; set; }

        public String Payload { get; set; }

        public Int32 ApplicationUserId { get; set; }
    }
}
