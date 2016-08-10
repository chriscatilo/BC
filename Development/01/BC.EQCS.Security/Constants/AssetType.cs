namespace BC.EQCS.Security.Constants
{
    public static class AssetType
    {
        /* INCIDENT */
        public const string IncidentModuleAccess = "INCIDENT__MODULE_ACCESS";
        public const string IncidentViewListIncidents = "INCIDENT__VIEW_LIST_INCIDENTS";
        public const string IncidentViewListClosedActions = "INCIDENT__VIEW_LIST_CLOSED_ACTIONS";
        public const string IncidentDraftRaise = "INCIDENT__DRAFT_RAISE";
        public const string IncidentAcceptReject = "INCIDENT__ACCEPT_REJECT";
        public const string IncidentEditPreAcceptReject = "INCIDENT__EDIT_PRE_ACCEPT";
        public const string IncidentEditPostAcceptReject = "INCIDENT__EDIT_POST_ACCEPT";
        public const string IncidentClose = "INCIDENT__CLOSE";
        public const string IncidentReopen = "INCIDENT__REOPEN";
        public const string IncidentDelete = "INCIDENT__DELETE";
        public const string IncidentOverrideDefaultRiskRating = "INCIDENT__OVERRIDE_DEFAULT_RISK_RATING";
        public const string IncidentPostSubmitSaveAllowed = "INCIDENT__POST_SUBMIT_SAVE_ALLOWED";

        public const string IncidentCanViewUkvi = "INCIDENT__CAN_VIEW_UKVI";
        
        
        /* Incident - Action */
        public const string IncidentViewListActions = "INCIDENT__VIEW_LIST_ACTIONS";
        public const string IncidentAddAction = "INCIDENT__ADD_ACTION";
        public const string IncidentUpdateAction = "INCIDENT__UPDATE_ACTION";
        public const string IncidentFinaliseAction = "INCIDENT__FINALISE_ACTION";
        public const string IncidentReopenAction = "INCIDENT__REOPEN_ACTION";
        public const string IncidentRespondAction = "INCIDENT__RESPOND_TO_ACTION";

        /* Incident - Activity */
        public const string IncidentActivityViewList = "INCIDENT__ACTIVITY__VIEW_LIST";
        public const string IncidentActivityAddWorkNote = "INCIDENT__ACTIVITY__ADD_WORKNOTE";
        public const string IncidentActivityDisplayAllActivity = "INCIDENT__ACTIVITY__DISPLAY_ALL_ACTIVITY";
        public const string IncidentSendFYI = "INCIDENT__SEND_FYI";

        /* AUDIT */
        public const string AuditModuleAccess = "AUDIT__MODULE_ACCESS";
        public const string AuditMyAuditsAccess = "AUDIT__MY_AUDITS_ACCESS";
        public const string AuditScheduledAuditsAccess = "AUDIT__SCHEDULED_AUDITS_ACCESS";
        public const string AuditSubmittedAuditsAccess = "AUDIT__SUBMITTED_AUDITS_ACCESS";
        public const string AuditActionPlansAccess = "AUDIT__ACTION_PLANS_ACCESS";
        public const string AuditTestCentreProfile = "AUDIT__TEST_CENTRE_PROFILE";
        public const string AuditTestLocationProfile = "AUDIT__TEST_LOCATION_PROFILE";
        
        /* user Admin */
        public const string UserAdminModuleAccess = "USER_ADMIN__MODULE_ACCESS";


        
    }
}