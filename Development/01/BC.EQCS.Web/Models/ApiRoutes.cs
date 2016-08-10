using System;

namespace BC.EQCS.Web.Models
{
    public class ApiRoutes
    {
        public static class Country
        {
            public const string Name = "Country";
            public const string Route = "api/country";
        }

        public static class NotificationWorkNote
        {
            public const string Name = "NotificationWorkNote";
            public const string Route = "api/notification/{incidentId:int}/SendWorkNoteNotification";
        }

        public static class NotificationRaisedIncident
        {
            public const string Name = "NotificationRaisedIncident";
            public const string Route = "api/notification/{incidentId:int}/SendRaisedIncidentNotification";
        }

        public static class NotificationAcceptedIncident
        {
            public const string Name = "NotificationAcceptedIncident";
            public const string Route = "api/notification/{incidentId:int}/SendAcceptedIncidentNotification";
        }

        public static class NotificationRejectIncident
        {
            public const string Name = "NotificationRejectIncident";
            public const string Route = "api/notification/{incidentId:int}/SendRejectIncidentNotification";
        }

        public static class NotificationCloseAction
        {
            public const string Name = "NotificationCloseAction";
            public const string Route = "api/notification/{incidentId:int}/SendCloseActionNotification/{actionId:int}";
        }

        public static class NotificationNewAction
        {
            public const string Name = "NotificationNewAction";
            public const string Route = "api/notification/{incidentId:int}/SendNewActionNotification/{actionId:int}";
        }

        public static class NotificationEditAction
        {
            public const string Name = "NotificationEditAction";
            public const string Route = "api/notification/{incidentId:int}/SendEditActionNotification/{actionId:int}";
        }

        public static class NotificationCloseIncident
        {
            public const string Name = "NotificationCloseIncident";
            public const string Route = "api/notification/{incidentId:int}/SendCloseIncidentNotification";
        }

        public static class Incident
        {
            public const string Name = "IncidentNoId";
            public const string Route = "api/incident";
        }

        public static class IncidentById
        {
            public const string Name = "IncidentById";
            public const string Route = "api/incident/{id:int}";
        }

        public static class IncidentByIdResource
        {
            public const string Name = "IncidentByIdResource";
            public const string Route = "api/incident/{id:int}/resource";
        }

        public static class IncidentByIdSchema
        {
            public const string Name = "IncidentByIdSchema";
            public const string Route = "api/incident/{id:int}/resource/schema";
        }

        public static class IncidentByIdAcceptance
        {
            public const string Name = "IncidentByIdAcceptance";
            public const string Route = "api/incident/{id:int}/acceptance";
        }

        public static class IncidentByIdAction
        {
            public const string Name = "IncidentByIdAction";
            public const string Route = "api/incident/{id:int}/action/";
        }

        public static class IncidentActions
        {
            public const string Name = "IncidentActions";
            public const string Route = "/odata/IncidentActionListing/";
        }
        
        public static class IncidentActivity
        {
            public const string Name = "IncidentActivity";
            public const string Route = "/odata/IncidentActivityListing/";
        }

        public static class IncidentByIdClosure
        {
            public const string Name = "IncidentByIdClosure";
            public const string Route = "api/incident/{id:int}/closure";
        }

        public static class IncidentByIdActivityLog
        {
            public const string Name = "IncidentByIdActivityLog";
            public const string Route = "api/incident/{id:int}/log/";
        }    
        
        public static class IncidentByIdActionAssignableUsers
        {
            public const string Name = "IncidentByIdActionAssignableUsers";
            public const string Route = "api/incident/{id:int}/assignableusers/";
        }

        public static class IncidentByIdNote
        {
            public const string Name = "IncidentByIdNote";
            public const string Route = "api/incident/{id:int}/note/";
        }

        public static class IncidentByIdPersistence
        {
            public const string Name = "IncidentByIdPersistence";
            public const string Route = "api/incident/{id:int}/persistence";
        }

        public static class IncidentByIdRejection
        {
            public const string Name = "IncidentByIdRejection";
            public const string Route = "api/incident/{id:int}/rejection";
        }

        public static class IncidentByIdReopening
        {
            public const string Name = "IncidentByIdReopening";
            public const string Route = "api/incident/{id:int}/reopening";
        }

        public static class IncidentByIdSubmission
        {
            public const string Name = "IncidentByIdSubmission";
            public const string Route = "api/incident/{id:int}/submission";
        }

