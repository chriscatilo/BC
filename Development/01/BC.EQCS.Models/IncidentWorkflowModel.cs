using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    public abstract class IncidentWorkflowModel : IWorkflowModel<IncidentStatus>
    {
        protected IncidentWorkflowModel(IncidentStatus status, IncidentActivityLogType logType)
        {
            Status = status;

            LogType = logType;
        }

        public IncidentStatus Status { get; private set; }

        public IncidentActivityLogType LogType { get; private set; }
    }
}