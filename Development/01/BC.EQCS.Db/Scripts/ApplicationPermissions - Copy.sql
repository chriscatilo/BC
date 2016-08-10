



IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'proc_AddAccess')
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE [proc_AddAccess]
END
GO

CREATE PROCEDURE [proc_AddAccess]	
	@AssetShortCode varchar(255),
	@RoleShortCode varchar(255)
AS
BEGIN	
	DECLARE @RoleId INT;
	SELECT @RoleId = Id FROM [dbo].[ApplicationRole] WHERE Code = @RoleShortCode
	
	DECLARE @AssetId INT;
	SELECT @AssetId = Id FROM [dbo].[ApplicationAsset] WHERE Code = @AssetShortCode

	IF(@RoleId IS NULL)
	BEGIN
		RAISERROR ('RoleId not found',1,1)
	END

	IF(@AssetId IS NULL)
	BEGIN
		RAISERROR ('AssetId not found',1,1)
	END

	IF(NOT EXISTS(SELECT * FROM [dbo].[ApplicationPermission] WHERE [ApplicationRoleId] = @RoleId AND [ApplicationAssetId] = @AssetId))
	BEGIN
		PRINT @RoleShortCode + ' ' + @AssetShortCode

		INSERT INTO [dbo].[ApplicationPermission]
		(
			[ApplicationRoleId],
			[ApplicationAssetId]
		)
		VALUES
		(
			@RoleId,
			@AssetId
		)
	END
		

	
END

GO


IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'proc_AddAllAccessExcluding')
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE [proc_AddAllAccessExcluding]
END
GO



CREATE PROCEDURE [proc_AddAllAccessExcluding]		
	@RoleShortCode varchar(255),
	@AssetShortCodes CODES	
AS
BEGIN	

END

DECLARE @ProductTotals TABLE
(
  ProductID int, 
  Revenue money
)

EXEC proc_AddAllAccessExcluding

-- Global Operations Team - GLOBAL_OPERATIONS
INSERT INTO [dbo].[ApplicationPermission]
(
	[ApplicationRoleId],
	[ApplicationAssetId]
)
	SELECT dbo.GetIdForRoleDefinition('GLOBAL_OPERATIONS'), 
	[Id]
	FROM [dbo].[ApplicationAsset]
	WHERE [Id] NOT IN ( SELECT ID FROM ApplicationAsset WHERE Code IN ( 'DISPLAY_SYSTEM_HEALTH_STATUS'))


-- Regional Management Team - RMT
INSERT INTO [dbo].[ApplicationPermission]
(
	[ApplicationRoleId],
	[ApplicationAssetId]
)
	SELECT dbo.GetIdForRoleDefinition('RMT'), 
	[Id]
	FROM [dbo].[ApplicationAsset]
	WHERE [Id] NOT IN ( SELECT ID FROM ApplicationAsset WHERE Code IN ( 'DISPLAY_SYSTEM_HEALTH_STATUS'))


-- Country Complience Team - CCT
INSERT INTO [dbo].[ApplicationPermission]
(
	[ApplicationRoleId],
	[ApplicationAssetId]
)
	SELECT dbo.GetIdForRoleDefinition('CCT'), 
	[Id]
	FROM [dbo].[ApplicationAsset]
	WHERE [Id] NOT IN ( SELECT ID FROM ApplicationAsset WHERE Code IN ('INCIDENT__SEND_FYI','INCIDENT__CAN_VIEW_UKVI','INCIDENT__ACCEPT_REJECT', 'INCIDENT__CLOSE', 'INCIDENT__REOPEN', 'INCIDENT__ACTIVITY__DISPLAY_ALL_ACTIVITY', 'DISPLAY_SYSTEM_HEALTH_STATUS'))

-- Verifications Team - VT
INSERT INTO [dbo].[ApplicationPermission]
(
	[ApplicationRoleId],
	[ApplicationAssetId]
)
	SELECT dbo.GetIdForRoleDefinition('VT'), 
	[Id]
	FROM [dbo].[ApplicationAsset]
	WHERE  [Id] NOT IN ( SELECT ID FROM ApplicationAsset WHERE Code IN ('INCIDENT__CAN_VIEW_UKVI', 'INCIDENT__REOPEN', 'DISPLAY_SYSTEM_HEALTH_STATUS'))


---- Test Centre Staff - TCS
EXEC proc_AddAccess 'INCIDENT__MODULE_ACCESS', 'TCS'
EXEC proc_AddAccess 'INCIDENT__DRAFT_RAISE', 'TCS'
EXEC proc_AddAccess	'INCIDENT__EDIT_PRE_ACCEPT', 'TCS'
EXEC proc_AddAccess 'INCIDENT__DELETE', 'TCS'
EXEC proc_AddAccess 'INCIDENT__FINALISE_ACTION', 'TCS'
EXEC proc_AddAccess 'INCIDENT__REOPEN_ACTION', 'TCS'
EXEC proc_AddAccess 'INCIDENT__RESPOND_TO_ACTION', 'TCS'
EXEC proc_AddAccess 'INCIDENT__VIEW_LIST_ACTIONS', 'TCS'
EXEC proc_AddAccess 'INCIDENT__VIEW_LIST_INCIDENTS', 'TCS'
EXEC proc_AddAccess 'INCIDENT__VIEW_LIST_CLOSED_ACTIONS', 'TCS'
EXEC proc_AddAccess 'INCIDENT__ACTIVITY__VIEW_LIST', 'TCS'
EXEC proc_AddAccess 'INCIDENT__ACTIVITY__ADD_WORKNOTE', 'TCS'
EXEC proc_AddAccess 'AUDIT__MODULE_ACCESS', 'TCS'
EXEC proc_AddAccess 'AUDIT__TEST_CENTRE_PROFILE', 'TCS'

---- External Test Centre Staff - ETCS
EXEC proc_AddAccess 'INCIDENT__MODULE_ACCESS', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__DRAFT_RAISE', 'TCS_EXTERNAL'
EXEC proc_AddAccess	'INCIDENT__EDIT_PRE_ACCEPT', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__DELETE', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__FINALISE_ACTION', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__REOPEN_ACTION', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__RESPOND_TO_ACTION', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__VIEW_LIST_ACTIONS', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__VIEW_LIST_INCIDENTS', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__VIEW_LIST_CLOSED_ACTIONS', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__ACTIVITY__VIEW_LIST', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'INCIDENT__ACTIVITY__ADD_WORKNOTE', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'AUDIT__MODULE_ACCESS', 'TCS_EXTERNAL'
EXEC proc_AddAccess 'AUDIT__TEST_CENTRE_PROFILE', 'TCS_EXTERNAL'

---- Internal Auditor - AUDITOR_INTERNAL
EXEC proc_AddAccess 'AUDIT__MODULE_ACCESS', 'AUDITOR_INTERNAL'
EXEC proc_AddAccess 'AUDIT__MY_AUDITS_ACCESS', 'AUDITOR_INTERNAL'


---- External Auditor - AUDITOR_EXTERNAL
EXEC proc_AddAccess 'AUDIT__MODULE_ACCESS', 'AUDITOR_EXTERNAL'
EXEC proc_AddAccess 'AUDIT__MY_AUDITS_ACCESS', 'AUDITOR_EXTERNAL'

---- System Monitor - SYSTEM_MONITOR
EXEC proc_AddAccess 'DISPLAY_SYSTEM_HEALTH_STATUS', 'SYSTEM_MONITOR'



IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'proc_AddAccess')
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE proc_AddAccess
END
GO