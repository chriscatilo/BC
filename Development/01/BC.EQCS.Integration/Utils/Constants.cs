namespace BC.EQCS.Integration.Utils
{
    public static class Constants
    {
        public const string SelfHostUrl = "http://localhost:9001";

        public static class FeatureKeys
        {
            public const string
                ClientResponse = "ClientResponse",

                GivenTableOfPeople = "GivenTableOfPeople",
                GivenPerson = "GivenPerson",
                SavedPerson = "RetrievedPerson",

                GivenTableOfIncidentsToPersist = "GivenTableOfIncidentsToPersist",
                GivenTableOfIncidentsToView = "GivenTableOfIncidentsToView",

                GivenIncident = "GivenIncident",
                SavedIncident = "RetrievedIncident",

                IncidentRetrieved = "IncidentRetrieved",
                IncidentPersistedRetrieved = "IncidentPersistedRetrieved",
                IncidentSchema = "IncidentSchema",
                IncidentIdUnderTest = "IncidentIdUnderTest",
                IncidentUriUnderTest = "IncidentUriUnderTest",
                IncidentUnderTest = "IncidentUnderTest",
                IncidentWorkflowActivityUnderTest = "IncidentWorkflowActivityUnderTest",
                IncidentActivityLogRetrieved = "IncidentActivityLogRetrieved",

                GivenTableOfCandidatesToPersist = "GivenTableOfCandidatesToPersist",
                GivenTableOfCandidatesToView = "GivenTableOfCandidatesToView",
                GivenIncidentCandidate = "GivenIncidentCandidate",
                CandidateUriUnderTest = "CandidateUriUnderTest",
                CandidateRetrieved = "CandidateRetrieved",
                CandidatesRetrieved = "CandidateRetrieved",
                CandidateUnderTest = "CandidateUnderTest",

                GivenTableOfActionsToPersist = "GivenTableOfActionsToPersist",
                GivenTableOfActionsToView = "GivenTableOfActionsToView",
                GivenIncidentAction = "GivenIncidentAction",
                ActionUriUnderTest = "ActionUriUnderTest",
                ActionRetrieved = "ActionRetrieved",
                ActionsRetrieved = "ActionRetrieved",
                ActionUnderTest = "ActionUnderTest",

                ResultantDocumentModel = "ResultantDocumentModel",
                GivenValidDocument = "GivenValidDocument",
                GivenValidEmptyDocument ="GivenValidEmptyDocument",
                GivenValidDocumentExceeds10MB = "GivenValidDocumentExceeds10MB",
                GivenInValidDocument = "GivenInValidDocument",
                ValidationFailureException = "ValidationFailureException",
                Location = "Location",
                Success = "Success",
                Failure = "Failure";
        }

        public static class Urls
        {
            public static readonly string UserAdminSearchFirstNameSurname = SelfHostUrl + "/api/useradmin/search?firstName={0}&surname={1}";
            public static readonly string UserAdminUserUri = SelfHostUrl + "/api/useradmin/{0}";
            public static readonly string UserAdmin = SelfHostUrl + "/api/useradmin";

            public static readonly string Incident = SelfHostUrl + "/api/incident/";
            public static readonly string IncidentById = SelfHostUrl + "/api/incident/{0}";
            public static readonly string IncidentPersistance = SelfHostUrl + "/api/incident/persistence";
            public static readonly string IncidentPersistanceById = SelfHostUrl + "/api/incident/persistence/{0}";
            public static readonly string IncidentSubmission = SelfHostUrl + "/api/incident/submission";
            public static readonly string IncidentSubmissionById = SelfHostUrl + "/api/incident/submission/{0}";
            public static readonly string IncidentReOpeningById = SelfHostUrl + "/api/incident/reopening/{0}";
            public static readonly string IncidentAcceptanceById = SelfHostUrl + "/api/incident/acceptance/{0}";
            public static readonly string IncidentRejectionById = SelfHostUrl + "/api/incident/rejection/{0}";
            public static readonly string IncidentClosureById = SelfHostUrl + "/api/incident/closure/{0}";
            public static readonly string IncidentActivityLogById = SelfHostUrl + "/api/incident/log/{0}";

            public static readonly string Document = SelfHostUrl + "/api/document/";
        }
    }
}

