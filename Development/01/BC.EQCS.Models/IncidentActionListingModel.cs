using System;

namespace BC.EQCS.Models
{
    public class IncidentActionListingModel
    {
        public Int32 Id { get; set; }
        public Int32 IncidentId { get; set; }
        public String AssignedTo { get; set; }
        public String AssignedBy { get; set; }
        public DateTime AssignedOn { get; set; }
        public String ActionDescription { get; set; }
        public String Comments { get; set; }
        public String Status { get; set; }
        public Byte StatusId { get; set; }
        public Int32? AssignedById { get; set; }
        public bool IsAuthorised { get; set; }
        public String FileIds { get; set; }
        public String FileNames { get; set; }
    }
}
