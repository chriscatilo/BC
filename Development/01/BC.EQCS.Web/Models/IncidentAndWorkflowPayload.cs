using BC.EQCS.Models;

namespace BC.EQCS.Web.Models
{
    public class IncidentAndWorkflowPayload<TWorkflow>
        where TWorkflow: IncidentWorkflowModel
    {
        public IncidentModel IncidentModel { get; set; }

        public TWorkflow WorkflowModel { get; set; }
    }
}