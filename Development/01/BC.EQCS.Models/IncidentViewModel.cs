// ReSharper disable InconsistentNaming

using System;
using BC.EQCS.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BC.EQCS.Models
{
    public class IncidentViewModel
    {
        public int Id { get; set; }
        public string FormalId { get; set; }
        public string LoggedByUser { get; set; }
        public string LoggedByUserRole { get; set; }
        public DateTime CreateDate { get; set; }

        [JsonConverter(typeof (StringEnumConverter))]
        public IncidentStatus Status { get; set; }

        // Details 
        public DateTime? IncidentDate { get; set; }
        public string IncidentTime { get; set; }
        public string Description { get; set; }
        public string ImmediateActionTaken { get; set; }
        public string Product { get; set; }
        public string IELTSRegion { get; set; }
        public string TestCentre { get; set; }
        public string TestLocation { get; set; }
        public string RaisedBy { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string RiskRating { get; set; }
        public string ResidualRiskRating { get; set; }
        public DateTime? TestDate { get; set; }
        public int? NoOfCandidates { get; set; }
        // Refering Organisation 
        public string ReferringOrgName { get; set; }
        public string ReferringOrgSurname { get; set; }
        public string ReferringOrgFirstnames { get; set; }
        public string ReferringOrgJobTitle { get; set; }
        public string ReferringOrgEmail { get; set; }
        public string ReferringOrgType { get; set; }
        public string ReferringOrgCountry { get; set; }
        // UKVI 
        public bool? IsUkvi { get; set; }
        public bool? ReportUkvi { get; set; }
        public DateTime? UkviFollowUpDate { get; set; }
        public string UkviImmediateReportType { get; set; }

        public string IncidentClassCode { get; set; }
    }
}