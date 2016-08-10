using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    public class IncidentReopeningModel : IncidentWorkflowModel
    {
        public string Reason { get; set; }

        public IncidentReopeningModel() : base(IncidentStatus.InProgress, IncidentActivityLogType.Reopening)
        {

        }
    }
}