using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Workflow.Incident
{
    public class IncidentClosureWorkflow : Workflow<Entities.Models.Incident, IncidentClosureModel, IncidentStatus>
    {
        public IncidentClosureWorkflow(
            IEntityFactory entityFactory,
            ILookup<IncidentStatus, IncidentStatus> availableTransitions,
            IEnumerable<IWorkflowActivityLogger<IncidentStatus, IncidentClosureModel>> workflowActivityLoggers)
            : base(
                entityFactory,
                availableTransitions,
                workflowActivityLoggers)
        {
            GetStatus = entity => entity.Status;

            SetStatus = (entity, status) => entity.Status = status;
        }

        protected override void OnExecution(int id, IncidentClosureModel workflowModel)
        {
        }
    }
}