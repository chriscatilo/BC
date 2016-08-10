using BC.EQCS.Models;

namespace BC.EQCS.Web.Models
{
    public static class PayloadFactory
    {
        public static IncidentAndWorkflowPayload<TWorkflow> Create<TWorkflow>(IncidentModel incidentModel, TWorkflow workflowModel)
            where TWorkflow: IncidentWorkflowModel
        {
            return new IncidentAndWorkflowPayload<TWorkflow>
            {
                IncidentModel = incidentModel,
                WorkflowModel = workflowModel
            };
        }
    }
}