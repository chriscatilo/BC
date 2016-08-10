using System.Collections.Generic;

namespace BC.EQCS.Web.Models.Api
{
    public class GetIncidentResourcesResult
    {
        public string Schema { get; set; }
        public string Authorisation { get; set; }
        public IEnumerable<AvailableCommandLink> Commands { get; set; }
        public IEnumerable<NamedLink> Odata { get; set; }
        public IEnumerable<NamedLink> References { get; set; }
        public IEnumerable<NamedLink> Trees { get; set; }
    }
}