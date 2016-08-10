using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BC.EQCS.Models
{
    public class IncidentCandidateViewModel
    {
        // these identifiers are not exposed to the client and are only used internally by the server.
        // the api should return the unique location identifier of this candidate instead 
        // and all verb operations should be thru that location
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int IncidentId { get; set; }

        public string Number { get; set; }
        public string Surname { get; set; }
        public string Firstnames { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender? Gender { get; set; }

        public string IdDocumentNumber { get; set; }
        public string TrfNumber { get; set; }
        public DateTime? DateTrfCancelled { get; set; }
        public string UKVIRefNumber { get; set; }
        public string Nationality { get; set; }
    }
}
