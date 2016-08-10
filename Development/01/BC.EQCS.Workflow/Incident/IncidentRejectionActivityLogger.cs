using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Workflow.Incident
{
    public class IncidentRejectionActivityLogger : IncidentWorkflowActivityLogger,
        IWorkflowActivityLogger<IncidentStatus, IncidentRejectionModel>
    {
        public IncidentRejectionActivityLogger(
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

        public void Log(int modelId, IncidentRejectionModel workflowModel)
        {
            var logEntry = Mapper.Map<IncidentActivityLogModel>(workflowModel);

            Execute(modelId, logEntry);
        }
    }
}