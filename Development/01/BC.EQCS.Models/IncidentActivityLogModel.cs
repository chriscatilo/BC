using System;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    public class IncidentActivityLogModel
    {
        public int Id { get; private set; }

        public int IncidentId { get; set; }

        public DateTime DateTimeOfActivity { get; set; }

        public UserModel User { get; set; }

        public IncidentActivityLogType LogType { get; set; }

        public string Payload { get; set; }
    }
}