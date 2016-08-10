// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable InconsistentNaming

using System.ComponentModel;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Models
{
    /// <summary>
    /// This class sets out to enumerate the attributes of Incident.
    /// These attributes are used drive the build of value constraints that constitute a model schema.
    /// Also used to drive the enumeration of incident attributes for the activity log
    /// IMPORTANT: KEEP THE PROPERTIES IN SEQUENCE FOR THE ACTIVITY LOG
    /// </summary>
    public class IncidentAttributes
    {
        [DisplayName(Constants.IncidentAttributeLabels.Full.RowVersion)]
        public dynamic RowVersion { get; private set; }

        [DoNotLogActivityAttribute]
        public dynamic Id { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.FormalId)]
        public dynamic FormalId { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.LoggedByUser)]
        public dynamic LoggedByUser { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CreateDate)]
        public dynamic CreateDate { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.IeltsRegion)]
        public dynamic IELTSRegion { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.Status)]
        public dynamic Status { get; private set; }

        // Details 

        [DisplayName(Constants.IncidentAttributeLabels.Full.TestCentre)]
        public dynamic TestCentre { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.TestLocation)]
        public dynamic TestLocation { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.Product)]
        public dynamic Product { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.RaisedBy)]
        public dynamic RaisedBy { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.TestDate)]
        public dynamic TestDate { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.Category)]
        public dynamic Category { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.SubCategory)]
        public dynamic SubCategory { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.RiskRating)]
        public dynamic RiskRating { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ResidualRiskRating)]
        public dynamic ResidualRiskRating { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.IncidentDate)]
        public dynamic IncidentDate { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.IncidentTime)]
        public dynamic IncidentTime { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.Description)]
        public dynamic Description { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ImmediateActionTaken)]
        public dynamic ImmediateActionTaken { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.NoOfCandidates)]
        public dynamic NoOfCandidates { get; private set; }

        // Refering Organisation 

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrgType)]
        public dynamic ReferringOrgType { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrgCountry)]
        public dynamic ReferringOrgCountry { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrganisation)]
        public dynamic ReferringOrganisation { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrgSurname)]
        public dynamic ReferringOrgSurname { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrgFirstnames)]
        public dynamic ReferringOrgFirstnames { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrgJobTitle)]
        public dynamic ReferringOrgJobTitle { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrgEmail)]
        public dynamic ReferringOrgEmail { get; private set; }

        public dynamic ReferringOrgExists { get; private set; }

        // UKVI 

        [DisplayName(Constants.IncidentAttributeLabels.Full.ReportUkvi)]
        public dynamic ReportUkvi { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.UkviFollowUpDate)]
        public dynamic UkviFollowUpDate { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.UkviImmediateReportType)]
        public dynamic UkviImmediateReportType { get; private set; }

        // Candidate

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateNumber)]
        public dynamic CandidateNumber { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateSurname)]
        public dynamic CandidateSurname { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateFirstnames)]
        public dynamic CandidateFirstnames { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateNationality)]
        public dynamic CandidateNationality { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateAddress)]
        public dynamic CandidateAddress { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateDateOfBirth)]
        public dynamic CandidateDateOfBirth { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateGender)]
        public dynamic CandidateGender { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateIdDocumentNumber)]
        public dynamic CandidateIdDocumentNumber { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateTrfNumber)]
        public dynamic CandidateTrfNumber { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateDateTrfCancelled)]
        public dynamic CandidateDateTrfCancelled { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateUkviRefNumber)]
        public dynamic CandidateUKVIRefNumber { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Full.CandidateRowVersion)]
        public dynamic CandidateRowVersion { get; private set; }
    }
}