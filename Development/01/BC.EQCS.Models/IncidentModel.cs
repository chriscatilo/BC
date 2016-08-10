using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BC.EQCS.Models.Enums;
using Newtonsoft.Json;

namespace BC.EQCS.Models
{
    public class IncidentModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        // TODO Chris: Refactor out as status should be read from the view model
        public IncidentStatus Status { get; set; }

        public DateTime? IncidentDate { get; set; }
        public string IncidentTime { get; set; }
        public string Description { get; set; }
        public string ImmediateActionTaken { get; set; }
        public string Product { get; set; }
        public string RaisedBy { get; set; }
        public string TestCentre { get; set; }
        public string TestLocation { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string RiskRating { get; set; }
        public string ResidualRiskRating { get; set; }
        public DateTime? TestDate { get; set; }
        public bool? ReportUkvi { get; set; }
        public string ReferringOrgSurname { get; set; }
        public string ReferringOrgFirstnames { get; set; }
        public string ReferringOrgJobTitle { get; set; }
        public string ReferringOrgEmail { get; set; }
        public string ReferringOrgType { get; set; }
        public string ReferringOrgCountry { get; set; }
        public string ReferringOrganisation { get; set; }
        public bool? ReferringOrgExists { get; set; }
        public int? NoOfCandidates { get; set; }
        public DateTime? UkviFollowUpDate { get; set; }
        public string UkviImmediateReportType { get; set; }

        public IEnumerable<IncidentActionModel> IncidentActions { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
