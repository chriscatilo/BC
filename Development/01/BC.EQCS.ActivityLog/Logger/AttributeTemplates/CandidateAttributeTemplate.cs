// ReSharper disable UnusedAutoPropertyAccessor.Local

using System.ComponentModel;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.ActivityLog.Logger.AttributeTemplates
{
    /// <summary>
    /// Used to drive the enumeration of candidates for the activity log
    /// IMPORTANT: KEEP THE PROPERTIES IN SEQUENCE FOR THE ACTIVITY LOG
    /// </summary>
    // TODO #4034 Replace use of CandidateAttributeTemplate with IncidentAttributes
    public class CandidateAttributeTemplate
    {
        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateNumber)]
        public dynamic Number { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateSurname)]
        public dynamic Surname { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateFirstnames)]
        public dynamic Firstnames { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateNationality)]
        public dynamic Nationality { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateAddress)]
        public dynamic Address { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateDateOfBirth)]
        public dynamic DateOfBirth { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateGender)]
        public dynamic Gender { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateIdDocumentNumber)]
        public dynamic IdDocumentNumber { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateTrfNumber)]
        public dynamic TrfNumber { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateDateTrfCancelled)]
        public dynamic DateTrfCancelled { get; private set; }

        [DisplayName(Constants.IncidentAttributeLabels.Short.CandidateUkviRefNumber)]
        public dynamic UKVIRefNumber { get; private set; }
    }
}