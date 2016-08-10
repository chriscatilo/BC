using System.Collections.Generic;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Web.Models.Api
{
    public class SendFyiCommand
    {
        public int IncidentId { get; set; }
        public string OptionalMessage { get; set; }
        public IEnumerable<SecurityUserModel> Recipients { get; set; }
    }
}