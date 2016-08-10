using BC.EQCS.Models.Enums;

namespace BC.EQCS.Entities.Models
{
    public class IncidentActivityLog
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public System.DateTime DateTimeOfActivity { get; set; }
        public int ApplicationUserId { get; set; }
        public IncidentActivityLogType LogType { get; set; }
        public string Payload { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Incident Incident { get; set; }
    }
}
