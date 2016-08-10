namespace BC.EQCS.Models.Enums
{
    public static class Constants
    {
        public static class AdminUnitTypes
        {
            public const string
                Root = "ROOT",
                TestCentre = "TEST_CENTRE",
                TestLocation = "TEST_LOCATION";
        }

        public static class IncidentClassTypes
        {
            public const string
                Root = "Root",
                IncidentType = "IncidentType",
                Category = "Category",
                SubCategory = "SubCategory";
        }

        public static class IncidentAttributeLabels
        {
            //Full is primarily used by Activity Log
            public static class Full
            {
                public const string
                    RowVersion = "Row Version",
                    FormalId = "Incident Number",
                    LoggedByUser = "Logged By",
                    CreateDate = "Logged On (GMT)",
                    IeltsRegion = "IELTS Region",
                    Status = "Status",
                    IncidentDate = "Incident Date",
                    IncidentTime = "Incident Time",
                    ImmediateActionTaken = "Immediate action that was taken",
                    Description = "Description of incident",
                    Product = "Product",
                    TestCentre = "Test Centre",
                    TestLocation = "Test Location",
                    RaisedBy = "Reported By",
                    Category = "Category",
                    SubCategory = "Sub Category",
                    RiskRating = "Risk Rating",
                    ResidualRiskRating = "Residual Risk Rating",
                    TestDate = "Test Date",
                    NoOfCandidates = "No of Candidates Affected (if applicable) by the Incident",
                    ReferringOrgSurname = "Recognising Organisation Contact Surname",
                    ReferringOrgFirstnames = "Recognising Organisation Contact First Name/s",
                    ReferringOrgJobTitle = "Recognising Organisation Job Title",
                    ReferringOrgEmail = "Recognising Organisation Email",
                    ReferringOrgType = "Recognising Organisation Type",
                    ReferringOrgCountry = "Recognising Organisation Country",
                    ReferringOrganisation = "Recognising Organisation Name",
                    ReportUkvi = "Report to UKVI?",
                    UkviFollowUpDate = "UKVI Follow Up Date (GMT)",
                    UkviImmediateReportType = "UKVI Immediate Report",
                    CandidateNumber = "Candidate Number",
                    CandidateSurname = "Candidate Surname",
                    CandidateFirstnames = "Candidate First Name(s)",
                    CandidateAddress = "Candidate Address",
                    CandidateDateOfBirth = "Candidate Date Of Birth",
                    CandidateGender = "Candidate Gender",
                    CandidateIdDocumentNumber = "Candidate Document Id Number",
                    CandidateTrfNumber = "Candidate TRF Number",
                    CandidateDateTrfCancelled = "Candidate Date TRF Cancelled",
                    CandidateUkviRefNumber = "Candidate UKVI Reference Number",
                    CandidateNationality = "Candidate Nationality",
                    CandidateRowVersion = "Candidate RowVersion";

            }

            public static class Short
            {
                public const string
                    Description = "Description",

                    ReferringOrgSurname = "Contact Surname",
                    ReferringOrgFirstnames = "Contact First Name/s",
                    ReferringOrgJobTitle = "Job Title",
                    ReferringOrgEmail = "Email",
                    ReferringOrgType = "Type",
                    ReferringOrgCountry = "Country",
                    ReferringOrganisation = "Name",
                    ReferringOrgExists = "Exists",

                    UkviImmediateReportType = "Immediate Report",

                    CandidateNumber = "Number",
                    CandidateSurname = "Surname",
                    CandidateFirstnames = "First Name(s)",
                    CandidateAddress = "Address",
                    CandidateDateOfBirth = "Date Of Birth",
                    CandidateGender = "Gender",
                    CandidateIdDocumentNumber = "Document Id Number",
                    CandidateTrfNumber = "TRF Number",
                    CandidateDateTrfCancelled = "Date TRF Cancelled",
                    CandidateUkviRefNumber = "UKVI Reference Number",
                    CandidateNationality = "Nationality";


            }
        }
    }
}