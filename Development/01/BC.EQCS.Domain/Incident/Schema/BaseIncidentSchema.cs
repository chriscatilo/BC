using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Domain.Incident.Schema
{
    internal class BaseIncidentSchema : ModelSchema<IncidentAttributes>
    {
        private BaseIncidentSchema()
        {
            MapBaseDetails();
            MapBaseReferringOrgType();
            MapBaseUkvi();
            MapBaseCandidate();
        }

        public static BaseIncidentSchema Create()
        {
            return new BaseIncidentSchema();
        }

        private void MapBaseDetails()
        {
            BuildFor(model => model.Id, "Id", ValueConstraint.ViewOnly);
            BuildFor(model => model.FormalId, Constants.IncidentAttributeLabels.Full.FormalId, ValueConstraint.ViewOnly);
            BuildFor(model => model.LoggedByUser, Constants.IncidentAttributeLabels.Full.LoggedByUser, ValueConstraint.ViewOnly);
            BuildFor(model => model.Status, Constants.IncidentAttributeLabels.Full.Status, ValueConstraint.ViewOnly);
            BuildFor(model => model.CreateDate, Constants.IncidentAttributeLabels.Full.CreateDate, ValueConstraint.ViewOnly);
            BuildFor(model => model.IELTSRegion, Constants.IncidentAttributeLabels.Full.IeltsRegion, ValueConstraint.ViewOnly);
            BuildFor(model => model.RowVersion, null, ValueConstraint.ServerResolved);

            BuildFor(model => model.IncidentDate, Constants.IncidentAttributeLabels.Full.IncidentDate);
            BuildFor(model => model.Description, Constants.IncidentAttributeLabels.Full.Description);
            BuildFor(model => model.ImmediateActionTaken, Constants.IncidentAttributeLabels.Full.ImmediateActionTaken);
            BuildFor(model => model.Product, Constants.IncidentAttributeLabels.Full.Product);
            BuildFor(model => model.NoOfCandidates, Constants.IncidentAttributeLabels.Full.NoOfCandidates);
            BuildFor(model => model.RaisedBy, Constants.IncidentAttributeLabels.Full.RaisedBy, maxLength: 50);
            BuildFor(model => model.TestCentre, Constants.IncidentAttributeLabels.Full.TestCentre);
            BuildFor(model => model.TestLocation, Constants.IncidentAttributeLabels.Full.TestLocation);
            BuildFor(model => model.Category, Constants.IncidentAttributeLabels.Full.Category);
            BuildFor(model => model.SubCategory, Constants.IncidentAttributeLabels.Full.SubCategory);
            BuildFor(model => model.RiskRating, Constants.IncidentAttributeLabels.Full.RiskRating);
            BuildFor(model => model.ResidualRiskRating, Constants.IncidentAttributeLabels.Full.ResidualRiskRating);
            BuildFor(model => model.TestDate, Constants.IncidentAttributeLabels.Full.TestDate);
            BuildFor(model => model.IncidentTime, Constants.IncidentAttributeLabels.Full.IncidentTime, maxLength: 5);
        }

        private void MapBaseReferringOrgType()
        {
            BuildFor(model => model.ReferringOrgSurname, Constants.IncidentAttributeLabels.Short.ReferringOrgSurname, maxLength: 255);
            BuildFor(model => model.ReferringOrgFirstnames, Constants.IncidentAttributeLabels.Short.ReferringOrgFirstnames, maxLength: 255);
            BuildFor(model => model.ReferringOrgJobTitle, Constants.IncidentAttributeLabels.Short.ReferringOrgJobTitle, maxLength: 255);
            BuildFor(model => model.ReferringOrgEmail, Constants.IncidentAttributeLabels.Short.ReferringOrgEmail, maxLength: 255);
            BuildFor(model => model.ReferringOrgType, Constants.IncidentAttributeLabels.Short.ReferringOrgType);
            BuildFor(model => model.ReferringOrgCountry, Constants.IncidentAttributeLabels.Short.ReferringOrgCountry);
            BuildFor(model => model.ReferringOrganisation, "Organisation");
            BuildFor(model => model.ReferringOrgExists);
        }

        private void MapBaseUkvi()
        {
            BuildFor(model => model.ReportUkvi, Constants.IncidentAttributeLabels.Full.ReportUkvi);
            BuildFor(model => model.UkviFollowUpDate, Constants.IncidentAttributeLabels.Full.UkviFollowUpDate);
            BuildFor(model => model.UkviImmediateReportType, Constants.IncidentAttributeLabels.Short.UkviImmediateReportType);
        }

        private void MapBaseCandidate()
        {
            BuildFor(model => model.CandidateNumber, Constants.IncidentAttributeLabels.Full.CandidateNumber, maxLength: 50);
            BuildFor(model => model.CandidateSurname, Constants.IncidentAttributeLabels.Short.CandidateSurname, maxLength: 255);
            BuildFor(model => model.CandidateFirstnames, Constants.IncidentAttributeLabels.Short.CandidateFirstnames, maxLength: 255);
            BuildFor(model => model.CandidateAddress, Constants.IncidentAttributeLabels.Short.CandidateAddress);
            BuildFor(model => model.CandidateDateOfBirth, Constants.IncidentAttributeLabels.Short.CandidateDateOfBirth);
            BuildFor(model => model.CandidateGender, Constants.IncidentAttributeLabels.Short.CandidateGender);
            BuildFor(model => model.CandidateIdDocumentNumber, Constants.IncidentAttributeLabels.Short.CandidateIdDocumentNumber, maxLength: 50);
            BuildFor(model => model.CandidateTrfNumber, Constants.IncidentAttributeLabels.Short.CandidateTrfNumber);
            BuildFor(model => model.CandidateDateTrfCancelled, Constants.IncidentAttributeLabels.Short.CandidateDateTrfCancelled);
            BuildFor(model => model.CandidateUKVIRefNumber, Constants.IncidentAttributeLabels.Short.CandidateUkviRefNumber, maxLength: 50);
            BuildFor(model => model.CandidateNationality, Constants.IncidentAttributeLabels.Short.CandidateNationality);
            BuildFor(model => model.CandidateRowVersion, null, ValueConstraint.ServerResolved);
        }
    }
}