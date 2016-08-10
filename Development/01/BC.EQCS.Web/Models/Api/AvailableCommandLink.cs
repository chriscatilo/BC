using BC.EQCS.Domain.Incident;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BC.EQCS.Web.Models.Api
{
    public class AvailableCommandLink
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public IncidentCommand Name { get; set; }

        public string Href { get; set; }

        public bool AllowsPersistence { get; set; }
    }
}