        public static class IncidentSubmission
        {
            public const string Name = "IncidentSubmission";
            public const string Route = "api/incident/submission";
        }

        public static class IncidentAdminUnit
        {
            public const string Name = "IncidentAdminUnit";
            public const string Route = "api/incident/adminunit";
        }

        [Obsolete("IncidentByIdCandidateById")]
        public static class IncidentCandidateOld
        {
            public const string Name = "IncidentCandidate";
            public const string Route = "api/incident/candidate/{id:int}";
        }

        [Obsolete("IncidentByIdCandidate")]
        public static class IncidentCandidateSave
        {
            public const string Name = "IncidentCandidateSave";
            public const string Route = "api/incident/candidate";
        }

        public static class IncidentByIdCandidate
        {
            public const string Name = "IncidentByIdCandidate";
            public const string Route = "api/incident/{id:int}/candidate";
        }

        public static class IncidentByIdCandidateById
        {
            public const string Name = "IncidentByIdCandidateById";
            public const string Route = "api/incident/{incidentId:int}/candidate/{id:int}";
        }

        public static class IncidentByIdCandidateByIdPersistence
        {
            public const string Name = "IncidentByIdCandidateByIdPersistence";
            public const string Route = "api/incident/{incidentId:int}/candidate/{id:int}/persistence";
        }

        public static class IncidentCandidateDelete
        {
            public const string Name = "IncidentCandidateDelete";
            public const string Route = "api/incident/candidate/schema";
        }

        public static class IncidentClass
        {
            public const string Name = "IncidentClass";
            public const string Route = "api/incident/class";
        }

        public static class IncidentOrgType
        {
            public const string Name = "IncidentOrgType";
            public const string Route = "api/incident/orgtype";
        }

        public static class IncidentProduct
        {
            public const string Name = "IncidentProduct";
            public const string Route = "api/incident/product";
        }

        public static class IncidentRiskRating
        {
            public const string Name = "IncidentRiskRating";
            public const string Route = "api/incident/risk";
        }

        public static class IncidentResidualRiskRating
        {
            public const string Name = "IncidentResidualRiskRatings";
            public const string Route = "api/incident/residualRiskRatings";
        }

        

        public static class IncidentResource
        {
            public const string Name = "IncidentResource";
            public const string Route = "api/incident/resource";
        }

        public static class IncidentSchema
        {
            public const string Name = "IncidentSchema";
            public const string Route = "api/incident/resource/schema";
        }


        public static class Document
        {
            public const string Name = "Document";
            public const string Route = "api/document";
        }

        public static class DocumentById
        {
            public const string Name = "DocumentById";
            public const string Route = "api/document/{id:int}";
        }

        public static class IncidentActionByIdDocument
        {
            public const string Name = "IncidentActionByIdDocument";
            public const string Route = "api/incident/actions/{id:int}/document";
        }

        public static class IncidentSubMenu
        {
            public const string Name = "IncidentSubMenu";
            public const string Route = "api/incident/subMenu/{id:int}";
        }

        public static class DownloadDocumentById
        {
            public const string Name = "DownloadDocument";
            public const string Route = "api/downloaddocument/{id:int}";
        }

        public static class DeleteDocumentById
        {
            public const string Name = "DeleteDocument";
            public const string Route = "api/deletedocument/{id:int}";
        }

        public static class HealthMonitoring
        {
            public const string Name = "HealthMonitoring";
            public const string Route = "healthmonitor";
        }

        public static class NotificationFyiMessage
        {
            public const string Name = "NotificationFyiMessage";
            public const string Route = "api/notification/SendFyiNotification";
        }

        public static class UkviImmediateReportType
        {
            public const string Name = "UkviImmediateReportType";
            public const string Route = "api/incident/ukviimmediatereporttype";
        }

        public static class SiteAuthorisation
        {
            public const string Name = "SiteAuthorisation";
            public const string Route = "api/authorisation";
        }

        public static class IncidentAuthorisation
        {
            public const string Name = "IncidentAuthorisation";
            public const string Route = "api/incident/authorisation";
        }

        public static class AuditingAuthorisation
        {
            public const string Name = "AuditingAuthorisation";
            public const string Route = "api/auditing/authorisation";
        }

        public static class DocumentsByActionId
        {
            public const string Name = "DocumentsByActionId";
            public const string Route = "api/documentsByActionId/{id:int}";           
        }
    }
}