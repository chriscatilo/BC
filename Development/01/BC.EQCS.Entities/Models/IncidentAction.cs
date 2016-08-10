using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BC.EQCS.Entities.Models
{
    public class IncidentAction
    {
        public IncidentAction()
        {
            AssignedTo = new List<ApplicationUser>();
        }
        public Int32 Id { get; set; }
        public Int32 IncidentId { get; set; }
        public Int32 AssignedById { get; set; }
        public DateTime AssignedOn { get; set; }
        public String ActionDescription { get; set; }
        public String ActionResponse { get; set; }
        public Boolean AssignedToTestCentre { get; set; }
        public ApplicationUser AssignedBy { get; set; }
        public IncidentActionStatus Status { get; set; }
        public ICollection<ApplicationUser> AssignedTo { get; set; }
        public virtual Incident Incident { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
