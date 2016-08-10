using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Entities.Models
{
    public class Incident
    {
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FormalId { get; private set; }

        public IncidentStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? RaisedDate { get; set; }
        public DateTime IncidentDate { get; set; }
        public string IncidentTime { get; set; }
        public string Description { get; set; }
        public string ImmediateActionTaken { get; set; }
        public int? ProductId { get; set; }
        public string RaisedBy { get; set; }
        public int? IncidentClassId { get; set; }
        public int? RiskRatingId { get; set; }
        public int? ResidualRiskRatingId { get; set; }
        public int? LoggedById { get; set; }
        public int? TestLocationId { get; set; }
        public int? TestCentreId { get; set; }
        public DateTime? TestDate { get;set;}
        public string ReferringOrgName { get; set; }
        public string ReferringOrgSurname { get; set; }
        public string ReferringOrgFirstnames { get; set; }
        public string ReferringOrgJobTitle { get; set; }
        public string ReferringOrgEmail { get; set; }
        
        public int? ReferringOrgCountryId  { get; set; }
        public int? ReferringOrgTypeId { get; set; }
        public int? ReferringOrganisationId { get; set; }

        public bool? ReportUkvi { get; set; }
        public DateTime? UkviFollowUpDate { get; set; }
        public int? UkviImmediateReportTypeId { get; set; }

        public virtual IncidentClass IncidentClass { get; set; }
        public virtual TestCentre TestCentre { get; set; }
        public virtual TestLocation TestLocation { get; set; }
        public virtual Product Product { get; set; }
        public virtual RiskRating RiskRating { get; set; }
        public virtual ResidualRiskRating ResidualRiskRating { get; set; }
        public virtual ApplicationUser LoggedBy { get; set; }
        public virtual Organisation ReferringOrganisation { get; set; }
        public virtual OrganisationType ReferringOrgType { get; set; }
        public virtual Country ReferringOrgCountry { get; set; }
        public virtual UkviImmediateReportType UkviImmediateReportType { get; set; }

        public virtual ICollection<IncidentAction> IncidentActions { get; set; }
        public virtual ICollection<IncidentActivityLog> IncidentActivityLogs { get; set; }
        public virtual ICollection<IncidentCandidate> IncidentCandidates { get; set; }

        public int? NumberOfCandidatesAffected { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
