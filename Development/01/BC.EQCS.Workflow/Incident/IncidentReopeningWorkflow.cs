using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Workflow.Incident
{
    public class IncidentReopeningWorkflow : Workflow<Entities.Models.Incident, IncidentReopeningModel, IncidentStatus>
    {
        public IncidentReopeningWorkflow(
            IEntityFactory entityFactory,
            ILookup<IncidentStatus, IncidentStatus> availableTransitions,
            IEnumerable<IWorkflowActivityLogger<IncidentStatus, IncidentReopeningModel>> workflowActivityLoggers)
            : base(
                entityFactory,
                availableTransitions,
                workflowActivityLoggers)
        {
            GetStatus = entity => entity.Status;

            SetStatus = (entity, status) => entity.Status = status;
        }

        protected override void OnExecution(int id, IncidentReopeningModel workflowModel)
        {
        }
    }
}