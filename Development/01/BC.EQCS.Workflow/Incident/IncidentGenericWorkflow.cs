using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Workflow.Incident
{
    public class IncidentGenericWorkflow : Workflow<Entities.Models.Incident, IncidentGenericWorkflowModel, IncidentStatus>
    {
        public IncidentGenericWorkflow(
            IEntityFactory entityFactory,
            ILookup<IncidentStatus, IncidentStatus> availableTransitions,
            IEnumerable<IWorkflowActivityLogger<IncidentStatus, IncidentGenericWorkflowModel>> workflowActivityLoggers)
            : base(
                entityFactory,
                availableTransitions,
                workflowActivityLoggers)
        {
            GetStatus = entity => entity.Status;

            SetStatus = (entity, status) => entity.Status = status;
        }

        protected override void OnExecution(int id, IncidentGenericWorkflowModel workflowModel)
        {
        }
    }
}