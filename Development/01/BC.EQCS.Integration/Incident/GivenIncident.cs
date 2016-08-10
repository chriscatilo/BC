using BC.EQCS.Models;

namespace BC.EQCS.Integration.Incident
{
    public class GivenIncident
    {
        public IncidentModel ForPersistence { get; set; }

        public IncidentViewModel ForViewing { get; set; }
    }
}