
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'proc_AddApplicationAsset')
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE [proc_AddApplicationAsset]
END
GO

CREATE PROCEDURE [proc_AddApplicationAsset]
	@Name varchar(255),
	@ShortCode varchar(255),
	@Description varchar(255)
	AS
BEGIN	

IF(EXISTS(SELECT * FROM [dbo].[ApplicationAsset] where code = @ShortCode))
BEGIN
	UPDATE [dbo].[ApplicationAsset]
	SET [Name] = @Name,
		[Description] = @Description
	WHERE [Code] = @ShortCode
	
END
ELSE
BEGIN 
	INSERT INTO [dbo].[ApplicationAsset]
	(
		[Name],
		[Code],
		[Description]
	)
	VALUES
	(
		@Name,
		@ShortCode,
		@Description
	)
END
	
END

GO

EXEC proc_AddApplicationAsset	'Incident Module Access',	'INCIDENT__MODULE_ACCESS',	'Allows access to the incident module'

EXEC proc_AddApplicationAsset	'Draft and Raise Incident','INCIDENT__DRAFT_RAISE', 'Ability to Draft and Raise an incident' 
EXEC proc_AddApplicationAsset	'Accept or Reject an Incident','INCIDENT__ACCEPT_REJECT', 'Ability to Accept or Reject an incident'

EXEC proc_AddApplicationAsset	'Edit Pre Accept','INCIDENT__EDIT_PRE_ACCEPT', 'Ability to Edit before accepting an incident'
EXEC proc_AddApplicationAsset	'Edit Post Accept','INCIDENT__EDIT_POST_ACCEPT', 'Ability to Edit post accepting an incident'

EXEC proc_AddApplicationAsset	'Close Incident', 'INCIDENT__CLOSE', 	'Ability to close an incident'
EXEC proc_AddApplicationAsset	'Delete Incident', 'INCIDENT__DELETE', 	'Ability to delete an incident'
EXEC proc_AddApplicationAsset	'Re-Open Incident','INCIDENT__REOPEN','Ability to reopen an incident'

EXEC proc_AddApplicationAsset	'Add Action', 'INCIDENT__ADD_ACTION',	'Ability to add an action'
EXEC proc_AddApplicationAsset	'Update Action', 'INCIDENT__UPDATE_ACTION',	'Ability to update an action'
EXEC proc_AddApplicationAsset	'Respond To Action', 'INCIDENT__RESPOND_TO_ACTION',	'Ability to respond to an action'
EXEC proc_AddApplicationAsset	'Finalise Action', 'INCIDENT__FINALISE_ACTION',	'Ability to finalise an action'
EXEC proc_AddApplicationAsset	'Re-open Action', 'INCIDENT__REOPEN_ACTION',	'Ability to Re-open an action'


EXEC proc_AddApplicationAsset	'Can override Risk Rating default', 'INCIDENT__OVERRIDE_DEFAULT_RISK_RATING',	'Can override Risk Rating default'


EXEC proc_AddApplicationAsset	'List : Incident Actions', 'INCIDENT__VIEW_LIST_ACTIONS',	'Ability to view a list of incident actions'
EXEC proc_AddApplicationAsset	'List : Live Incidents', 'INCIDENT__VIEW_LIST_INCIDENTS',	'Ability  to view a list of live incidents'
EXEC proc_AddApplicationAsset	'List : Closed Incidents', 'INCIDENT__VIEW_LIST_CLOSED_ACTIONS',	'Ability to view a list of closed actions'

EXEC proc_AddApplicationAsset	'List : Incident Activity', 'INCIDENT__ACTIVITY__VIEW_LIST',	'Ability to view a list of incident activity'
EXEC proc_AddApplicationAsset	'Add Worknote', 'INCIDENT__ACTIVITY__ADD_WORKNOTE',	'Ability to add Worknote'
EXEC proc_AddApplicationAsset	'Display All Activity', 'INCIDENT__ACTIVITY__DISPLAY_ALL_ACTIVITY',	'For activiy log, Display All Activity, when this option is not selected the activity log only display''s worknotes for the current user'

EXEC proc_AddApplicationAsset	'Send FYI', 'INCIDENT__SEND_FYI',	'Ability to Send FYI'


EXEC proc_AddApplicationAsset	'Audit Module Access',	'AUDIT__MODULE_ACCESS',	'Allows access to the audit module'
EXEC proc_AddApplicationAsset	'My Audits',	'AUDIT__MY_AUDITS_ACCESS',	'Allows access to the my audit section'
EXEC proc_AddApplicationAsset	'Scheduled Audits',	'AUDIT__SCHEDULED_AUDITS_ACCESS',	'Allows access to the scheduled audits section'
EXEC proc_AddApplicationAsset	'Submitted Audits',	'AUDIT__SUBMITTED_AUDITS_ACCESS',	'Allows access to the submitted audits section'
EXEC proc_AddApplicationAsset	'Action Plans',	'AUDIT__ACTION_PLANS_ACCESS',	'Allows access to the action plans section'
EXEC proc_AddApplicationAsset	'Test Centre Profile',	'AUDIT__TEST_CENTRE_PROFILE',	'Allows access to the Test Centre Profile section'
EXEC proc_AddApplicationAsset	'Test Location Profile',	'AUDIT__TEST_LOCATION_PROFILE',	'Allows access to the Test Location Profile section'

EXEC proc_AddApplicationAsset	'User Admin Module Access',	'USER_ADMIN__MODULE_ACCESS',	'Allows access to the user admin module'
GO

EXEC proc_AddApplicationAsset	'Display System Health Status',	'DISPLAY_SYSTEM_HEALTH_STATUS',	'Display System Health Status'
EXEC proc_AddApplicationAsset	'User can view UKVI section in Incident',	'INCIDENT__CAN_VIEW_UKVI',	'This allows the user to view UKVI Report details'

EXEC proc_AddApplicationAsset	'Post submit save allowed', 'INCIDENT__POST_SUBMIT_SAVE_ALLOWED',	'Can save after an incident has been submitted'


IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'proc_AddApplicationAsset')
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE [proc_AddApplicationAsset]
END
GO