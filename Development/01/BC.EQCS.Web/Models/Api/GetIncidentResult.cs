using System.Collections.Generic;
using BC.EQCS.Models;

namespace BC.EQCS.Web.Models.Api
{
    public class GetIncidentResult
    {
        public string Uri { get; set; }
        public IncidentViewModel Model { get; set; }
        public string Candidates { get; set; }
        public string Resource { get; set; }
        public string Persisted { get; set; }

        // TODO Chris: Remove this
        public IEnumerable<AvailableCommandLink> Commands { get; set; }

        public string[] TabsAvailable { get; set; }
    }
}