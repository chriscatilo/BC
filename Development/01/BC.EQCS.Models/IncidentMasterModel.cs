using System.ComponentModel.DataAnnotations;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    // NOTE: DO NOT EXTEND - Obtain the other attributes from IncidentModel
    public class IncidentMasterModel
    {
        public int Id { get; set; }

        public IncidentStatus Status { get; set; }

        public string IncidentClass { get; set; }

        public string TestCentre { get; set; }

        public string TestLocation { get; set; }

        public string Category { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}