using System;
using System.Collections.Generic;
using BC.EQCS.Entities;
using System.ComponentModel.DataAnnotations;

namespace BC.EQCS.Models
{
    public class IncidentActionModel
    {
        public Int32 Id { get; set; }
        public Int32 IncidentId { get; set; }
        public String ActionDescription { get; set; }
        public DateTime AssignedOn { get; set; }
        public Boolean AssignedToTestCentre { get; set; }
        public String[] AssignedTo { get; set; }
        public String ActionResponse { get; set; }
        public IncidentActionStatus Status { get; set; }
        public IEnumerable<DocumentViewModel> DocumentList { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
