// ReSharper disable UnusedAutoPropertyAccessor.Local

using System;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Entities.Models
{
    public class IncidentView
    {
        public int Id { get; private set; }
        public string FormalId { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string LoggedByUser { get; private set; }
        public string LoggedByUserRole { get; private set; }

        public DateTime? RaisedDate { get; private set; }
        public IncidentStatus Status { get; private set; }
        // Details 
        public DateTime? IncidentDate { get; private set; }
        public string IncidentTime { get; private set; }
        public string Description { get; private set; }
        public string ImmediateActionTaken { get; private set; }
        public string Product { get; private set; }
        public string IELTSRegion { get; private set; }
        public string TestCentre { get; private set; }
        public string TestLocation { get; private set; }
        public string RaisedBy { get; private set; }
        public string IncidentType { get; private set; }
        public string Category { get; private set; }
        public string SubCategory { get; private set; }
        public string RiskRating { get; private set; }
        public string ResidualRiskRating { get; private set; }
        public DateTime? TestDate { get; private set; }

        // Refering Organisation 
        public string ReferringOrgName { get; private set; }
        public string ReferringOrgSurname { get; private set; }
        public string ReferringOrgFirstnames { get; private set; }
        public string ReferringOrgJobTitle { get; private set; }
        public string ReferringOrgEmail { get; private set; }
        public string ReferringOrgType { get; private set; }
        public string ReferringOrgCountry { get; private set; }

        // UKVI 
        public bool? IsUkvi { get; set; }
        public bool? ReportUkvi { get; private set; }
        public DateTime? UkviFollowUpDate { get; private set; }
        public string UkviImmediateReportType { get; private set; }

        public int? NumberOfCandidatesAffected { get; set; }

        public string IncidentClassCode { get; set; }
    }
}   