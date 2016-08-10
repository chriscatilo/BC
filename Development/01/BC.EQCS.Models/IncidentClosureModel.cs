using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    public class IncidentClosureModel : IncidentWorkflowModel
    {
        public string Comments { get; set; }

        public IncidentClosureModel() : base(IncidentStatus.Closed, IncidentActivityLogType.Closure)
        {
            
        }
    }
}