using BC.EQCS.Models;

namespace BC.EQCS.Integration.Incident
{
    public class GivenIncidentCandidate
    {
        public IncidentCandidateModel ForPersistence { get; set; }

        public IncidentCandidateViewModel ForViewing { get; set; }
    }

    public class GivenIncidentAction
    {
        public IncidentActionModel ForPersistence { get; set; }

        public IncidentActionViewModel ForViewing { get; set; }
    }
}