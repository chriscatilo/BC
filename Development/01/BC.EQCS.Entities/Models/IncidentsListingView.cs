using System;

namespace BC.EQCS.Entities.Models
{
    public class IncidentsListingView
    {
        public int Id { get; set; }
        public string IncidentNumber { get; set; }
        public string TestCentreNumber { get; set; }
        public string Product { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string CategoryCode { get; set; }
        public string SubCategoryCode { get; set; }
        public string DisplayedCatOrSubCat { get; set; }
        public string RaisedBy { get; set; }
        public string LoggedBy { get; set; }
        public DateTime IncidentDate { get; set; }
        public DateTime? UkviFollowUpDate { get; set; }
        public DateTime? TestDate { get; set; }
        public String Status { get; set; }
        public String StatusCode { get; set; }
        public string HasActiveAction { get; set; }
        public string VenueAdminUnitCode { get; set; }
        public int? VenueAdminUnitId { get; set; }
        public String ReportUkvi { get; set; }
    }

}
