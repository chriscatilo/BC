using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Workflow.Incident
{
    public class IncidentAcceptanceActivityLogger : IncidentWorkflowActivityLogger,
        IWorkflowActivityLogger<IncidentStatus, IncidentGenericWorkflowModel>
    {
        public IncidentAcceptanceActivityLogger(
            IRepository<IncidentActivityLogModel> activityLogRepository,
            IModelValidator<IncidentActivityLogModel> activityLogValidator,
            IContextResolver contextResolver)
            : base(activityLogRepository, activityLogValidator, contextResolver)
        {
        }

        public override IncidentStatus ForIncidentStatus
        {
            get { return IncidentStatus.InProgress; }
        }

        public void Log(int modelId, IncidentGenericWorkflowModel workflowModel)
        {
            var logEntry = Mapper.Map<IncidentActivityLogModel>(workflowModel);

            Execute(modelId, logEntry);
        }
    }
}