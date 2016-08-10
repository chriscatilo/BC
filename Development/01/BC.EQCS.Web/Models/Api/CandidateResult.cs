using BC.EQCS.Models;

namespace BC.EQCS.Web.Models.Api
{
    public class CandidateResult
    {
        public string Uri { get; set; }

        public string Persisted { get; set; }

        public IncidentCandidateViewModel Model { get; set; }
    }

    public class ActionResult
    {
        public string Uri { get; set; }

        public string Persisted { get; set; }

        public IncidentActionViewModel Model { get; set; }
    }
}