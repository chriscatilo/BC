using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    public class IncidentGenericWorkflowModel : IncidentWorkflowModel
    {
        public IncidentGenericWorkflowModel(IncidentStatus status, IncidentActivityLogType logType) : base(status, logType)
        {
        }
    }
}