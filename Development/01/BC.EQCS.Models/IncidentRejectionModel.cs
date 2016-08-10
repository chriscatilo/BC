using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    public class IncidentRejectionModel : IncidentWorkflowModel
    {
        public string Reason { get; set; }

        public IncidentRejectionModel() : base(IncidentStatus.Rejected, IncidentActivityLogType.Rejection)
        {
        }
    }
}