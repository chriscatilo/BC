using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Workflow.Incident
{
    public class IncidentRejectionWorkflow : Workflow<Entities.Models.Incident, IncidentRejectionModel, IncidentStatus>
    {
        public IncidentRejectionWorkflow(
            IEntityFactory entityFactory,
            ILookup<IncidentStatus, IncidentStatus> availableTransitions,
            IEnumerable<IWorkflowActivityLogger<IncidentStatus, IncidentRejectionModel>> workflowActivityLoggers)
            : base(
                entityFactory,
                availableTransitions,
                workflowActivityLoggers)
        {
            GetStatus = entity => entity.Status;

            SetStatus = (entity, status) => entity.Status = status;
        }

        protected override void OnExecution(int id, IncidentRejectionModel workflowModel)
        {
        }
    }
}