using System;

namespace BC.EQCS.Models
{
    public class IncidentsListingModel
    {
        public Int32 Id { get; set; }
        public String Status { get; set; }
        public String StatusCode { get; set; }
        public String IncidentNumber { get; set; }
        public String TestCentreNumber { get; set; }
        public String LoggedBy { get; set; }
        public String Product { get; set; }
        public String SubCategory { get; set; }
        public String Category { get; set; }
        public String DisplayedCatOrSubCat { get; set; }
        public DateTime? UkviFollowUpDate { get; set; }
        public DateTime? TestDate { get; set; }
        public DateTime? IncidentDate { get; set; }
        public string HasActiveAction { get; set; }
        public string VenueAdminUnitCode { get; set; }
        public int? VenueAdminUnitId { get; set; }
        public String ReportUkvi { get; set; }
        
    }
}
